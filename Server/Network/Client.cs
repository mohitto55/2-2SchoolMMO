
using Server.Network;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Runtime.Serialization;

public class Client
{
    public Client(string id, TcpClient client)
    {
        m_id = id;
        m_tcp = new TCP(client, ProcessPacket, Disconnect);
    }

    public bool isLogin = false;
    public string m_id;

    public TCP m_tcp;

    private void ProcessPacket(byte[] data)
    {
        EHandleType type = (EHandleType)data[0];
        var list = new List<byte>(data);
        list.RemoveAt(0);
        data = list.ToArray();

        var handle = PacketHandlerPoolManager.GetPacketHandler(type);
        handle.Init(data, m_id);
        Server.m_packetHandlerQueue.Enqueue(handle);
    }
    public void SendPacket(PacketHandler packetHandler)
    {
        m_tcp.SendPacket(packetHandler);
    }
    public void Disconnect()
    {
        Server.RemoveClient(m_id);
    }

}
// TCP Wrapper
public class TCP
{
    public const int dataBufferSize = 4096;

    public TcpClient m_socket;

    private Action<byte[]> m_onPacketProcess;
    private Action m_onDisconnect;

    private NetworkStream m_stream;

    private byte[] receiveBuffer = new byte[dataBufferSize];


    public TCP(TcpClient socket,
        Action<byte[]> onPacketProcess = null,
        Action onDisconnectProcess = null)
    {
        m_onDisconnect = onDisconnectProcess;
        m_onPacketProcess = onPacketProcess;

        m_socket = socket;
        m_stream = socket.GetStream();

        m_socket.SendBufferSize = dataBufferSize;
        m_socket.ReceiveBufferSize = dataBufferSize;

        m_stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
    }

    List<byte> m_packetStream = new List<byte>();
    List<byte> m_packetHeader = new List<byte>();
    // 패킷완성에 필요한 크기
    short size;

    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int byteLength = m_stream.EndRead(result);

            if (byteLength <= 0)
            {
                m_onDisconnect.Invoke();
                Disconnect();
                return;
            }
            byte[] data = new byte[byteLength];
            Array.Copy(receiveBuffer, data, byteLength);
            var list = new List<byte>(data);

            // 모든 데이터를 쓸때까지 반복
            while (list.Count > 0)
            {
                // 헤더가 완성되지 않은 상태라면
                if (m_packetHeader.Count < 2)
                {
                    m_packetHeader.Add(list[0]);
                    list.RemoveAt(0);
                }
                else
                {
                    // 헤더가 완성된 상태라면 사이즈를 구함.
                    size = BitConverter.ToInt16(m_packetHeader.ToArray(), 0);

                    // 사이즈와 같아질때까지 반복
                    if (m_packetStream.Count < size)
                    {
                        m_packetStream.Add(list[0]);
                        list.RemoveAt(0);

                        if (m_packetStream.Count == size)
                        {

                            m_onPacketProcess?.Invoke(m_packetStream.ToArray());

                            m_packetStream.Clear();
                            m_packetHeader.Clear();
                        }
                    }

                }
            }

            m_stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }
        catch (Exception _ex)
        {
            Console.WriteLine($"Error receiving TCP data: {_ex}");
            Disconnect();
            m_onDisconnect.Invoke();
        }
    }

    public void Disconnect()
    {
        m_stream.Close();
        m_socket.Close();
    }

    // 클라이언트로 패킷을 전송합니다.
    public void SendPacket(PacketHandler packet)
    {
        var sendData = packet.MergeData();

        m_stream.BeginWrite(sendData, 0, sendData.Length, SendCallBack, null);
    }
    private void SendCallBack(IAsyncResult result)
    {

    }
}
using Runtime.BT.Singleton;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
/// <summary>
/// Ŭ���̾�Ʈ �Ŵ���
/// </summary>
public class NetworkManager : MonoSingleton<NetworkManager>
{
    Queue<byte[]> packetQueue = new Queue<byte[]>();

    TcpClient client;
    NetworkStream stream;

    [SerializeField]
    private string server_ip = "127.0.0.1";
    [SerializeField]
    private int server_port = 4826;
    [SerializeField]
    private static int dataBufferSize = 512;

    private byte[] receiveBuffer = new byte[dataBufferSize];

    public string m_id;

    private Dictionary<EHandleType, Queue<Action<object>>> _responseCallback;

    public bool AutoLoginTry;
    [ShowIf("AutoLoginTry", true)]
    public string loginID;
    [ShowIf("AutoLoginTry", true)]
    public string loginPassword;

    protected override void Awake()
    {
        if (HasInstance())
        {
            DestroyImmediate(gameObject);
            return;
        }



        DontDestroyOnLoad(this.gameObject);
        PacketHandlerPoolManager.Init();

        _responseCallback = new Dictionary<EHandleType, Queue<Action<object>>>();
    } 
    private void Update()
    {
        while (packetQueue.Count > 0)
        {
            var data = packetQueue.Dequeue();
            EHandleType type = (EHandleType)data[0];
            var list = new List<byte>(data);
            list.RemoveAt(0);
            data = list.ToArray();

            Debug.Log(type.ToString() + " ��Ŷ�� �޾ҽ��ϴ�.");
            var handle = PacketHandlerPoolManager.GetPacketHandler(type);
            handle.Init(data, m_id);

            handle.OnProcess();
            InvokeResponseCallback(type, handle.data);
        }
    }

    public void Start()
    {
        if(TryConnect())
        {
            if (AutoLoginTry)
            {
                DtoAccount loginAccount = new DtoAccount("", loginID, loginPassword);
                Instance.SendPacket(EHandleType.LoginRequest, loginAccount);
            }
        }
    }
    public bool TryConnect()
    {
        client = new TcpClient();
        try
        {
            client.Connect("127.0.0.1", 4826);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;
        }

        Debug.Log("����Ȯ��");
        stream = client.GetStream();

        client.SendBufferSize = dataBufferSize;
        client.ReceiveBufferSize = dataBufferSize;

        stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

        return true;
    }
    private void ClientConnectCallBack(IAsyncResult result)
    {
        client.EndConnect(result);
        Debug.Log("����Ȯ��");
        stream = client.GetStream();

        client.SendBufferSize = dataBufferSize;
        client.ReceiveBufferSize = dataBufferSize;
    }
    bool packetRead = false;
    // �ϳ��� ��Ŷ ��Ʈ��

    List<byte> packetStream = new List<byte>();
    List<byte> packetHeader = new List<byte>();
    // ��Ŷ�ϼ��� �ʿ��� ũ��
    short size;
    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int byteLength = stream.EndRead(result);

            if (byteLength <= 0)
            {
                // TODO: disconnect
                return;
            }
            byte[] data = new byte[byteLength];
            Array.Copy(receiveBuffer, data, byteLength);
            var list = new List<byte>(data);

            // ��� �����͸� �������� �ݺ�
            while (list.Count > 0)
            {
                // ����� �������� ���� ���¶��
                if (packetHeader.Count < 2)
                {
                    packetHeader.Add(list[0]);
                    list.RemoveAt(0);
                }
                else
                {
                    // ����� �ϼ��� ���¶�� ����� ����.
                    size = BitConverter.ToInt16(packetHeader.ToArray(), 0);

                    // ������� ������������ �ݺ�
                    if (packetStream.Count < size)
                    {
                        packetStream.Add(list[0]);
                        list.RemoveAt(0);

                        if (packetStream.Count == size)
                        {
                            // �������ų� ��Ŀ���ٸ� ������� ��ť �ϰ� �ٽùݺ�
                            packetQueue.Enqueue(packetStream.ToArray());
                            packetStream.Clear();
                            packetHeader.Clear();
                        }
                    }

                }
            }

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }
        catch (Exception _ex)
        {
            Debug.Log($"Error receiving TCP data: {_ex}");
        }
    }
    public void SendPacket(PacketHandler packet)
    {
        var sendData = packet.MergeData();
        stream.Write(sendData, 0, sendData.Length);
    }

    public void SendPacket(EHandleType handleType, object data)
    {
        if (stream == null) return;
        if (data == null) return;
        
        var sendData = MergeData(handleType, data);
        stream.Write(sendData, 0, sendData.Length);
    }

    // ��Ŷ�����͸� ���ļ� Byte�������� ��ȯ�մϴ�.
    public byte[] MergeData(EHandleType handleType, object data)
    {
        // | size(2byte) | type(1byte) | data(size byte)
        short size = (short)(Marshal.SizeOf(data) + 1);

        var list = new List<byte>();
        list.AddRange(BitConverter.GetBytes(size));
        list.Add((byte)handleType);
        list.AddRange(new List<byte>(SerializeHelper.StructureToByte(data)));
        return list.ToArray();
    }

    public void RegisterResponseCallback(EHandleType handleType, Action<object> callback)
    {
        if (!_responseCallback.ContainsKey(handleType))
        {
            _responseCallback.Add(handleType, new Queue<Action<object>>());
        }

        _responseCallback[handleType].Enqueue(callback);
    }

    public void InvokeResponseCallback(EHandleType handleType, object data)
    {
        if (_responseCallback.ContainsKey(handleType))
        {
            while (_responseCallback[handleType].Count > 0)
            {
                Action<object> callback = _responseCallback[handleType].Dequeue();
                callback?.Invoke(data);
            }
        }
    }
}
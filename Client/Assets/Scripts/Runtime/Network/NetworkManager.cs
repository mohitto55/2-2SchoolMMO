using Runtime.BT.Singleton;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;
/// <summary>
/// 클라이언트 매니저
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
    private static int dataBufferSize = 4096;

    private byte[] receiveBuffer = new byte[dataBufferSize];

    public string m_id;


    protected override void Awake()
    {
        base.Awake();
        PacketHandlerPoolManager.Init();
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

            var handle = PacketHandlerPoolManager.GetPacketHandler(type);
            handle.Init(data, m_id);
            handle.OnProcess();
        }
    }

    public void Start()
    {
        TryConnect();
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

        Debug.Log("접속확인");
        stream = client.GetStream();

        client.SendBufferSize = dataBufferSize;
        client.ReceiveBufferSize = dataBufferSize;

        stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

        return true;
    }
    private void ClientConnectCallBack(IAsyncResult result)
    {
        client.EndConnect(result);
        Debug.Log("접속확인");
        stream = client.GetStream();

        client.SendBufferSize = dataBufferSize;
        client.ReceiveBufferSize = dataBufferSize;
    }
    bool packetRead = false;
    // 하나의 패킷 스트림

    List<byte> packetStream = new List<byte>();
    List<byte> packetHeader = new List<byte>();
    // 패킷완성에 필요한 크기
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

            // 모든 데이터를 쓸때까지 반복
            while (list.Count > 0)
            {
                // 헤더가 지정되지 않은 상태라면
                if (packetHeader.Count < 2)
                {
                    packetHeader.Add(list[0]);
                    list.RemoveAt(0);
                }
                else
                {
                    // 헤더가 완성된 상태라면 사이즈를 구함.
                    size = BitConverter.ToInt16(packetHeader.ToArray(), 0);

                    // 사이즈와 같아질때까지 반복
                    if (packetStream.Count < size)
                    {
                        packetStream.Add(list[0]);
                        list.RemoveAt(0);

                        if (packetStream.Count == size)
                        {
                            // 같아지거나 더커졋다면 결과물을 인큐 하고 다시반복
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
        if (data == null)
        {
            return;
        }
        var sendData = MergeData(handleType, data);
        stream.Write(sendData, 0, sendData.Length);
    }

    // 패킷데이터를 합쳐서 Byte형식으로 반환합니다.
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
}
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System;
using System.Runtime.ConstrainedExecution;


public abstract class PacketHandler
{
    public PacketHandler(object data, EHandleType type)
    {
        m_data = data;
        m_type = type;
    }

    protected string m_id;
    protected EHandleType m_type;
    protected object? m_data;


    private bool m_usable;
    public bool usable
    {
        get => m_usable;
        protected set
        {
            m_usable = value;
        }
    }

    public abstract void Init(byte[] data, string id);
    public abstract void Init(object? data, string id);

    public abstract byte[] MergeData();

    public abstract void OnProcess();

    public void ReturnPool()
    {
        usable = true;
    }

}
public abstract class PacketHandler<T> : PacketHandler where T : DtoBase
{
    public PacketHandler(object data, EHandleType type) : base(data, type)
    {

    }
    public PacketHandler(byte[] data, EHandleType type) : base(data, type)
    {
        m_data = SerializeHelper.ByteToStructure<T>(data);
    }

    public override void Init(object? data, string id)
    {
        m_id = id;
        usable = true;
        m_data = data;
    }
    public override void Init(byte[] data, string id)
    {
        m_id = id;
        usable = true;
        m_data = SerializeHelper.ByteToStructure<T>(data);
    }

    // 패킷데이터를 합쳐서 Byte형식으로 반환합니다.
    public override byte[] MergeData()
    {
        if (m_data == null)
        {
            throw new Exception($"[Warning] {typeof(T).Name} data is null");
        }

        // | size(2byte) | type(1byte) | data(size byte)
        short size = (short)(Marshal.SizeOf(m_data) + 1);

        var list = new List<byte>();
        list.AddRange(BitConverter.GetBytes(size));
        list.Add((byte)m_type);
        list.AddRange(new List<byte>(SerializeHelper.StructureToByte(m_data)));
        return list.ToArray();
    }


    public override void OnProcess()
    {
        T? data = (T?)m_data;

        if (data?.errorCode > 0)
        {
            Console.WriteLine($"[Warning] {data.errorMessage}");
            OnFailed((T)m_data);
        }
        else
        {
            OnSuccess((T)m_data);
        }


        usable = false;


    }
    protected abstract void OnSuccess(T data);
    protected abstract void OnFailed(T data);


}
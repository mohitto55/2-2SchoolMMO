
using MySqlX.XDevAPI;

public class ObjectInfoRequestHandler : PacketHandler<DtoObjectInfo>
{
    public ObjectInfoRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoObjectInfo data)
    {
    }

    protected override void OnSuccess(DtoObjectInfo data)
    {
        var client = IOCPServer.GetClient(m_id);
        SendRequestIDTask(data);
    }
    public async void SendRequestIDTask(DtoObjectInfo objectInfo)
    {
        var client = IOCPServer.GetClient(m_id);
        client.SendPacket(EHandleType.ObjectInfoResponse,new DtoObjectInfo()
             {
                 entityID = objectInfo.entityID
             });
    }
}

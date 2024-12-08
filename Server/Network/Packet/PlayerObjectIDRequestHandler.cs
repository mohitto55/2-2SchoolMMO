
public class PlayerObjectIDRequestHandler : PacketHandler<DtoObjectInfo>
{
    public PlayerObjectIDRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoObjectInfo data)
    {
    }

    protected override void OnSuccess(DtoObjectInfo data)
    {
        var client = IOCPServer.GetClient(m_id);
        client.SendPacket(
            EHandleType.PlayerObjectIDResponse,
            new DtoObjectInfo()
            {
                entityID = client.characterObject.entity.Id
            });
    }
}

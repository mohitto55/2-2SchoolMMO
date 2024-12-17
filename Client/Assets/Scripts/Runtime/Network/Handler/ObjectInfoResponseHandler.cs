public class ObjectInfoResponseHandler : PacketHandler<DtoObjectInfo>
{
    public ObjectInfoResponseHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoObjectInfo data)
    {
    }

    protected override void OnSuccess(DtoObjectInfo data)
    {
        ObjectManager.Instance.GenerateEntityData(data.entityID, data);
    }
}

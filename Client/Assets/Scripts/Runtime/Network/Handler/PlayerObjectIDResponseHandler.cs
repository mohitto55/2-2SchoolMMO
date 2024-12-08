public class PlayerObjectIDResponseHandler : PacketHandler<DtoObjectInfo>
{
    public PlayerObjectIDResponseHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoObjectInfo data)
    {
    }

    protected override void OnSuccess(DtoObjectInfo data)
    {
        var obj = ObjectManager.Instance.GenerateEntityData(data.entityID, data);
        PlayerController.Instance.SetTarget(obj);


    }
}

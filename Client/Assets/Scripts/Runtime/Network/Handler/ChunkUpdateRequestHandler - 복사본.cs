public class BuyItemHandler : PacketHandler<DtoItemData>
{
    public BuyItemHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoItemData data)
    {

    }

    protected override void OnSuccess(DtoItemData data)
    {
        
    }
}
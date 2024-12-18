using Arch.Core.Extensions;

public class ShopItemResponseHandler : PacketHandler<DtoShopData>
{
    public ShopItemResponseHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoShopData data)
    {
    }

    protected override void OnSuccess(DtoShopData data)
    {
    }
}
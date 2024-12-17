using System.Linq;

public class InventoryItemDataResponseHandler : PacketHandler<DtoInventoryItemData>
{
    public InventoryItemDataResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoInventoryItemData data)
    {

    }

    protected override void OnSuccess(DtoInventoryItemData data)
    {
    }
}
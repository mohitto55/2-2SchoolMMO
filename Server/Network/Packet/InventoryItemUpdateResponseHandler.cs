using System.Linq;

public class InventoryItemUpdateResponseHandler : PacketHandler<DtoInventoryItem>
{
    public InventoryItemUpdateResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoInventoryItem data)
    {

    }

    protected override void OnSuccess(DtoInventoryItem data)
    {
    }
}
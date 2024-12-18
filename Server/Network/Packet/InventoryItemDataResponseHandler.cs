using System.Linq;

public class InventoryItemDataResponseHandler : PacketHandler<DtoItemSlotData>
{
    public InventoryItemDataResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoItemSlotData data)
    {

    }

    protected override void OnSuccess(DtoItemSlotData data)
    {

    }
}
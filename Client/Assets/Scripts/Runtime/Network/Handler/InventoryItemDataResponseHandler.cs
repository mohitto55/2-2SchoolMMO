using Runtime.DB.Model;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
        InventoryView inventoryView;
        if (UIView.TryGetView("Inventory", out inventoryView)){
            for(int i = 0; i < data.slotCount; i++)
            {
                var slotItem = data.slotItems[i];
                int inventorySlot = slotItem.inventorySlot;
                InventorySlotModel inventorySlotModel = new InventorySlotModel();
                inventorySlotModel.count = slotItem.count;
                inventoryView.SetInventorySlotModel(inventorySlot, slotItem);
            }
        }
    }
}
using Runtime.DB.Model;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
        Debug.Log("아이템 수신" );
        InventoryView inventoryView;
        if (UIView.TryGetView("Inventory", out inventoryView)){
            for(int i = 0; i < data.slotCount; i++)
            {
                var slotItem = data.slotItems[i];
                int inventorySlot = slotItem.slotIndex;
                ItemSlotModel inventorySlotModel = new ItemSlotModel();
                inventorySlotModel.count = slotItem.item.count;
                inventoryView.SetInventorySlotModel(inventorySlot, slotItem);
            }
        }
    }
}
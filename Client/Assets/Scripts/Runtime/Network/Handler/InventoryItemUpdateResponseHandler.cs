using Runtime.DB.Model;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
        InventoryView inventoryView;

        if(UIView.TryGetView("Inventory", out inventoryView)){
            int inventorySlot = data.inventorySlot;
            
            InventorySlotModel inventorySlotModel = new InventorySlotModel();
            inventorySlotModel.count = data.count;

            inventoryView.SetInventorySlotModel(inventorySlot, data);
        }
    }
}
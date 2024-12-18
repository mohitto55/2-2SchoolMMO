using Runtime.DB.Model;
using Runtime.DB.ViewModel;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : ItemSlot
{
    [SerializeField] protected TextMeshProUGUI _itemNameText;
    [SerializeField] protected TextMeshProUGUI _itemDescText;
    [SerializeField] protected TextMeshProUGUI _itemCostText;

    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnViewModelPropertyChanged(sender, e);
    }

    public override void SlotUpdate(InventorySlotModel itemSlotData)
    {
        base.SlotUpdate(itemSlotData);
        _itemNameText.text = itemSlotData.item.name;
        _itemDescText.text = itemSlotData.item.desc;
        _itemCostText.text = "Cost : " + itemSlotData.item.cost.ToString();
    }
}

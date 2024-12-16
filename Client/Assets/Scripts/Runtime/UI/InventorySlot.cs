using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : UIView
{
    public Image itemImage;
    public TextMeshProUGUI itemCountText;

    protected override void Awake()
    {
        base.Awake();
        ClearSlot();
    }

    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        ItemSlotData itemSlotData = (ItemSlotData)sender;
        if (itemSlotData.count == 0 || itemSlotData.item.sprite == null)
        {
            ClearSlot();
            return;
        }
        itemImage.color = new Color(1, 1, 1, 1);
        itemImage.sprite = itemSlotData.item.sprite;
        itemCountText.text = itemSlotData.count.ToString();
    }

    private void ClearSlot()
    {
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
        itemCountText.text = "";
    }

    public struct ItemSlotData
    {
        public ItemData item;
        public int count;
    }
}

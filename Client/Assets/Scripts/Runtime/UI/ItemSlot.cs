using Runtime.DB.Model;
using Runtime.DB.ViewModel;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : UIView
{
    [SerializeField] protected Image _itemImage;
    [SerializeField] protected Button _itemImageCover;
    [SerializeField] protected TextMeshProUGUI _itemCountText;
    protected ItemSlotModel _itemSlotData;
    protected override void Awake()
    {
        base.Awake();
        ClearSlot();
    }

    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        ItemSlotViewModel itemSlotViewModel = (ItemSlotViewModel)sender;
        if (itemSlotViewModel != null)
        {
            _itemSlotData = itemSlotViewModel.slotItem;
            SlotUpdate(_itemSlotData);
        }
    }

    public virtual void SlotUpdate(ItemSlotModel itemSlotData)
    {
        if (itemSlotData == null || itemSlotData.count == 0 || itemSlotData.sprite == null)
        {
            ClearSlot();
            return;
        }
        _itemImage.color = new Color(1, 1, 1, 1);
        _itemImage.sprite = itemSlotData.sprite;
        _itemCountText.text = itemSlotData.count.ToString();
    }

    private void ClearSlot()
    {
        _itemImage.sprite = null;
        _itemImage.color = new Color(1, 1, 1, 0);
        _itemCountText.text = "";
    }
}

using Runtime.DB.Model;
using Runtime.DB.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class ShopView : UIView
{
    [SerializeField] GameObject _slotGrid;
    [SerializeField] SOItem _soItem;

    private List<ShopItemSlot> _slots = new List<ShopItemSlot>();
    public List<ItemSlotViewModel> _slotViewModels = new List<ItemSlotViewModel>();
    private Dictionary<int, ItemSpriteData> ItemSpriteDataTable = new Dictionary<int, ItemSpriteData>();
    protected override void Awake()
    {
        base.Awake();
        if (_slotGrid != null)
        {
            _slots = _slotGrid.GetComponentsInChildren<ShopItemSlot>().ToList();
        }

        for (int i = 0; i < _soItem.itemdatas.Count; i++)
        {
            ItemSpriteData itemData = _soItem.itemdatas[i];
            if (!ItemSpriteDataTable.ContainsKey(itemData.id))
            {
                ItemSpriteDataTable.Add(itemData.id, itemData);
            }
        }

        for (int i = 0; i < _slots.Count; i++)
        {
            ItemSlotViewModel slotViewModel = new ItemSlotViewModel(new InventorySlotModel(), ItemSpriteDataTable);
            _slots[i].RegisterViewModel(slotViewModel);
            _slotViewModels.Add(slotViewModel);
        }
    }

    public void SlotUpdate(Character character)
    {
        NetworkManager.Instance.SendPacket(EHandleType.InventoryItemDataRequest, new DtoMessage());
    }

    public void SetSlotModel(int slotIndex, DtoItem dtoItem)
    {
        if (slotIndex >= 0 && slotIndex < _slotViewModels.Count)
        {
            InventorySlotModel slotModel = new InventorySlotModel();
            slotModel.count = dtoItem.count;
            slotModel.item = dtoItem;
            if (ItemSpriteDataTable.ContainsKey(dtoItem.itemId))
            {
                slotModel.sprite = ItemSpriteDataTable[dtoItem.itemId].sprite;
            }
            _slotViewModels[slotIndex].slotItem = slotModel;
        }
    }
    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }
}

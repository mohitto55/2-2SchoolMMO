using Runtime.DB.Model;
using Runtime.DB.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class InventoryView : UIView
{
    [SerializeField] GameObject _slotGrid;
    [SerializeField] SOItem _soItem;

    private List<ItemSlot> _slots = new List<ItemSlot>();
    public List<ItemSlotViewModel> _slotViewModels = new List<ItemSlotViewModel>();
    private Dictionary<int, ItemSpriteData> ItemSpriteDataTable = new Dictionary<int, ItemSpriteData>();
    protected override void Awake()
    {
        base.Awake();
        if (_slotGrid != null)
        {
            // 슬롯 갯수 동적으로 만들고 싶은데 m_viewID를 직접 넣어줄순 없다 ㅠㅠ
            _slots = _slotGrid.GetComponentsInChildren<ItemSlot>().ToList();
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
            ItemSlotViewModel slotViewModel = new ItemSlotViewModel(new ItemSlotModel(), ItemSpriteDataTable);
            _slots[i].RegisterViewModel(slotViewModel);
            _slotViewModels.Add(slotViewModel);
        }
    }

    private void Start()
    {
        PlayerController.Instance.OnSetTarget += InventoryUpdate;
    }

    private void OnDisable()
    {
        if (PlayerController.Instance)
            PlayerController.Instance.OnSetTarget -= InventoryUpdate;
    }

    public void InventoryUpdate(Character character)
    {
        NetworkManager.Instance.SendPacket(EHandleType.InventoryItemDataRequest, new DtoMessage());
    }

    public void SetInventorySlotModel(int slotIndex, DtoItemSlot dtoSlot)
    {
        if(slotIndex >= 0 && slotIndex < _slotViewModels.Count)
        {
            ItemSlotModel slotModel = new ItemSlotModel();
            slotModel.count = dtoSlot.item.count;
            slotModel.item = dtoSlot.item;
            if (ItemSpriteDataTable.ContainsKey(dtoSlot.item.itemId)) {
                slotModel.sprite = ItemSpriteDataTable[dtoSlot.item.itemId].sprite;
            }
            _slotViewModels[slotIndex].slotItem = slotModel;
        }
    }
    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }
}

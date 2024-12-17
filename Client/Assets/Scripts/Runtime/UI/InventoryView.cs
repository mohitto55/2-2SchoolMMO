using Runtime.DB.Model;
using Runtime.DB.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryView : UIView
{
    [SerializeField] GameObject _slotGrid;
    [SerializeField] SOItem _soItem;
    private List<InventorySlot> _slots = new List<InventorySlot>();
    public List<InventorySlotViewModel> _slotViewModels = new List<InventorySlotViewModel>();
    private Dictionary<int, ItemData> itemDataTable = new Dictionary<int, ItemData>();
    protected override void Awake()
    {
        base.Awake();
        if (_slotGrid != null)
        {
            _slots = _slotGrid.GetComponentsInChildren<InventorySlot>().ToList();
        }

        
        for (int i = 0; i < _soItem.itemdatas.Count; i++)
        {
            ItemData itemData = _soItem.itemdatas[i];
            if (!itemDataTable.ContainsKey(itemData.id))
            {
                itemDataTable.Add(itemData.id, itemData);
            }
        }

        for (int i = 0; i < _slots.Count; i++)
        {
            InventorySlotViewModel slotViewModel = new InventorySlotViewModel(new InventorySlotModel(), itemDataTable);
            _slots[i].RegisterViewModel(slotViewModel);
            _slotViewModels.Add(slotViewModel);
        }
    }

    private void Start()
    {
        StartCoroutine(SlotUpdate());
    }

    IEnumerator SlotUpdate()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            DtoMessage itemSlotId = new DtoMessage();
            itemSlotId.message = i.ToString();
            NetworkManager.Instance.SendPacket(EHandleType.InventoryItemUpdateRequest, itemSlotId);
        }
    }

    public void SetInventorySlotModel(int slotIndex, DtoInventoryItem dtoItem)
    {
        if(slotIndex >= 0 && slotIndex < _slotViewModels.Count)
        {
            InventorySlotModel slotModel = new InventorySlotModel();
            slotModel.count = dtoItem.count;
            if (itemDataTable.ContainsKey(dtoItem.itemId)) {
                slotModel.item = itemDataTable[dtoItem.itemId];
            }
            Debug.Log("�ε��� : " + slotIndex + " ���̵� : " + dtoItem.itemId);
            _slotViewModels[slotIndex].slotItem = slotModel;
        }
    }
    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }
}

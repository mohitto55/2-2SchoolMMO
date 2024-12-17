using Runtime.DB.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.DB.ViewModel
{
    [System.Serializable]
    public class InventorySlotViewModel : ViewModel<InventorySlotModel>
    {
        public InventorySlotModel slotItem
        {
            get => m_model;
            set
            {
                m_model = value;
                OnPropertyChanged();
            }
        }
        private Dictionary<int, ItemData> itemDataTable;
        public InventorySlotViewModel(InventorySlotModel model, Dictionary<int, ItemData> itemDataTable) : base(model)
        {
            this.itemDataTable = itemDataTable;
        }
    }
}
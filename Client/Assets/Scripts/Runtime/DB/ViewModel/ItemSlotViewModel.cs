using Runtime.DB.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.DB.ViewModel
{
    [System.Serializable]
    public class ItemSlotViewModel : ViewModel<InventorySlotModel>
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
        private Dictionary<int, ItemSpriteData> itemSpriteDataTable;
        public ItemSlotViewModel(InventorySlotModel model, Dictionary<int, ItemSpriteData> itemSpriteDataTable) : base(model)
        {
            this.itemSpriteDataTable = itemSpriteDataTable;
        }
    }
}
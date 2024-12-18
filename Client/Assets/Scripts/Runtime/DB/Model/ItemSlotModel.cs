using UnityEngine;

namespace Runtime.DB.Model
{
    [System.Serializable]
    public class ItemSlotModel : IModel
    {
        public Sprite sprite;
        public DtoItem item;
        public int count;
    }
}
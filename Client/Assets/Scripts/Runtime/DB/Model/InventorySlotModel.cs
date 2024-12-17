namespace Runtime.DB.Model
{
    [System.Serializable]
    public class InventorySlotModel : IModel
    {
        public ItemData item;
        public int count;
    }
}
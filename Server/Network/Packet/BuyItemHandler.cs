using Server.Debug;
using Server.Map;
using Server.MySQL;

public class BuyItemHandler : PacketHandler<DtoItemData>
{
    public BuyItemHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoItemData data)
    {

    }

    protected override void OnSuccess(DtoItemData data)
    {
        ServerDebug.Log(LogType.Log, "아이템 구매 패킷");
        if (!ItemDataManager.IsCorrectItemData(data.item.itemId))
        {
            return;
        }
        string[] splitId = m_id.Split(":");
        string id = splitId.Length > 0 ? splitId[0] : "";
        string uid = DatabaseManager.GetUidFromId(id);
        DatabaseManager.InsertItemToInventory(uid, data.item);
        DatabaseManager.SendInventoryUpdatePacket(uid, m_id);
    }
}
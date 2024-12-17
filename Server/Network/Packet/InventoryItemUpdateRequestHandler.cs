using CommunityToolkit.HighPerformance;
using Server.Debug;
using Server.MySQL;
using System.Linq;

public class InventoryItemUpdateRequestHandler : PacketHandler<DtoMessage>
{
    public InventoryItemUpdateRequestHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoMessage data)
    {
        ServerDebug.Log(LogType.Log, "인벤토리 실패 ㅠㅠ");

    }

    protected override void OnSuccess(DtoMessage data)
    {
        ServerDebug.Log(LogType.Log, "인벤토리");
        string[] splitId = m_id.Split(":");
        string id = splitId.Length > 0 ? splitId[0] : "";
        DtoInventoryItem? inventoryItem = DatabaseManager.GetInventoryItem(id, data.message);
        if (inventoryItem != null)
        {
            ServerDebug.Log(LogType.Log, "아이템이 존재합니다.");

            PacketHandler handler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.InventoryItemUpdateResponse);
            handler.Init(inventoryItem, m_id);
            IOCPServer.SendClient(m_id, handler);
        }
    }
}
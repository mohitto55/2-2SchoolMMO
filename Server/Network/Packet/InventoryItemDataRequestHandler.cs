using CommunityToolkit.HighPerformance;
using Server.Debug;
using Server.MySQL;
using System.Linq;

public class InventoryItemDataRequestHandler : PacketHandler<DtoMessage>
{
    public InventoryItemDataRequestHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoMessage data)
    {
    }

    protected override void OnSuccess(DtoMessage data)
    {
        ServerDebug.Log(LogType.Log, "인벤토리");
        string[] splitId = m_id.Split(":");
        string id = splitId.Length > 0 ? splitId[0] : "";
        DtoInventoryItemData inventoryData = DatabaseManager.GetInventoryItems(id);
        if (inventoryData != null)
        {
            ServerDebug.Log(LogType.Log, "아이템이 존재합니다.");

            PacketHandler handler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.InventoryItemDataResponse);
            handler.Init(inventoryData, m_id);
            IOCPServer.SendClient(m_id, handler);
        }
    }
}
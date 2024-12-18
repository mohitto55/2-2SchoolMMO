using Server.Debug;
using Server.MySQL;

[NPCInteractionTypeAttribure(EInteractionType.Shop)]
public class ShopInteraction : NPCInteraction
{
    public override void Interaction(string playerID, string npcUID)
    {
        ServerDebug.Log(LogType.Log, npcUID + " 상점 상호작용 PlayerID : " + playerID);
        DtoShop? shop = ShopDataManager.GetShopData(npcUID);

        if (shop != null)
        {
            DtoItem[] shopItems = new DtoItem[shop.Value.itemIDTable.Length];
            for (int i = 0; i < shop.Value.itemIDTable.Length; i++)
            {
                DtoItem? item = ItemDataManager.GetItemData(shop.Value.itemIDTable[i]);
                if (item != null)
                    shopItems[i] = item.Value;
            }

            DtoShopData shopData = new DtoShopData(shopItems);
            var handler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.ShopItemResponse);
            handler.Init(shopData, playerID);
            IOCPServer.SendClient(playerID, handler);
        }
    }
}


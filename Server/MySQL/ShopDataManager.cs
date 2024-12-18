using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Server.Debug;
using System.Reflection;
using System.Runtime.InteropServices;
using Utility.Data;


namespace Server.MySQL
{
    public static class ShopDataManager
    {
        private static Dictionary<string, DtoShop> shopTable = new Dictionary<string, DtoShop>();
        public static async Task InitAsync(string url, string gid)
        {
            GoogleSheetLoader loader = new GoogleSheetLoader();
            loader.Initialize();
            loader.AddGuid<DtoShop>(gid);
            IAsyncEnumerator<DtoShop> data = loader.Load<DtoShop>(url);

            // 데이터 확인
            // 비동기 + yield return으로 데이터를 가져오면 순차적으로 순회한다.
            while (await data.MoveNextAsync())
            {
                var current = data.Current;
                if (current.npcUID != null)
                {
                    if (!shopTable.ContainsKey(current.npcUID))
                    {
                        shopTable.Add(current.npcUID, current);
                    }
                }
            }
        }

        public static DtoShop? GetShopData(string npcUID)
        {
            if(shopTable.ContainsKey(npcUID)) 
                return shopTable[npcUID];
            ServerDebug.Log(LogType.Warning, npcUID + "라는 Shop Data는 없습니다.");
            return null;
        }
    }
}

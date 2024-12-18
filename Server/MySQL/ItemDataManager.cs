using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Server.Debug;
using System.Reflection;
using System.Runtime.InteropServices;
using Utility.Data;


namespace Server.MySQL
{
    public static class ItemDataManager
    {
        private static Dictionary<int, DtoItem> itemTable = new Dictionary<int, DtoItem>();
        public static async Task InitAsync(string url, string gid)
        {
            GoogleSheetLoader loader = new GoogleSheetLoader();
            loader.Initialize();
            loader.AddGuid<DtoItem>(gid);
            IAsyncEnumerator<DtoItem> data = loader.Load<DtoItem>(url);

            // 데이터 확인
            while (await data.MoveNextAsync())
            {
                var current = data.Current;
                if (current.name != null)
                {
                    if (!itemTable.ContainsKey(current.itemId))
                    {
                        itemTable.Add(current.itemId, current);
                    }
                }
            }
        }

        public static DtoItem? GetItemData(int itemId)
        {
            if(itemTable.ContainsKey(itemId)) 
                return itemTable[itemId];
            return null;
        }
        public static bool IsCorrectItemData(int itemId)
        {
            return itemTable.ContainsKey(itemId) ? true : false;
        }
    }
}

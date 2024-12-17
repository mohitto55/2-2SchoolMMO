using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Server.Debug;
using System.Reflection;


namespace Server.MySQL
{
    public class DatabaseManager
    {
        public struct UserTable
        {
            public const string table = "users";
            public const string uid = "uid";
            public const string id = "id";
        }

        public struct InventoryTable
        {
            public const string table = "inventory";
            public const string inventoryId = "inventory_id";
            public const string inventorySlot = "inventory_slot";
            public const string userUid = "user_uid";
            public const string itemId = "item_id";
            public const string count = "count";
        }

        public static MySqlConnection _sqlConnection;

        public const string DatabaseName = "db";
        public const string RecordTable = "record";


        // AWS EC2에서 작동중인 Docker>MySQL로 연결한다.
        public static string MySQLServerDNS => "3.35.236.71";
        public static string ConnenctionStr => "host=" + MySQLServerDNS + ";Port=3306;Database=" + DatabaseName + "; UserName=root;Pwd=202411";

        public void Init()
        {
            ConnectSQL();
        }

        private void ConnectSQL()
        {
            _sqlConnection = new MySqlConnection(ConnenctionStr);
            _sqlConnection.Open();
        }

        public static RegisterResult RegisterPlayer(DtoAccount dtoAccount)
        {
            ServerDebug.Log(LogType.Log, "Try Register Player");
            RegisterData registerData = new RegisterData(dtoAccount);

            if (!IsValidUsername(dtoAccount.username))
            {
                ServerDebug.Log(LogType.Log, dtoAccount.username + " 은 적절한 이름이 아닙니다.");
                return RegisterResult.NotValidUsername;
            }

            if (IsUserRegistered(dtoAccount.id))
            {
                ServerDebug.Log(LogType.Log, dtoAccount.id + " 는 이미 존재하는 ID입니다.");
                return RegisterResult.AlreadyRegister;
            }

            string[] userDataName = GetFieldNames(registerData);
            string[] userDataValue = GetFieldValues(registerData);

            //// errormessage와 errorCode 제거
            //userDataName = userDataName.Take(userDataName.Length - 2).ToArray();
            //userDataValue = userDataValue.Take(userDataValue.Length - 2).ToArray();

            ServerDebug.Log(LogType.Log, "User Data Name : ", userDataName);
            ServerDebug.Log(LogType.Log, "User Data Value : ", userDataValue);

            string cmd = MySQLUtility.GetInsertCmd(UserTable.table, userDataName, userDataValue);

            try
            {
                MySQLUtility.ExcuteSQL(cmd, _sqlConnection);
            }
            catch (Exception ex)
            {
                return RegisterResult.Faild;
            }

            return RegisterResult.Success;
        }

        public static bool IsUserRegistered(string userId)
        {
            string cmd = String.Format("SELECT id FROM {0} where id = '{1}';", UserTable.table, userId);
            string[] data = MySQLUtility.ExcuteSQL(cmd, _sqlConnection);

            if (data.Length > 0 && data[0] == userId)
            {
                return true;
            }
            return false;
        }

        public static LoginResult UserLogin(string userId, string password)
        {
            if (!IsUserRegistered(userId))
            {
                return LoginResult.NonexistentId;
            }
            string selectPasswordCmd = String.Format("SELECT password FROM {0} where id = '{1}';", UserTable.table, userId);
            string[] data = MySQLUtility.ExcuteSQL(selectPasswordCmd, _sqlConnection);
            if (data.Length > 0 && data[0] == password)
            {
                return LoginResult.Success;
            }
            return LoginResult.PasswordDoesNotMatch;
        }

        public static string GetUidFromId(string userId)
        {
            if (!IsUserRegistered(userId))
            {
                return "";
            }
            string selectPasswordCmd = $"SELECT {UserTable.uid} FROM {UserTable.table} where {UserTable.id} = '{userId}';";
            string[] data = MySQLUtility.ExcuteSQL(selectPasswordCmd, _sqlConnection);
            if (data.Length > 0)
            {
                return data[0];
            }
            return "";
        }


        public static string[] GetFieldNames(object data)
        {
            FieldInfo[] fieldInfos = data.GetType().GetFields();
            string[] datas = new string[fieldInfos.Length];
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                datas[i] = fieldInfo.Name;
            }
            return datas;
        }

        public static string[] GetFieldValues(object data)
        {
            FieldInfo[] fieldInfos = data.GetType().GetFields();
            string[] datas = new string[fieldInfos.Length];
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                object value = fieldInfo.GetValue(data);
                datas[i] = value?.ToString() ?? string.Empty;
            }
            return datas;
        }

        //public static string[] GetUserData(string userId)
        //{

        //}

        /// <summary>
        /// 유저 이름이 생성될 수 있는 이름인지 확인하는 임시 함수
        /// 나중에 정규 표현식으로 확장가능
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsValidUsername(string username)
        {
            return username != null && username.Length > 0;
        }

        public enum RegisterResult
        {
            Success,
            Faild,
            NotValidUsername,
            AlreadyRegister
        }

        public enum LoginResult
        {
            Success,
            NonexistentId,
            PasswordDoesNotMatch
        }

        public static void InsertInventoryItem(int userUid, int slot, int itemId, int count)
        {
            string[] tableName = new string[4] { InventoryTable.userUid, InventoryTable.inventorySlot, InventoryTable.itemId, InventoryTable.count };
            string[] tableValue = new string[4] { userUid.ToString(), slot.ToString(), itemId.ToString(), count.ToString() };
            string insertCmd = MySQLUtility.GetInsertCmd(InventoryTable.table, tableName, tableValue);

            try
            {
                MySQLUtility.ExcuteSQL(insertCmd, _sqlConnection);
            }
            catch (Exception ex)
            {
                ServerDebug.Log(LogType.Warning, ex.Message);
            }
        }

        public static void UpdateInventoryItem(string userUid, int slot, int count)
        {
            string updateCmd = $"UPDATE {InventoryTable.table} SET {InventoryTable.count} = {count} WHERE {InventoryTable.inventorySlot} = '{slot}' AND {InventoryTable.userUid} = '{userUid}';";
            try
            {
                MySQLUtility.ExcuteSQL(updateCmd, _sqlConnection);
            }
            catch (Exception ex)
            {
                ServerDebug.Log(LogType.Warning, ex.Message);
            }
        }

        public static DtoInventoryItemData? GetInventoryItems(string userId)
        {
            string userUid = GetUidFromId(userId);

            if (userUid == "")
            {
                return null;
            }

            string cmd = $"SELECT {InventoryTable.inventoryId}, {InventoryTable.itemId}, {InventoryTable.count}, {InventoryTable.inventorySlot} " +
                $"FROM {InventoryTable.table} WHERE {InventoryTable.userUid} = '{userUid}';";
            string[][] data = MySQLUtility.GetSQLColumn(cmd, _sqlConnection);

            DtoInventoryItemData inventoryItemData = new DtoInventoryItemData();
            int slotCount = data.Length;
            inventoryItemData.slotItems = new DtoInventoryItem[100];
            for(int i = 0; i < data.Length; i++)
            {
                string[] slotDataColumn = data[i];
                if (slotDataColumn.Length <= 0)
                    continue;

                inventoryItemData.slotCount++;
                DtoInventoryItem item = new DtoInventoryItem();
                item.inventoryId = int.Parse(slotDataColumn[0]);
                item.itemId = int.Parse(slotDataColumn[1]);
                item.count = int.Parse(slotDataColumn[2]);
                item.inventorySlot = int.Parse(slotDataColumn[3]);
                inventoryItemData.slotItems[i] = item;

            }

            return inventoryItemData;
        }

        public static bool HasInventoryItemInSlot(string userUid, string slot)
        {
            string cmd = $"SELECT {InventoryTable.userUid} FROM {InventoryTable.table} where {InventoryTable.userUid} = '{userUid}' AND {InventoryTable.inventorySlot} = '{slot}';";
            string[] data = MySQLUtility.ExcuteSQL(cmd, _sqlConnection);

            if (data.Length > 0 && data[0] == userUid.ToString())
            {
                return true;
            }
            return false;
        }
    }
}

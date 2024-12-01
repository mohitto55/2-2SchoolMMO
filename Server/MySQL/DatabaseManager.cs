using MySql.Data.MySqlClient;
using Server.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.MySQL
{
    public class DatabaseManager
    {
        public static MySqlConnection _sqlConnection;

        public const string DatabaseName = "db";
        public const string UserTable = "users";
        public const string RecordTable = "record";


        // AWS EC2에서 작동중인 Docker>MySQL로 연결한다.
        public static string MySQLServerDNS => "13.125.54.178";
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

        public static void RegisterPlayer(DtoAccount dtoAccount)
        {
            ServerDebug.Log(LogType.Log, "Try Register Player");

            string[] userDataName = SerializeDataPropertyValue(dtoAccount);
            string[] userDataValue = SerializeDataPropertyValue(dtoAccount);
            ServerDebug.Log(LogType.Log, userDataName);
            ServerDebug.Log(LogType.Log, userDataValue);
            string cmd = MySQLUtility.GetInsertStr(UserTable, userDataName, userDataValue);
            MySQLUtility.ExcuteSQL(cmd, _sqlConnection);
        }

        public static string[] SerializeDataPropertyName(DtoBase dto)
        {
            PropertyInfo[] propertyInfos = dto.GetType().GetProperties();
            string[] datas = new string[propertyInfos.Length];
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfos[i];
                datas[i] = propertyInfo.Name;
            }
            return datas;
        }

        public static string[] SerializeDataPropertyValue(DtoBase dto)
        {
            PropertyInfo[] propertyInfos = dto.GetType().GetProperties();
            string[] datas = new string[propertyInfos.Length];
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfos[i];
                object value = propertyInfo.GetValue(dto);
                datas[i] = value?.ToString() ?? string.Empty;
            }
            return datas;
        }

        class RegisterData : DBData
        {
            public string username;
            public string password;
            public string creation_date;
            public string last_login;
            public string status;
            public string email;
        }
    }
}

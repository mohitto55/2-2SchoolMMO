using MySql.Data.MySqlClient;
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
        public static Socket server;

        public MySqlConnection _sqlConnection;

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

        public void RegisterPlayer(DtoAccount dtoAccount)
        {
            Assembly.GetExecutingAssembly().GetTypes().
                Where(t => t.IsClass && t.IsSubclassOf(typeof(DBData))).ToString();
        }

        class RegisterData : DBData
        {
            public string username;
            public string password;
            public string creation_date;
            public string last_login;
            public string status;
            public string email;
            //public string InsertTable(string tableName, string[] value)
            //{
            //    string command = "INSERT INTO " + tableName + " ";
            //}
        }
    }
}

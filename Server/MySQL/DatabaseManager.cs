using MySql.Data.MySqlClient;
using Server.Debug;
using System.Reflection;


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
            RegisterData registerData = new RegisterData(dtoAccount);

            if (!IsValidUsername(dtoAccount.username))
            {
                ServerDebug.Log(LogType.Log, dtoAccount.username + " 은 적절한 이름이 아닙니다.");
                return;
            }

            if (IsUserRegistered(dtoAccount.id))
            {
                ServerDebug.Log(LogType.Log, dtoAccount.id + " 는 이미 존재하는 ID입니다.");
                return;
            }

            string[] userDataName = GetFieldNames(registerData);
            string[] userDataValue = GetFieldValues(registerData);

            //// errormessage와 errorCode 제거
            //userDataName = userDataName.Take(userDataName.Length - 2).ToArray();
            //userDataValue = userDataValue.Take(userDataValue.Length - 2).ToArray();

            ServerDebug.Log(LogType.Log, "User Data Name : ", userDataName);
            ServerDebug.Log(LogType.Log, "User Data Value : ", userDataValue);

            string cmd = MySQLUtility.GetInsertStr(UserTable, userDataName, userDataValue);
            MySQLUtility.ExcuteSQL(cmd, _sqlConnection);
        }

        public static bool IsUserRegistered(string userId)
        {
            string cmd = String.Format("SELECT id FROM {0} where id = '{1}';", UserTable, userId);
            string[] data = MySQLUtility.ExcuteSQL(cmd, _sqlConnection);

            if (data.Length > 0 && data[0] == userId)
            {
                return true;
            }
            return false;
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
    }
}

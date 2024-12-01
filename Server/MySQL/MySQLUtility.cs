using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Server.Debug;

namespace Server.MySQL
{

    public static class MySQLUtility
    {

        public static string[] ExcuteSQL(string cmd, MySqlConnection _sqlConnection)
        {
            ServerDebug.Log(LogType.Log, "Excute SQL Cmd : " + cmd);
            List<string> data = new List<string>();
            try
            {
                MySqlCommand sqlCmd = new MySqlCommand(cmd, _sqlConnection);
                using (MySqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    int k = 0;
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data.Add(String.Format("{0}", reader[0]));
                        }
                    }
                }
                ServerDebug.Log(LogType.Log, "Excute SQL Cmd Success");
            }
            catch (Exception e)
            {
                ServerDebug.Log(LogType.Warning, e.Message);
            }
            return data.ToArray();
        }

        public static string[][] GetSQLColumn(string cmd, MySqlConnection _sqlConnection)
        {
            List<string[]> data = new List<string[]>();
            try
            {
                MySqlCommand sqlCmd = new MySqlCommand(cmd, _sqlConnection);
                using (MySqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    int k = 0;
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[k][i] = String.Format("{0}", reader[i]);
                        }
                        k++;
                    }

                }
            }
            catch (Exception e)
            {
            }
            return data.ToArray();
        }

        public static string GetInsertStr(string table, string[] names, string[] values)
        {
            // 기본 INSERT 구문 시작
            string interStr = "INSERT INTO " + table + " (";

            // 열 이름 추가
            for (int i = 0; i < names.Length; i++)
            {
                interStr += names[i];
                if (i < names.Length - 1)
                {
                    interStr += ", ";
                }
            }

            interStr += ") VALUES (";

            // 값 추가
            for (int i = 0; i < values.Length; i++)
            {
                interStr += "'" + values[i] + "'";
                if (i < values.Length - 1)
                {
                    interStr += ", ";
                }
            }

            interStr += ");";
            return interStr;
        }
    }
}

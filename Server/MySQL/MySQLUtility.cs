using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server.MySQL
{

    public static class MySQLUtility
    {

        public static string[] ExcuteSQL(string cmd, MySqlConnection _sqlConnection)
        {
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
            }
            catch (Exception e)
            {

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
    }
}

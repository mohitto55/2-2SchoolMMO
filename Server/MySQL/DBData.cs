using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MySQL
{
    [System.Serializable]
    public class DBData
    {
    }

    class RegisterData : DBData
    {
        public string id;
        public string username;
        public string password;
        public string creation_date;
        public string status;
        public string email;

        public RegisterData(DtoAccount dtoAccount)
        {
            id = dtoAccount.id;
            username = dtoAccount.username;
            password = dtoAccount.password;
            creation_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            status = "active";
            email = "ck@naver.com";
        }
    }
}

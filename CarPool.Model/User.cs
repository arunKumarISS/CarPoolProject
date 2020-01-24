using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Model
{
    public class User
    {
        public string Name;
        public string UserId;
        public string Password;
        public string PhoneNumber;
        public double Wallet;

        public User(string name, string password)
        {
            Name = name;
            UserId = name.ToUpper().Substring(0, 3) + DateTime.Now.ToString("hhmmss");
            Password = password;
            Wallet = 0;
        }
    }
}

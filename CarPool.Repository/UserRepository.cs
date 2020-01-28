using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;
using CarPool.Database;

namespace CarPool.Repository
{
    public class UserRepository<T> : IRepository<T> where T : User
    {
        public void Add(T user)
        {
            DataBase.Users.Add(user);
        }

        public User GetById(string userId)
        {
            foreach(var user in DataBase.Users)
            {
                if(string.Equals(userId, user.UserId))
                {
                    return user;
                }
            }
            return null;
        }

        public bool Delete(T user)
        {
            return DataBase.Users.Remove(user);
        }

        public void Update(T user)
        {

        }
    }
}

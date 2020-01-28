using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Model;
using CarPool.Database;
using CarPool.Repository;

namespace CarPool.Services
{
    public class UserService  : EntityBase
    {
        public User CreateUser(string name, string password)
        {
            User NewUser = new User(name, password);
            DataBase.Users.Add(NewUser);
            return NewUser;
        }

        public void Add(User user)
        {
            UserRepository<User> userRepository = new UserRepository<User>();
            userRepository.Add(user);
        }

        public bool CheckUserCredentials(string userId, string password)
        {
            foreach (var user in DataBase.Users)
            {
                string UserId = user.UserId;
                string Password = user.Password;
                if (UserId == userId && Password == password)
                {

                    return true;
                }
            }
            return false;
        }

        public void AddMoneyToWallet(double amount, string userId)
        {
            foreach(var user in DataBase.Users)
            {
                if(user.UserId.Equals(userId))
                {
                    user.Wallet += amount;
                    break;
                }
            }
        }

        public double GetWalletBalance(string userId)
        {
            foreach(var user in DataBase.Users)
            {
                if(user.UserId.Equals(userId))
                {
                    return user.Wallet;                    
                }
            }
            return 0;
        }
    }
}

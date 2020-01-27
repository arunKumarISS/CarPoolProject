using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Model;
using CarPool.Database;

namespace CarPool.Services
{
    public class UserService
    {
        public User CreateUser(string name, string password)
        {
            User NewUser = new User(name, password);
            return NewUser;
        }



        public bool CheckUserCredentials(string userId, string password)
        {
            foreach (var user in DataBase.Users)
            {
                if (user.UserId.Equals(userId) && user.Password.Equals(password))
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

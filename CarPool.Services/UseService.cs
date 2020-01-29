using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Model;
using CarPool.Database;
using CarPool.Repository;

namespace CarPool.Services
{
    public class UserService  
    {
        

        public User CreateUser(string name, string password)
        {
            User NewUser = new User(name, password);
            Repository<User>.Add(NewUser);
            return NewUser;
        }

        public bool CheckUserCredentials(string userId, string password)
        {
            User User = Repository<User>.GetById(userId);
            if (string.Equals(User.Password, password))
                return true;
            else
                return false;
        }

        public void AddMoneyToWallet(double amount, string userId)
        {
            User User = Repository<User>.GetById(userId);
            User.Wallet += amount;
        }

        public double GetWalletBalance(string userId)
        {
            return Repository<User>.GetById(userId).Wallet;
        }
    }
}

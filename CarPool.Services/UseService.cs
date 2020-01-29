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

        Repository<User> userRepository = new Repository<User>();
        public User CreateUser(string name, string password)
        {
            User NewUser = new User(name, password);
            userRepository.Add(NewUser);
            return NewUser;
        }

        public bool CheckUserCredentials(string userId, string password)
        {
            User User = userRepository.GetById(userId);
            if (string.Equals(User.Password, password))
                return true;
            else
                return false;
        }

        public void AddMoneyToWallet(double amount, string userId)
        {
            User User = userRepository.GetById(userId);
            User.Wallet += amount;
        }

        public double GetWalletBalance(string userId)
        {
            return userRepository.GetById(userId).Wallet;
        }

        
    }
}

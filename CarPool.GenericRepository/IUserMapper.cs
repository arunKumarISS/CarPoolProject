using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.GenericRepository
{
    public interface IUserMapper
    {
        User Find(string userId);

        List<User> GetAll();

        void Add(User user);
        
        void Update(User user);

        void Delete(User user);
    }
}

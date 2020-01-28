using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.Repository
{
    public interface IRepository<T> where T : User
    {
        User GetById(string id);

        void Add(T entity);

        bool Delete(T entity);

        void Update(T entity);
    }
}

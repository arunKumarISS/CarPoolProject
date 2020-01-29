using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.Repository
{
    public interface IRepository<T>  
    {
        List<T> GetList();

        T GetById(string id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}

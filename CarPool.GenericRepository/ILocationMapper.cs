using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.GenericRepository
{
    interface ILocationMapper
    {
        Location Find(string LocationName);

        List<Location> GetAll();

        void Add(Location location);

        void Update(Location location);

        void Delete(Location location);
    }
}

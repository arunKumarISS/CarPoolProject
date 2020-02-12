using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Database;
using CarPool.Model;

namespace CarPool.Controllers
{
    class OffersController
    {
        static void Main(string[] args)
        {

        }

        public void addclass()
        {
            CarPoolEntities context = new CarPoolEntities();
            var queryExample = context.BookingTables;
            Booking user = new Booking();
            var groups = context.OfferTables.ToList();
        }
    }

    
}

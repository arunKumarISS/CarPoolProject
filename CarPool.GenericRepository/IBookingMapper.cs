using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.GenericRepository
{
    interface IBookingMapper
    {
        Booking Find(string bookingId);

        List<Booking> GetAll();

        void Add(Booking booking);

        void Update(Booking booking);

        void Delete(Booking booking);
    }
}

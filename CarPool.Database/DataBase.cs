using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;

namespace CarPool.Database
{
    public class DataBase
    {
        public static List<User> Users = new List<User>();
        public static List<Offer> Offers = new List<Offer>();
        public static List<Booking> Bookings = new List<Booking>();
        public static List<OfferRequest> OfferRequests = new List<OfferRequest>();
        public static List<Payment> Payments = new List<Payment>();
        public static List<Location> Locations = new List<Location>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;
using Newtonsoft.Json;
using System.IO;

namespace CarPool.Database
{
    public class DataBase
    {
        //public static List<User> Users = new List<User>();
        //public static List<Offer> Offers = new List<Offer>();
        //public static List<Booking> Bookings = new List<Booking>();
        //public static List<OfferRequest> OfferRequests = new List<OfferRequest>();
        //public static List<Payment> Payments = new List<Payment>();
        //public static List<Location> Locations = new List<Location>();


        IDictionary<string, string> ListOfPaths = new Dictionary<string, string>()
        {
            {"USER",@"D:\tasks\CarPool\Documents\users.json" },
            {"OFFER", @"D:\tasks\CarPool\Documents\offers.json"},
            {"BOOKING",  @"D:\tasks\CarPool\Documents\bookings.json"},
            {"OFFERREQUEST", @"D:\tasks\CarPool\Documents\offerRequests.json"},
            {"PAYMENT", @"D:\tasks\CarPool\Documents\payments.json"},
            {"LOCATION", @"D:\tasks\CarPool\Documents\locations.json"}
        };                                    


        public void Save()
        {
            string UsersString = JsonConvert.SerializeObject(DataBase.Users);
            File.WriteAllText(@"D:\tasks\CarPool\Documents\users.json", UsersString);

            string OffersString = JsonConvert.SerializeObject(DataBase.Offers);
            File.WriteAllText(@"D:\tasks\CarPool\Documents\offers.json", OffersString);

            string BookingsString = JsonConvert.SerializeObject(DataBase.Bookings);
            File.WriteAllText(@"D:\tasks\CarPool\Documents\bookings.json", BookingsString);

            string OfferRequestsString = JsonConvert.SerializeObject(DataBase.OfferRequests);
            File.WriteAllText(@"D:\tasks\CarPool\Documents\offerRequests.json", OfferRequestsString);

            string PaymentsString = JsonConvert.SerializeObject(DataBase.Payments);
            File.WriteAllText(@"D:\tasks\CarPool\Documents\payments.json", PaymentsString);

            string LocationsString = JsonConvert.SerializeObject(DataBase.Locations);
            File.WriteAllText(@"D:\tasks\CarPool\Documents\locations.json", LocationsString);
        }

        public void GetDataFromJson(string type)
        {
            string result;
            ListOfPaths.TryGetValue(type, out result);
            objects = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(result));
        }



    }
}

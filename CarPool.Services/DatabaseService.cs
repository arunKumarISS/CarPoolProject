using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Database;
using Newtonsoft.Json;
using System.IO;
using CarPool.Model;

namespace CarPool.Services
{
    public class DatabaseService
    {
        public void MoveDataToJson()
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

        public void GetDataFromJson()
        {
            string UsersString = File.ReadAllText(@"D:\tasks\CarPool\Documents\users.json");
            DataBase.Users = JsonConvert.DeserializeObject<List<User>>(UsersString);

            string OffersString = File.ReadAllText(@"D:\tasks\CarPool\Documents\offers.json");
            DataBase.Offers = JsonConvert.DeserializeObject<List<Offer>>(OffersString);

            string BookingsString = File.ReadAllText(@"D:\tasks\CarPool\Documents\bookings.json");
            DataBase.Bookings = JsonConvert.DeserializeObject<List<Booking>>(BookingsString);

            string OfferRequestsString = File.ReadAllText(@"D:\tasks\CarPool\Documents\offerRequests.json");
            DataBase.OfferRequests = JsonConvert.DeserializeObject<List<OfferRequest>>(OfferRequestsString);

            string PaymentsString = File.ReadAllText(@"D:\tasks\CarPool\Documents\payments.json");
            DataBase.Payments = JsonConvert.DeserializeObject<List<Payment>>(PaymentsString);

            string LocationsString = File.ReadAllText(@"D:\tasks\CarPool\Documents\locations.json");
            DataBase.Locations = JsonConvert.DeserializeObject<List<Location>>(LocationsString);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Enums;

namespace CarPool.Model
{
    public class Offer : EntityBase
    {
        
        public string DriverName;
        public string RiderId;
        public Location FromLocation;
        public Location ToLocation;
        public int Availability;
        public string VehicleRegNumber;
        public string VehicleModel;
        public string PhoneNumber;
        public IEnums.OfferStatus Status;
        public List<Location> ViaPoints = new List<Location>();

        public Offer(string driverName, string riderId, Location fromLocation, Location toLocation, int availability, string vehicleRegNumber, string vehicleModel, IEnums.OfferStatus status)
        {
            DriverName = driverName;
            RiderId = riderId;
            FromLocation = fromLocation;
            ToLocation = toLocation;
            Availability = availability;
            VehicleRegNumber = vehicleRegNumber;
            VehicleModel = vehicleModel;
            Status = status;
            Id = RiderId.Substring(0, 3) + DateTime.Now.ToString("hhmmss");
            ViaPoints.Add(fromLocation);
            ViaPoints.Add(toLocation);
        }

        public Offer()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Enums;

namespace CarPool.Model
{
    public class Booking
    {
        public Location FromLocation;
        public Location ToLocation;
        public string RideeId;
        public string RiderId;
        public IEnums.BookingStatus Status;
        
        public double Fair;
        public int NumberOfPassengers;

        public Booking(string riderId, string rideeId, Location fromLocation, Location toLocation, int numnberOfPassengers, double fair)
        {
            RiderId = riderId;
            FromLocation = fromLocation;
            ToLocation = toLocation;
            RideeId = rideeId;
            Status = IEnums.BookingStatus.Pending;
            NumberOfPassengers = numnberOfPassengers;
            Fair = fair;
        }

        public Booking()
        {

        }
    }
}

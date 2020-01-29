using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Enums;

namespace CarPool.Model 
{
    public class OfferRequest : EntityBase
    {
        public string RiderId;
        public string RideeId;
        public Location FromLocation;
        public Location ToLocation;
        public IEnums.RequestStatus Status;
        public int NumberOfPassengers;
        

        public OfferRequest(Location fromLocation, Location toLocation, int numberOfPassengers, string riderId, string rideeId)
        {
            RiderId = riderId;
            RideeId = rideeId;
            FromLocation = fromLocation;
            ToLocation = toLocation;
            Status = IEnums.RequestStatus.Pending;
            NumberOfPassengers = numberOfPassengers;
            Id = rideeId.Substring(0, 3) + DateTime.Now.ToString("hhmmss") + riderId.Substring(0, 3);
        }
    }
}

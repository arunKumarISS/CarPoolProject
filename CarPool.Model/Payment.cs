using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Enums;

namespace CarPool.Model
{
    public class Payment : EntityBase
    {
        
        public string RiderId;
        public string RideeId;
        public double Fair;
        public IEnums.PaymentStatus Status;

        public Payment(string riderId, string rideeId, double fair)
        {
            RiderId = riderId;
            RideeId = rideeId;
            Fair = fair;
            Status = IEnums.PaymentStatus.Pending;
            // All Id generators can me moved to a separate helper class.
            Id = riderId.Substring(0, 3) + DateTime.Now.ToString("hhmm") + rideeId.Substring(0, 3);
        }
    }
}

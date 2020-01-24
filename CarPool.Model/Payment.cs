using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Enums;

namespace CarPool.Model
{
    public class Payment
    {
        public string PaymentID;
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
            PaymentID = riderId.Substring(0, 3) + DateTime.Now.ToString("hhmmss") + rideeId.Substring(0, 3);
        }
    }
}

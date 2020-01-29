using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Database;
using CarPool.Model;
using CarPool.Enums;
using CarPool.Repository;

namespace CarPool.Services
{
    public class PaymentService
    {
        public bool Pay(string paymentId)
        {
            foreach(var payment in Repository<Payment>.GetList())
            {
                if(payment.Id.Equals(paymentId))
                {
                    foreach(var ridee in Repository<User>.GetList())
                    {
                        if(ridee.Id.Equals(payment.RideeId))
                        {
                            if (ridee.Wallet >= payment.Fair)
                                ridee.Wallet -= payment.Fair;
                            else
                                return false;
                        }
                    }
                    foreach(var rider in Repository<User>.GetList())
                    {
                        if(rider.Id.Equals(payment.RiderId))
                        {
                            rider.Wallet += payment.Fair;
                        }
                    }
                    payment.Status = IEnums.PaymentStatus.Paid;
                    return true;
                }
            }
            return false;
        }

        public void AddPaymentDue(string riderId)
        {
            foreach (var booking in Repository<Booking>.GetList())
            {
                if (booking.RiderId.Equals(riderId) && booking.Status.Equals(IEnums.BookingStatus.RideStarted))
                {
                    Payment NewPayment = new Payment(riderId, booking.RideeId, booking.Fair);
                    Repository<Payment>.Add(NewPayment);
                }
            }
        }

        public bool IsEligibleToBook(string userId)
        {
            int count = 0;
            foreach (var payment in Repository<Payment>.GetList())
            {
                if (payment.RideeId.Equals(userId) && payment.Status.Equals(IEnums.PaymentStatus.Pending))
                    count++;
            }
            if (count >= 1)
                return false;
            else
                return true;
        }

        public List<Payment> GetPendingDues(string userId)
        {
            List<Payment> PendingDues = new List<Payment>();
            foreach(var payment in Repository<Payment>.GetList())
            {
                if(payment.RideeId.Equals(userId) && payment.Status.Equals(IEnums.PaymentStatus.Pending))
                {
                    PendingDues.Add(payment);
                }
            }
            return PendingDues;
        }

        public List<Payment> GetPaymentHistory(string UserId)
        {
            List<Payment> AllPayments = new List<Payment>();
            foreach(var payment in Repository<Payment>.GetList())
            {
                if(payment.RideeId.Equals(UserId) || payment.RiderId.Equals(UserId))
                {
                    AllPayments.Add(payment);
                }
            }
            return AllPayments;
        }

    }
}

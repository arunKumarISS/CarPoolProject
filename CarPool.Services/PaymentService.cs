using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Database;
using CarPool.Model;
using CarPool.Enums;

namespace CarPool.Services
{
    public class PaymentService
    {
        public bool Pay(string paymentId)
        {
            foreach(var payment in DataBase.Payments)
            {
                if(payment.PaymentID.Equals(paymentId))
                {
                    foreach(var ridee in DataBase.Users)
                    {
                        if(ridee.UserId.Equals(payment.RideeId))
                        {
                            if (ridee.Wallet >= payment.Fair)
                                ridee.Wallet -= payment.Fair;
                            else
                                return false;
                        }
                    }
                    foreach(var rider in DataBase.Users)
                    {
                        if(rider.UserId.Equals(payment.RiderId))
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
            foreach (var booking in DataBase.Bookings)
            {
                if (booking.RiderId.Equals(riderId) && booking.Status.Equals(IEnums.BookingStatus.Confirmed))
                {
                    Payment NewPayment = new Payment(riderId, booking.RideeId, booking.Fair);
                    DataBase.Payments.Add(NewPayment);
                }
            }
        }

        public bool IsEligibleToBook(string userId)
        {
            int count = 0;
            foreach (var payment in DataBase.Payments)
            {
                if (payment.RideeId.Equals(userId) && payment.Status.Equals(IEnums.PaymentStatus.Pending))
                    count++;
            }
            if (count >= 3)
                return false;
            else
                return true;
        }

        public List<Payment> DisplayPendingDues(string userId)
        {
            List<Payment> PendingDues = new List<Payment>();
            foreach(var payment in DataBase.Payments)
            {
                if(payment.RideeId.Equals(userId) && payment.Status.Equals(IEnums.PaymentStatus.Pending))
                {
                    PendingDues.Add(payment);
                }
            }
            return PendingDues;
        }

        public List<Payment> DisplayPaymentHistory(string UserId)
        {
            List<Payment> AllPayments = new List<Payment>();
            foreach(var payment in DataBase.Payments)
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

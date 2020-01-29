using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Model;
using CarPool.Enums;
using CarPool.Repository;

namespace CarPool.Services
{
    public class PaymentService
    {
        Repository<Payment> paymentRepository = new Repository<Payment>();
        public bool Pay(string paymentId)
        {
            
            Repository<User> userRepository = new Repository<User>();

            Payment payment = paymentRepository.GetById(paymentId);
            User ridee = userRepository.GetById(payment.RideeId);
            if (ridee.Wallet >= payment.Fair)
            {
                ridee.Wallet -= payment.Fair;
                User rider = userRepository.GetById(payment.RiderId);
                rider.Wallet += payment.Fair;
                payment.Status = IEnums.PaymentStatus.Paid;
                return true;
            }
            else
                return false;
        }

        public void AddPaymentDue(string riderId)
        {
            Repository<Booking> bookingRepository = new Repository<Booking>();
            foreach (var booking in bookingRepository.GetList())
            {
                if (booking.RiderId.Equals(riderId) && booking.Status.Equals(IEnums.BookingStatus.RideStarted))
                {
                    Payment NewPayment = new Payment(riderId, booking.RideeId, booking.Fair);
                    paymentRepository.Add(NewPayment);
                }
            }
        }

        public bool IsEligibleToBook(string userId)
        {
            int count = 0;
            foreach (var payment in paymentRepository.GetList())
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
            foreach(var payment in paymentRepository.GetList())
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
            foreach(var payment in paymentRepository.GetList())
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

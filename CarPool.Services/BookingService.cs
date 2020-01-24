using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Database;
using CarPool.Model;
using CarPool.Enums;

namespace CarPool.Services
{
    public class BookingService
    {
        public Booking CreateBooking(string riderId, string rideeId, Location fromLocation, Location toLocation, int numberOfPassengers)
        {
            LocationService LocationService = new LocationService();
            double Fair = LocationService.CalculateFair(fromLocation, toLocation);
            Booking NewBooking = new Booking(riderId, rideeId, fromLocation, toLocation, numberOfPassengers, Fair);
            DataBase.Bookings.Add(NewBooking);
            return NewBooking;
        }

        public void UpdateBookingStatus(string riderId, string rideeId, IEnums.BookingStatus bookingStatus)
        {
            foreach (var booking in DataBase.Bookings)
            {
                if (booking.RideeId.Equals(rideeId) && booking.RiderId.Equals(riderId))
                {
                    booking.Status = bookingStatus;
                }
            }
        }

        public Booking ViewBookingStatus(string userId)
        {
            foreach (var booking in DataBase.Bookings)
            {
                if (booking.RideeId.Equals(userId) && booking.Status != IEnums.BookingStatus.Ended)
                {
                    return booking;
                }
            }
            return null;
        }

        public void EndRide(string riderId, string rideeId)
        {
            foreach (var booking in DataBase.Bookings)
            {
                if (booking.RiderId.Equals(riderId) && booking.RideeId.Equals(rideeId))
                {
                    PaymentService NewPaymentDue = new PaymentService();
                    NewPaymentDue.AddPaymentDue(riderId);
                    booking.Status = IEnums.BookingStatus.Ended;
                    break;
                }
            }
        }

        public void EndAllRides(string riderId)
        {
            foreach(var booking in DataBase.Bookings)
            {
                if(booking.RiderId.Equals(riderId) && booking.Status.Equals(IEnums.BookingStatus.Confirmed))
                {
                    booking.Status = IEnums.BookingStatus.Ended;
                }
            }
        }

        public bool AnyActiveBooking(string UserId)
        {
            foreach(var booking in DataBase.Bookings)
            {
                if (booking.RideeId.Equals(UserId) && booking.Status != IEnums.BookingStatus.Ended && booking.Status != IEnums.BookingStatus.Cancelled)
                    return true;
            }
            return false;
        }

        public List<Booking> DisplayBookingsHistory(string userId)
        {
            List<Booking> AllBookings = new List<Booking>();
            foreach(var booking in DataBase.Bookings)
            {
                if (booking.RideeId.Equals(userId))
                    AllBookings.Add(booking);
            }
            return AllBookings;
        }

        public List<Booking> DisplayActiveBookings(string userId)
        {
            List<Booking> AllBookings = new List<Booking>();
            foreach (var booking in DataBase.Bookings)
            {
                if (booking.RideeId.Equals(userId) && (booking.Status.Equals(IEnums.BookingStatus.Confirmed) || booking.Status.Equals(IEnums.BookingStatus.Pending)) )
                    AllBookings.Add(booking);
            }
            return AllBookings;
        }

        public void CancelRide(string riderId)
        {
            foreach(var booking in DataBase.Bookings)
            {
                if(booking.RiderId.Equals(riderId))
                {
                    booking.Status = IEnums.BookingStatus.Cancelled;
                }
            }
        }

        public List<string> DisplayPassengersInVehicle(string riderId)
        {
            List<string> PassengersInVehicle = new List<string>();
            foreach(var booking in DataBase.Bookings)
            {
                if (booking.RiderId.Equals(riderId) && booking.Status.Equals(IEnums.BookingStatus.Confirmed))
                    PassengersInVehicle.Add(booking.RideeId);
            }
            return PassengersInVehicle;
        }

    }
}

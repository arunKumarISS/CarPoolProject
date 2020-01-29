using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Model;
using CarPool.Enums;
using CarPool.Repository;

namespace CarPool.Services
{
    public class OfferService
    {
        Repository<Offer> offerRepository = new Repository<Offer>();
        Repository<Location> locationRepository = new Repository<Location>();
        public Offer CreateOffer(string driverName, string userId, Location fromLocation, Location toLocation, int availability, string vehicleRegNumber, string vehicleModel)
        {
            Offer NewOffer = new Offer(driverName, userId, fromLocation, toLocation, availability, vehicleRegNumber, vehicleModel, IEnums.OfferStatus.Active);
            offerRepository.Add(NewOffer);
            return NewOffer;
        }

        public void EndOffer(string riderId)
        {
            foreach (var offer in offerRepository.GetList())
            {
                if (offer.RiderId.Equals(riderId) && offer.Status != IEnums.OfferStatus.Ended)
                {
                    PaymentService NewPaymentDue = new PaymentService();
                    NewPaymentDue.AddPaymentDue(riderId);
                    BookingService BookingService = new BookingService();
                    BookingService.EndAllRides(riderId);
                    UpdateOfferStatus(offer, IEnums.OfferStatus.Ended);
                }
            }
        }

        public bool VehicleVerification(string vehicleNumber)
        {
            foreach (var offer in offerRepository.GetList())
            {
                if (string.Equals(vehicleNumber, offer.VehicleRegNumber) && offer.Status.Equals(IEnums.OfferStatus.Active))
                {
                    return false;
                }
            }
            return true;
        }

        public List<Offer> GetActiveOffers(Location fromLocation, Location toLocation, int numberOfPassengers)
        {
            int Count1, Count2;
            List<Offer> ActiveOffers = new List<Offer>();
            foreach(var offer in offerRepository.GetList())
            {
                Count1 = 0;
                Count2 = 0;
                foreach (var location in offer.ViaPoints)
                {
                    if (string.Equals(location.Name, fromLocation.Name))
                        Count1 = 1;
                    if (string.Equals(location.Name, toLocation.Name))
                        Count2 = 2;
                    if (Count1 == 1 && Count2 == 2)
                        ActiveOffers.Add(offer);
                }
                
            }
            return ActiveOffers;
        }

        public bool CheckAvailability(string riderId, int numberOfPassengers)
        {
            foreach (var offer in offerRepository.GetList())
            {
                if (offer.RiderId.Equals(riderId) && (offer.Status.Equals(IEnums.OfferStatus.Active) || offer.Status.Equals(IEnums.OfferStatus.RideStarted)))
                {
                    if (numberOfPassengers < offer.Availability)
                        return true;
                }
            }
            return false;
        }

        public void UpdateAvailability(string riderId, int numberOfPassengers)
        {
            foreach (var offer in offerRepository.GetList())
            {
                if (offer.RiderId.Equals(riderId) && (offer.Status.Equals(IEnums.OfferStatus.Active) || offer.Status.Equals(IEnums.OfferStatus.RideStarted)))
                {
                    offer.Availability -= numberOfPassengers;
                    if (offer.Availability == 0)
                        UpdateOfferStatus(offer, IEnums.OfferStatus.OutOfSeats);
                }
            }
        }

        public void AddViaPoint(Offer offer, string viaPoint)
        {
            Location location = locationRepository.GetById(viaPoint);
            offer.ViaPoints.Add(location);
        }

        public void UpdateOfferStatus(Offer offer, IEnums.OfferStatus status)
        {
            offer.Status = status;
        }

        public bool AnyActiveOffer(string userId)
        {
            foreach(var offer in offerRepository.GetList())
            {
                if (offer.RiderId.Equals(userId) && offer.Status != IEnums.OfferStatus.Ended)
                    return true;
            }
            return false;
        }

        public Offer GetDriverDetails(string riderId)
        {
            foreach (var offer in offerRepository.GetList())
            {
                if (offer.RiderId.Equals(riderId) && offer.Status != IEnums.OfferStatus.Ended)
                    return offer;
            }
            return null;
        }

        public List<Offer> GetOffersHistory(string userId)
        {
            List<Offer> AllOffers = new List<Offer>();
            foreach (var offer in offerRepository.GetList())
            {
                if (offer.RiderId.Equals(userId))
                    AllOffers.Add(offer);
            }
            return AllOffers;
        }

        public void CancelOffer(string userId)
        {
            foreach(var offer in offerRepository.GetList())
            {
                if (offer.RiderId.Equals(userId) && (offer.Status.Equals(IEnums.OfferStatus.Active) || offer.Status.Equals(IEnums.OfferStatus.OutOfSeats)) )
                {
                    offer.Status = IEnums.OfferStatus.Ended;
                    break;
                }
            }
        }

        public bool StartRide(string userId)
        {
            foreach(var offer in offerRepository.GetList())
            {
                if(string.Equals(offer.RiderId, userId) && offer.Status.Equals(IEnums.OfferStatus.Active))
                {
                    offer.Status = IEnums.OfferStatus.RideStarted;
                    BookingService bookingService = new BookingService();
                    bookingService.StartRide(userId);
                    return true;
                }
            }
            return false;
        }
    }
}

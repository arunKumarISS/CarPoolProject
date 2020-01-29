using System;
using System.Collections.Generic;
using System.Text;
using CarPool.Database;
using CarPool.Model;
using CarPool.Enums;
using CarPool.Repository;

namespace CarPool.Services
{
    public class OfferRequestService
    {
        Repository<OfferRequest> offerRequestRepository = new Repository<OfferRequest>();
        public void OfferRequestApproval(string requestId, IEnums.Decisions decision)
        {
            OfferRequest offerRequest = offerRequestRepository.GetById(requestId);
            BookingService NewBookingService = new BookingService();
            OfferService OfferService = new OfferService();
            if (decision == IEnums.Decisions.Accept)
            {
                offerRequest.Status = IEnums.RequestStatus.Accepted;
                NewBookingService.UpdateBookingStatus(offerRequest.RiderId, offerRequest.RideeId, IEnums.BookingStatus.Confirmed);
                OfferService.UpdateAvailability(offerRequest.RiderId, offerRequest.NumberOfPassengers);
            }
            else if (decision == IEnums.Decisions.Reject)
            {
                offerRequest.Status = IEnums.RequestStatus.Rejected;
                NewBookingService.UpdateBookingStatus(offerRequest.RiderId, offerRequest.RideeId, IEnums.BookingStatus.Cancelled);
            }
        }

        public void SendRideRequest(Location fromLocation, Location toLocation, int numberOfPassengers, string riderId, string rideeId)
        {
            OfferRequest NewOfferRequest = new OfferRequest(fromLocation, toLocation, numberOfPassengers, riderId, rideeId);
            offerRequestRepository.Add(NewOfferRequest);
        }

        public List<OfferRequest> GetOfferRequests(string riderId)
        {
            List<OfferRequest> OfferRequests = new List<OfferRequest>();
            foreach (var offerRequest in offerRequestRepository.GetList())
            {
                if (offerRequest.RiderId.Equals(riderId) && offerRequest.Status.Equals(IEnums.RequestStatus.Pending))
                {
                    OfferRequests.Add(offerRequest);
                }
            }
            return OfferRequests;
        }

        public bool AnyOfferRequest(string riderId)
        {

            foreach (var offerRequest in offerRequestRepository.GetList())
            {
                if (offerRequest.RiderId.Equals(riderId) && offerRequest.Status.Equals(IEnums.RequestStatus.Pending))
                    return true;
            }
            return false;

        }

        

    }
}

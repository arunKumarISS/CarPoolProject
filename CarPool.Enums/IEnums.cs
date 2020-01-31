using System;

namespace CarPool.Enums
{

    // Why is it called IEnums.
    //Enums need not be in another project. They are part of Models only.
    // No plurals for enum names. Because when you are using you will have only one value.
    public static class IEnums
    {
        public enum CarPoolOptions
        {
            SignUp = 1,
            SignIn = 2,
            Exit = 0
        }

        public enum UserOptions
        {
            BookARide = 1,
            OfferARide = 2,
            DisplayCurrentBookingStatus = 3,
            DisplayOfferRequests = 4,
            DisplayBookingHistory = 5,
            DisplayOfferHistory = 6,
            CancelRide = 7,
            CancelOffer = 8,
            EndRide = 9,
            EndOffer = 15,
            Pay = 10,
            AddMoneyToWallet = 11,
            DisplayPaymentDues = 12,
            WalletBalance = 13,
            DisplayPaymentHistory = 14,
            StartRide = 15,
            Logout = 0
        }

        public enum Decisions
        {
            Accept = 1,
            Reject = 2
        }

        public enum OfferStatus
        {
            Active,
            OutOfSeats,
            Cancelled,
            RideStarted,
            Ended
        }

        public enum BookingStatus
        {
            Pending,
            Confirmed,
            Cancelled,
            RideStarted,
            Ended
        }

        public enum PaymentStatus
        {
            Pending = 1,
            Paid = 2
        }

        public enum PaymentDecision
        {
            Now = 1,
            Later = 2
        }
        
        public enum YesOrNo
        {
            Yes = 1,
            No = 2
        }

        public enum RequestStatus
        {
            Accepted,
            Rejected,
            Pending
        }
        public enum LocationIndex
        {

        }
    }
}

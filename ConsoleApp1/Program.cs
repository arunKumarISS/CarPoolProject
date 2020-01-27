using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Services;
using CarPool.Model;
using CarPool.Enums;

using System.IO;

namespace CarPool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseService DatabaseService = new DatabaseService();
            UserService UserService = new UserService();
            BookingService BookingService = new BookingService();
            OfferService OfferService = new OfferService();
            OfferRequestService OfferRequestService = new OfferRequestService();
            PaymentService PaymentService = new PaymentService();
            LocationService LocationService = new LocationService();
            DatabaseService.MoveDataToJson();
            DatabaseService.GetDataFromJson();
            LocationService.AddLocation("MIYAPUR", 17.512510, 78.352226);
            LocationService.AddLocation("MADHAPUR", 17.448294, 78.391487);
            LocationService.AddLocation("KOTI", 17.385042, 78.485753);
            LocationService.AddLocation("AMEERPET", 17.437462, 78.448288);
            LocationService.AddLocation("JUBILEE HILLS", 17.426161, 78.412537);
            LocationService.AddLocation("HITECH CITY", 17.445190, 78.385117);
            LocationService.AddLocation("LINGAMPALLY", 17.487400, 78.314453);
            LocationService.AddLocation("DILSUKHNAGAR", 17.361718, 78.525805);
            LocationService.AddLocation("LB NAGAR", 17.352533, 78.555088);
            LocationService.AddLocation("BACHUPALLY", 17.526719, 78.354426);
            

            while (true)
            {
            Login:
                Console.WriteLine("1 -> SignUp\n2 -> SignIn\n0 -> Exit");

                IEnums.CarPoolOptions Option = (IEnums.CarPoolOptions)Convert.ToInt32(Console.ReadLine());
                switch (Option)
                {
                    case IEnums.CarPoolOptions.SignUp:
                        {
                            Console.WriteLine("enter name:");
                            string Name = Console.ReadLine();
                            Console.WriteLine("enter Password:");
                            string Password = Console.ReadLine();
                            User NewUser = UserService.CreateUser(Name.ToUpper(), Password);
                            Console.WriteLine("Name: " + NewUser.Name);
                            Console.WriteLine("UserId: " + NewUser.UserId);
                            Console.WriteLine("Account Created Successfully!!");
                            break;
                        }

                    case IEnums.CarPoolOptions.SignIn:
                        {
                            Console.WriteLine("enter UserId:");
                            string UserId = Console.ReadLine();
                            Console.WriteLine("enter Password:");
                            string Password = Console.ReadLine();
                            if (UserService.CheckUserCredentials(UserId, Password))
                            {

                                while (true)
                                {
                                    if (BookingService.AnyActiveBooking(UserId))
                                    {

                                        Booking Booking = BookingService.ViewBookingStatus(UserId);
                                        Console.WriteLine("Your Booking Status: " + Booking.Status);
                                        if (Booking.Status.Equals(IEnums.BookingStatus.Confirmed))
                                        {
                                            Offer Offer = OfferService.GetDriverDetails(Booking.RiderId);
                                            Console.WriteLine("Drivername: " + Offer.DriverName + ", VehicleNumber: " + Offer.VehicleRegNumber + ", VehicleModel: " + Offer.VehicleModel);
                                        }
                                        Console.ReadKey();
                                        Console.WriteLine("1 -> Book a Ride\n5 -> Display bookings history\n6 -> Display created offers history\n7 -> Cancel Ride\n10 -> Pay\n11 -> Add money to wallet\n" +
                                            "13 -> wallet Balance\n14 -> display payment History\n0 -> Logout");
                                    }
                                    else if (OfferService.AnyActiveOffer(UserId))
                                    {
                                        Console.WriteLine("4 -> Display Offer requests\n5 -> Display bookings history\n" +
                                        "6 -> Display offers history\n8 -> Cancel Offer\n9 -> End Ride\n15 -> End Offer\n10 -> Pay\n11 -> Add money to wallet\n" +
                                        "13 -> wallet Balance\n14 -> display payment History\n0 -> Logout");
                                    }
                                    else
                                    {
                                        Console.WriteLine("1 -> Book a Ride\n2 -> Offer a Ride\n5 -> Display booking history\n" +
                                        "6 -> Display offers history\n10 -> Pay\n11 -> Add money to wallet\n" +
                                        "13 -> wallet Balance\n14 -> display payment History\n0 -> Logout");
                                    }

                                    IEnums.UserOptions UserOption = (IEnums.UserOptions)Convert.ToInt32(Console.ReadLine());
                                    switch (UserOption)
                                    {
                                        case IEnums.UserOptions.BookARide:
                                            {
                                                if (PaymentService.IsEligibleToBook(UserId))
                                                {

                                                    Console.WriteLine("enter pick-up location:");
                                                    string PickUpLocation = Console.ReadLine();
                                                    Location FromLocation = LocationService.GetLocation(PickUpLocation.ToUpper());
                                                    if (FromLocation != null)
                                                    {
                                                        Console.WriteLine("enter Destination:");
                                                        string Destination = Console.ReadLine();
                                                        Location ToLocation = LocationService.GetLocation(Destination.ToUpper());
                                                        if (ToLocation != null)
                                                        {
                                                            Console.WriteLine("number of passengers:");
                                                            int NumberOfPassengers = Convert.ToInt32(Console.ReadLine());
                                                            List<Offer> ActiveOffers = OfferService.GetActiveOffers(FromLocation, ToLocation, NumberOfPassengers);
                                                            if (ActiveOffers.Count == 0)
                                                                Console.WriteLine("no active offers");
                                                            else
                                                            {
                                                                foreach (var offer in ActiveOffers)
                                                                {
                                                                    Console.WriteLine("Drivername: " + offer.DriverName + " RiderId " + offer.RiderId + " Vehicle Number: " + offer.VehicleRegNumber + " Vehicle Model: " + offer.VehicleModel + " Phone Number: " + offer.PhoneNumber);
                                                                }
                                                                Console.WriteLine("enter Riders userId to select the offer\nRider UserId:");
                                                                string RiderId = Console.ReadLine();
                                                                OfferRequestService.SendRideRequest(FromLocation, ToLocation, NumberOfPassengers, RiderId.ToUpper(), UserId.ToUpper());
                                                                Booking NewBooking = BookingService.CreateBooking(RiderId, UserId, FromLocation, ToLocation, NumberOfPassengers);
                                                            }
                                                        }
                                                        else
                                                            Console.WriteLine(Destination + " not found");
                                                    }
                                                    else
                                                        Console.WriteLine(PickUpLocation + " not found");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("pay your pending dues to book for next ride");
                                                    Console.WriteLine("do you want to pay now?");
                                                    Console.WriteLine("1 -> paynow\n2 -> later");
                                                    IEnums.PaymentDecision Decision = (IEnums.PaymentDecision)Convert.ToInt32(Console.ReadLine());
                                                    if (Decision.Equals(IEnums.PaymentDecision.Now))
                                                    {
                                                        List<Payment> PaymentDues = PaymentService.GetPendingDues(UserId);
                                                        foreach (var payment in PaymentDues)
                                                        {
                                                            Console.WriteLine("BookingId: " + payment.PaymentID + ", amount to be paid: " + payment.Fair);
                                                        }
                                                        Console.WriteLine("enter paymentId: ");
                                                        string PaymentId = Console.ReadLine();
                                                        if (PaymentService.Pay(PaymentId))
                                                        {
                                                            Console.WriteLine("payment done successfully!!");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("insufficient wallet balance");
                                                            Console.WriteLine("Do you want to add money to the wallet?");
                                                            Console.WriteLine("1 -> yes\n2 -> no");
                                                            IEnums.YesOrNo Choice = (IEnums.YesOrNo)Convert.ToInt32(Console.ReadLine());
                                                            if (Choice.Equals(IEnums.YesOrNo.Yes))
                                                            {
                                                                Console.WriteLine("enter amount: ");
                                                                double Amount = Convert.ToDouble(Console.ReadLine());
                                                                UserService.AddMoneyToWallet(Amount, UserId);
                                                                Console.WriteLine("Money added successfully!!");
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        case IEnums.UserOptions.OfferARide:
                                            {
                                                Console.WriteLine("enter name:");
                                                string Name = Console.ReadLine();
                                                Console.WriteLine("enter From location:");
                                                string FromLocation = Console.ReadLine();
                                                Location StartPoint = LocationService.GetLocation(FromLocation.ToUpper());
                                                if (StartPoint != null)
                                                {
                                                    Console.WriteLine("enter To location:");
                                                    string ToLocation = Console.ReadLine();
                                                    Location EndPoint = LocationService.GetLocation(ToLocation.ToUpper());
                                                    if (EndPoint != null)
                                                    {
                                                        Console.WriteLine("enter Availability");
                                                        int Availability = Convert.ToInt32(Console.ReadLine());
                                                        Console.WriteLine("enter Vehicle Number");
                                                        string VehicleNumber = Console.ReadLine();
                                                        Console.WriteLine("enter Vehicle Model");
                                                        string VehicleModel = Console.ReadLine();
                                                        if (OfferService.VehicleVerification(VehicleNumber))
                                                        {
                                                            Offer Offer = OfferService.CreateOffer(Name.ToUpper(), UserId, StartPoint, EndPoint, Availability, VehicleNumber.ToUpper(), VehicleModel.ToUpper());
                                                            List<Location> ViaPoints = LocationService.GetViaPoints(StartPoint, EndPoint);
                                                            if (ViaPoints.Count != 0)
                                                            {
                                                                Offer.ViaPoints.Add(StartPoint);
                                                                int SelectViaPoint = 0;
                                                                do
                                                                {
                                                                    Console.WriteLine("you might touch these locations: ");
                                                                    foreach (var location in ViaPoints)
                                                                    {
                                                                        Console.WriteLine(location.Index + " -> " + location.Name);
                                                                    }
                                                                    Console.WriteLine("0 -> end");
                                                                    Console.WriteLine("selects the locations: ");
                                                                    SelectViaPoint = Convert.ToInt32(Console.ReadLine());
                                                                    IEnums.LocationIndex LocationIndex = (IEnums.LocationIndex)SelectViaPoint;
                                                                    OfferService.AddViaPoint(Offer, LocationIndex);
                                                                } while (SelectViaPoint != 0);
                                                                Offer.ViaPoints.Add(EndPoint);
                                                                Console.WriteLine("offer created successfully!!");
                                                            }
                                                            else
                                                                Console.WriteLine("Cannot use same vehicle for two offers");
                                                        }
                                                    }
                                                    else
                                                        Console.WriteLine(ToLocation + " not found");
                                                }
                                                else
                                                    Console.WriteLine(FromLocation + " not found");
                                                break;
                                            }
                                        case IEnums.UserOptions.DisplayCurrentBookingStatus:
                                            {
                                                Booking Booking = BookingService.ViewBookingStatus(UserId);
                                                if (Booking != null)
                                                    Console.WriteLine("Rider: " + Booking.RiderId + " From " + Booking.FromLocation + " To " + Booking.ToLocation + " Status " + Booking.Status);
                                                else
                                                    Console.WriteLine("you have no current bookings");
                                                break;
                                            }
                                        case IEnums.UserOptions.DisplayOfferRequests:
                                            {
                                                if (OfferRequestService.AnyOfferRequest(UserId))
                                                {
                                                    List<OfferRequest> OfferRequests = OfferRequestService.GetOfferRequests(UserId);
                                                    foreach (var offerRequest in OfferRequests)
                                                        Console.WriteLine("RequestId: " + offerRequest.RequestId + " from " + offerRequest.FromLocation + " to " + offerRequest.ToLocation);
                                                    Console.WriteLine("enter the RequestId to accept or reject offer request");
                                                    string RequestId = Console.ReadLine();
                                                    Console.WriteLine("1 -> Accept offer\n2 -> reject offer");
                                                    IEnums.Decisions Decision = (IEnums.Decisions)Convert.ToInt32(Console.ReadLine());
                                                    OfferRequestService.OfferRequestApproval(RequestId.ToUpper(), Decision);
                                                }
                                                else
                                                    Console.WriteLine("no offers requests to display");
                                                break;
                                            }
                                        case IEnums.UserOptions.DisplayBookingHistory:
                                            {
                                                List<Booking> AllBookings = BookingService.GetBookingsHistory(UserId);
                                                if (AllBookings != null)
                                                {
                                                    foreach (var booking in AllBookings)
                                                    {
                                                        Console.WriteLine("From: " + booking.FromLocation.Name + " to " + booking.ToLocation.Name + " Status: " + booking.Status);
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("no bookings to display");
                                                }
                                                break;
                                            }
                                        case IEnums.UserOptions.DisplayOfferHistory:
                                            {
                                                List<Offer> AllOffers = OfferService.GetOffersHistory(UserId);
                                                if (AllOffers != null)
                                                {
                                                    foreach (var offer in AllOffers)
                                                    {
                                                        Console.WriteLine("From: " + offer.FromLocation.Name + " to " + offer.ToLocation.Name + " Status: " + offer.Status);
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("no offers to display");
                                                }
                                                break;
                                            }
                                        case IEnums.UserOptions.CancelRide:
                                            {
                                                List<Booking> ActiveBookings = BookingService.GetActiveBookings(UserId);
                                                if (ActiveBookings != null)
                                                {
                                                    foreach (var booking in ActiveBookings)
                                                    {
                                                        Console.WriteLine("RiderId: " + booking.RiderId + " From " + booking.FromLocation.Name + " to " + booking.ToLocation.Name + " Status: " + booking.Status);
                                                    }
                                                    Console.WriteLine("enter RiderId to cancel: ");
                                                    string RiderId = Console.ReadLine();
                                                    BookingService.CancelRide(RiderId);
                                                    Console.WriteLine("Ride Cancelled");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("no active bookings to cancel");
                                                }
                                                break;
                                            }
                                        case IEnums.UserOptions.CancelOffer:
                                            {
                                                OfferService.CancelOffer(UserId);
                                                Console.WriteLine("Offer Cancelled");
                                                break;
                                            }
                                        case IEnums.UserOptions.EndRide:
                                            {
                                                List<string> PassengersInVehicle = BookingService.GetPassengersInVehicle(UserId);
                                                foreach (var Passenger in PassengersInVehicle)
                                                {
                                                    Console.WriteLine("RideeId: " + Passenger);
                                                }
                                                Console.WriteLine("enter ridee Id");
                                                string RideeId = Console.ReadLine();
                                                BookingService.EndRide(UserId, RideeId);
                                                break;
                                            }
                                        case IEnums.UserOptions.Pay:
                                            {
                                                List<Payment> PendingPayments = PaymentService.GetPendingDues(UserId);
                                                if (PendingPayments.Count != 0)
                                                {
                                                    foreach (var payment in PendingPayments)
                                                    {
                                                        Console.WriteLine("PaymentId: " + payment.PaymentID + ", Fair: " + payment.Fair);
                                                    }
                                                    Console.WriteLine("enter paymentId: ");
                                                    string PaymentId = Console.ReadLine();
                                                    if (PaymentService.Pay(PaymentId))
                                                    {
                                                        Console.WriteLine("payment done successfully!!");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("insufficient wallet balance");
                                                        Console.WriteLine("Do you want to add money to the wallet?");
                                                        Console.WriteLine("1 -> yes\n2 -> no");
                                                        IEnums.YesOrNo Choice = (IEnums.YesOrNo)Convert.ToInt32(Console.ReadLine());
                                                        if (Choice.Equals(IEnums.YesOrNo.Yes))
                                                        {
                                                            Console.WriteLine("enter amount: ");
                                                            double Amount = Convert.ToDouble(Console.ReadLine());
                                                            UserService.AddMoneyToWallet(Amount, UserId);
                                                            Console.WriteLine("Money added successfully!!");
                                                        }
                                                    }
                                                }
                                                else
                                                    Console.WriteLine("no pending dues");
                                                break;
                                            }
                                        case IEnums.UserOptions.AddMoneyToWallet:
                                            {
                                                Console.WriteLine("enter amount: ");
                                                double Amount = Convert.ToDouble(Console.ReadLine());
                                                UserService.AddMoneyToWallet(Amount, UserId);
                                                Console.WriteLine("Money added successfully!!");
                                                break;
                                            }

                                        case IEnums.UserOptions.WalletBalance:
                                            {
                                                Console.WriteLine("your wallet balance: " + UserService.GetWalletBalance(UserId));
                                                break;
                                            }
                                        case IEnums.UserOptions.DisplayPaymentHistory:
                                            {
                                                List<Payment> AllPayments = PaymentService.GetPaymentHistory(UserId);
                                                if (AllPayments.Count != 0)
                                                {
                                                    foreach (var payment in AllPayments)
                                                    {
                                                        if (payment.RideeId.Equals(UserId))
                                                        {
                                                            Console.WriteLine("PaymentId: " + payment.PaymentID + " to " + payment.RiderId + " Status: " + payment.Status);
                                                        }
                                                        else if (payment.RiderId.Equals(UserId))
                                                        {
                                                            Console.WriteLine("PaymentId: " + payment.PaymentID + " from " + payment.RideeId + " Status: " + payment.Status);
                                                        }
                                                    }
                                                }
                                                else
                                                    Console.WriteLine("you have no payments to display");
                                                break;
                                            }
                                        case IEnums.UserOptions.Logout:
                                            {
                                                goto Login;

                                            }
                                    }
                                }
                            }
                            else
                                Console.WriteLine("Incorrect userId or password");
                            break;
                        }

                    case IEnums.CarPoolOptions.Exit:
                        {
                            
                            System.Environment.Exit(0);
                            break;
                        }
                }
                
                DatabaseService.MoveDataToJson();
            }
           
        }
    }
}

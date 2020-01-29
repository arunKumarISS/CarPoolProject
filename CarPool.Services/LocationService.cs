using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Model;
using CarPool.Database;
using CarPool.Repository;

namespace CarPool.Services
{
    public class LocationService
    {
        public Location GetLocation(string locationName)
        {
            foreach (var location in Repository<Location>.GetList())
            {
                if (location.Name.Equals(locationName))
                    return location;
            }
            return null;
        }

        public List<Location> GetViaPoints(Location startPoint, Location endPoint)
        {
            List<Location> ViaPoints = new List<Location>();
            foreach (var location in Repository<Location>.GetList())
            {
                if (location.Equals(startPoint) || location.Equals(endPoint))
                    continue;
                if (((location.Latitude < endPoint.Latitude && location.Latitude > startPoint.Latitude) || (location.Latitude > endPoint.Latitude && location.Latitude < startPoint.Latitude)) &&
                    ((location.Longitude < endPoint.Longitude && location.Longitude > startPoint.Longitude) || (location.Longitude > endPoint.Longitude && location.Longitude < startPoint.Longitude)))
                {
                    ViaPoints.Add(location);
                }
            }
            return ViaPoints;
        }

        public double CalculateFair(Location fromLocation, Location toLocation)
        {
            double Distance = 0;
            double Latitude1 = fromLocation.Latitude;
            double Latitude2 = toLocation.Latitude;
            double Longitude1 = fromLocation.Latitude;
            double Longitude2 = toLocation.Longitude;
            double dLat = (Latitude2 - Latitude1) / 180 * Math.PI;
            double dLong = (Longitude2 - Longitude1) / 180 * Math.PI;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                        + Math.Cos(Latitude2) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double radiusE = 6378135;
            double radiusP = 6356750;
            double nr = Math.Pow(radiusE * radiusP * Math.Cos(Latitude1 / 180 * Math.PI), 2);
            double dr = Math.Pow(radiusE * Math.Cos(Latitude1 / 180 * Math.PI), 2)
                            + Math.Pow(radiusP * Math.Sin(Latitude1 / 180 * Math.PI), 2);
            double radius = Math.Sqrt(nr / dr);
            Distance = radius * c;
            return Distance;
        }

        public void AddLocation(string name, double latitude, double longitude)
        {
            Location NewLocation = new Location(name, latitude, longitude);
            Repository<Location>.Add(NewLocation);
        }
    }
}

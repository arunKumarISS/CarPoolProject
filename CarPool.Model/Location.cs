using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Enums;

namespace CarPool.Model
{
    public class Location 
    {
        public string Name;
        public double Latitude;
        public double Longitude;
        public IEnums.LocationIndex Index;

        public Location(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

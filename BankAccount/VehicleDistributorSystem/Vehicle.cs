using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDistributorSystem
{

    public class Vehicle
    {
        public string Model { get; set; }
        public int Price { get; set; }
    }

    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }
    }

    public class Bike : Vehicle
    {
        public bool HasSidecar { get; set; }
    }
}
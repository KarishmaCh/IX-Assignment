using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDistributorSystem
{
    // Encapsulation: Define a Distributor class to encapsulate distributor data
    // Vehicle type enum
    public class Distributor
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public Type VehicleType { get; set; }
        public int Margin { get; set; }
    }
}
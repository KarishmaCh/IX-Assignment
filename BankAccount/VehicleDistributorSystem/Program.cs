using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDistributorSystem
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            List<Distributor> distributors = new List<Distributor>();

            // Get data for three vehicles
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Enter data for vehicle {i + 1}:");

                Console.Write("Model: ");
                string model = Console.ReadLine();

                Console.Write("Price: ");
                int price = int.Parse(Console.ReadLine());

                Console.Write("Type (1 for Car, 2 for Bike): ");
                int type = int.Parse(Console.ReadLine());

                Vehicle vehicle;
                if (type == 1)
                {
                    Console.Write("Number of doors: ");
                    int numberOfDoors = int.Parse(Console.ReadLine());

                    vehicle = new Car() { Model = model, Price = price, NumberOfDoors = numberOfDoors };
                }
                else
                {
                    Console.Write("Has sidecar (true or false): ");
                    bool hasSidecar = bool.Parse(Console.ReadLine());

                    vehicle = new Bike() { Model = model, Price = price, HasSidecar = hasSidecar };
                }

                vehicles.Add(vehicle);
            }

            // Get data for three distributors
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Enter data for distributor {i + 1}:");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Company: ");
                string company = Console.ReadLine();

                Console.Write("Vehicle type (1 for Car, 2 for Bike): ");
                int vehicleType = int.Parse(Console.ReadLine());

                Console.Write("Margin: ");
                int margin = int.Parse(Console.ReadLine());

                Type type = vehicleType == 1 ? typeof(Car) : typeof(Bike);

                distributors.Add(new Distributor() { Name = name, Company = company, VehicleType = type, Margin = margin });
            }

            // Find distributor with the best margin for selected vehicle
            Console.Write("Enter the model name of the vehicle you want to buy: ");
            string modelToBuy = Console.ReadLine();

            Vehicle selectedVehicle = vehicles.Find(v => v.Model == modelToBuy);

            if (selectedVehicle == null)
            {
                Console.WriteLine($"No vehicle with model name {modelToBuy} found.");
                return;
            }

            Distributor bestMarginDistributor = GetBestMarginDistributor(selectedVehicle, distributors);

            Console.WriteLine($"The best margin distributor for {modelToBuy} is {bestMarginDistributor.Name} from {bestMarginDistributor.Company} with a margin of {bestMarginDistributor.Margin}.");
            Console.ReadKey();
        }

        static Distributor GetBestMarginDistributor(Vehicle vehicle, List<Distributor> distributors)
        {
            List<Distributor> matchingDistributors = distributors.FindAll(d => d.VehicleType == vehicle.GetType());

            Distributor bestMarginDistributor = null;

            foreach (Distributor distributor in matchingDistributors)
            {
                if (bestMarginDistributor == null || distributor.Margin < bestMarginDistributor.Margin)
                {
                    bestMarginDistributor = distributor;
                }
            }

            return bestMarginDistributor;
        }


    }
}










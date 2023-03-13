using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("----------- Welcome Employee  -----------");
                Console.Write("Enter employee name: ");
                string name;
                do
                {
                    name = Console.ReadLine();
                } while (string.IsNullOrEmpty(name));

                Console.Write("Enter employee age: ");
                int age;
                while (!int.TryParse(Console.ReadLine(), out age) || age < 1 || age > 100)
                {
                    Console.Write("Invalid input. Please enter a valid age: ");
                }

                Console.Write("Enter employee salary: ");
                int salary;
                while (!int.TryParse(Console.ReadLine(), out salary))
                {
                    Console.Write("Invalid input. Please enter a valid salary: ");
                }

                Console.Write("Enter employee phone number (10 digits): ");
                long phone;
                while (!long.TryParse(Console.ReadLine(), out phone) || phone.ToString().Length != 10)
                {
                    Console.Write("Invalid input. Please enter a valid phone number: ");
                }

                Console.Write("Enter employee ID (1-1000): ");
                int empId;
                while (!int.TryParse(Console.ReadLine(), out empId) || empId < 1 || empId > 1000)
                {
                    Console.Write("Invalid input. Please enter a valid employee ID: ");
                }

                Employee emp = new Employee(name, age, salary, phone, empId);
                emp.DisplayEmployee();
                Console.ReadLine();

            }
        }
    }
}


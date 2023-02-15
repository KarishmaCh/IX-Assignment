using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem
{
    class Employee
    {

        private string name;
        private int age;
        private int salary;
        private long phone;
        private int empId;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public long Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        public Employee(string name, int age, int salary, long phone, int empId)
        {
            this.name = name;
            this.age = age;
            this.salary = salary;
            this.phone = phone;
            this.empId = empId;
        }

        public void DisplayEmployee()
        {
            Console.WriteLine("----------- Employee Information-----------");

            Console.WriteLine("Name: " + name);
            Console.WriteLine("Age: " + age);
            Console.WriteLine("Salary: " + salary);
            Console.WriteLine("Phone: " + phone);
            Console.WriteLine("Employee ID: " + empId);
            Console.ReadLine();
        }
    }
}

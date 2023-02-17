using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Account_1
{
    class Bank_Account_1
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the bank account program!");

            // Ask for user name
            string userName;
            do
            {
                Console.Write("Enter your name: ");
                userName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(userName));

            // Ask for birthdate
            DateTime birthdate;
            do
            {
                Console.Write("Enter your birthdate (YYYY-MM-DD): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out birthdate));

            string permanentAddress;
            string communicationAddress;
            Console.Write("Enter your permanent address: ");
            permanentAddress = Console.ReadLine();

            string sameAddress;
            do
            {
                Console.Write("Is your communication address the same as your permanent address? (Y/N): ");
                sameAddress = Console.ReadLine().ToUpper();
            } while (sameAddress != "Y" && sameAddress != "N");

            if (sameAddress == "Y")
            {
                communicationAddress = permanentAddress;
            }
            else
            {
                Console.Write("Enter your communication address: ");
                communicationAddress = Console.ReadLine();
            }

            // Ask for phone number
            string phoneNumber;
            do
            {
                Console.Write("Please enter your phone number (10 digits): ");
                phoneNumber = Console.ReadLine();
            } while (phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit));

            // Ask for gender
            string gender;
            do
            {
                Console.Write("Enter your gender (M/F): ");
                gender = Console.ReadLine().ToUpper();
            } while (gender != "M" && gender != "F");

            // Ask for marital status
            string maritalStatus;
            do
            {
                Console.Write("Enter your marital status (S/M): ");
                maritalStatus = Console.ReadLine().ToUpper();
            } while (maritalStatus != "S" && maritalStatus != "M");

            string spouseName = "";
            if (maritalStatus == "M")
            {
                Console.Write("Enter your spouse's name: ");
                spouseName = Console.ReadLine();
            }

            int numChildren = 0;
            if (maritalStatus == "M")
            {
                do
                {
                    Console.Write("Enter the number of children: ");
                } while (!int.TryParse(Console.ReadLine(), out numChildren));
            }

            string[] childrenNames = new string[numChildren];
            for (int i = 0; i < numChildren; i++)
            {
                Console.Write($"Enter the name of child {i + 1}: ");
                childrenNames[i] = Console.ReadLine();
            }

            // Ask for account type
            string accountType;
            do
            {
                Console.Write("Enter account type (Savings/Current): ");
                accountType = Console.ReadLine().ToLower();
            } while (accountType != "savings" && accountType != "current");

            string creditCard = "";
            double income = 0;
            if (accountType == "current" || accountType == "savings")
            {
                do
                {
                    Console.Write("Do you want a credit card? (Y/N): ");
                    creditCard = Console.ReadLine().ToUpper();

                } while (creditCard != "Y" && creditCard != "N");

                if (creditCard == "Y")
                {
                    do
                    {
                        Console.Write("Enter your income: ");
                    } while (!double.TryParse(Console.ReadLine(), out income));
                   
                }
            }

            Random random = new Random();
            string cardNumber = "";

            for (int i = 0; i < 16; i++)
            {
                cardNumber += random.Next(0, 10);
            }

            // Generate account number
            Random rnd = new Random();
            string accountNumber = rnd.Next(100000000, 999999999).ToString();
          

            // Display all information
            Console.WriteLine();
            Console.WriteLine("------------User Information------------");
            Console.WriteLine($"Name: {userName}");
            Console.WriteLine($"Birthdate: {birthdate.ToShortDateString()}");
            Console.WriteLine($"Phone Number: {phoneNumber}");

            Console.WriteLine($"Permanent Address: {permanentAddress}");
            Console.WriteLine($"Communication Address: {communicationAddress}");
            Console.WriteLine($"Gender: {gender}");
            Console.WriteLine($"Marital Status: {maritalStatus}");
            if (maritalStatus == "M")
            {
                Console.WriteLine($"Spouse's Name: {spouseName}");
                Console.WriteLine($"Number of Children: {numChildren}");
                for (int i = 0; i < numChildren; i++)
                {
                    Console.WriteLine($"Child {i + 1}: {childrenNames[i]}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("------------Bank Account Information------------");
            Console.WriteLine($"Account Type: {accountType}");
            if (creditCard == "Y")
            {
                string creditCardType = income > 50000 ? "Platinum" : "Gold";
                Console.WriteLine($"Credit Card: {creditCardType}");
                Console.WriteLine($"Account number: {accountNumber}");
                Console.WriteLine($"Card number: {cardNumber}");

                Console.ReadKey();
            }

            Console.ReadKey();

        }
    }
}


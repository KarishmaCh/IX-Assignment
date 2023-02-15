using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class BankAccount
    {
        private string userName;
        private DateTime birthdate;
        private string phoneNumber;
        private string gender;
        private string maritalStatus;
        private string spouseName;
        public int numChildren;
        private string[] childrenNames;
        private string accountType;
        private string creditCard;
        private double income;
        private string accountNumber;


        private void GetUserName()
        {
            do
            {
                Console.Write("Enter your name: ");
                userName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(userName));
        }

        protected void GetBirthdate()
        {
            do
            {
                Console.Write("Enter your birthdate (YYYY-MM-DD): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out birthdate));
        }

        private void GetPhoneNumber()
        {
            do
            {
                Console.Write("Please enter your phone number (10 digits): ");
                phoneNumber = Console.ReadLine();
            } while (phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit));
        }

        internal void GetGender()
        {
            do
            {
                Console.Write("Enter your gender (M/F): ");
                gender = Console.ReadLine().ToUpper();
            } while (gender != "M" && gender != "F");
        }

        protected internal void GetMaritalStatus()
        {
            do
            {
                Console.Write("Enter your marital status (S/M): ");
                maritalStatus = Console.ReadLine().ToUpper();
            } while (maritalStatus != "S" && maritalStatus != "M");
        }

        private void GetSpouseName()
        {
            if (maritalStatus == "M")
            {
                Console.Write("Enter your spouse's name: ");
                spouseName = Console.ReadLine();
            }
        }
        private int GetNumChildren()
        {
            int numChildren = 0;
            if (maritalStatus == "M")
            {
                do
                {
                    Console.Write("Enter the number of children: ");
                } while (!int.TryParse(Console.ReadLine(), out numChildren));
            }
            return numChildren;
        }
        private string[] GetChildrenNames(int numChildren)
        {
            string[] childrenNames = new string[numChildren];
            for (int i = 0; i < numChildren; i++)
            {
                Console.Write($"Enter the name of child {i + 1}: ");
                childrenNames[i] = Console.ReadLine();
            }
            return childrenNames;
        }


        public void DisplayUserInfo()
        {
            Console.WriteLine();
            Console.WriteLine("-----------User Information------------------");
            Console.WriteLine($"Name: {userName}");
            Console.WriteLine($"Birthdate: {birthdate.ToShortDateString()}");
            Console.WriteLine($"Phone Number: {phoneNumber}");
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


        }

        public void GetAccountType()
        {

            do
            {
                Console.Write("Enter account type (Savings/Current): ");
                accountType = Console.ReadLine().ToLower();
            } while (accountType != "savings" && accountType != "current");

        }

        public void GetCreditCard()
        {
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
                    } while (!double.TryParse(Console.ReadLine(), out _));
                }
            }

        }

        public void DisplayBankInfo()
        {
            Console.WriteLine();
            Console.WriteLine("-------------Bank Account Information------------");
            Console.WriteLine($"Account Type: {accountType}");
            if (creditCard == "Y")
            {
                string creditCardType = income > 50000 ? "Platinum" : "Gold";
                Console.WriteLine($"Credit Card: {creditCardType}");
            }
            Console.ReadLine();
        }



        public void RunBankAccountProgram()
        {
            Console.WriteLine("Welcome to the bank account program!");

            GetUserName();
            GetBirthdate();
            GetPhoneNumber();
            GetGender();
            GetMaritalStatus();
            GetSpouseName();
            if (maritalStatus == "M")
            {
                numChildren = GetNumChildren();
                childrenNames = GetChildrenNames(numChildren);
            }
            GetAccountType();
            GetCreditCard();
            DisplayUserInfo();
            DisplayBankInfo();
            Console.WriteLine("Thank you for using the bank account program!");
        }
    }
}










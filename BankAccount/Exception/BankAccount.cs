using System;

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
    public void GetAccountNumber()
    {
        Random rnd = new Random();
        string accountNumber = rnd.Next(100000000, 999999999).ToString();
        Console.WriteLine($"Account number: {accountNumber}");

    }

    public void DepositWithdraw()
    {
        decimal balance = 0;
        decimal dailyDepositLimit = 100000;
        decimal dailyWithdrawLimit = 50000;
        decimal totalDeposits = 0;
        decimal totalWithdrawals = 0;
        DateTime lastDepositDate = DateTime.Today.AddDays(-1);
        DateTime lastWithdrawDate = DateTime.Today.AddDays(-1);

        while (true)
        {
            Console.WriteLine("Enter 1 to deposit, 2 to withdraw, or 3 to quit:");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    try
                    {
                        Console.WriteLine("Enter deposit amount:");
                        decimal amount = decimal.Parse(Console.ReadLine());
                        if (amount <= 0)
                        {
                            throw new ArgumentException("Amount must be greater than zero.");
                        }
                        if (totalDeposits + amount > dailyDepositLimit)
                        {
                            throw new InvalidOperationException("You have exceeded the daily deposit limit.");
                        }
                        if (DateTime.Today > lastDepositDate)
                        {
                            totalDeposits = 0;
                            lastDepositDate = DateTime.Today;
                        }
                        balance += amount;
                        totalDeposits += amount;
                        Console.WriteLine($"Deposit of {amount:C} successful. New balance: {balance:C}");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input format.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "2":
                    try
                    {
                        Console.WriteLine("Enter withdrawal amount:");
                        decimal amount = decimal.Parse(Console.ReadLine());
                        if (amount <= 0)
                        {
                            throw new ArgumentException("Amount must be greater than zero.");
                        }
                        if (totalWithdrawals + amount > dailyWithdrawLimit)
                        {
                            throw new InvalidOperationException("You have exceeded the daily withdrawal limit.");
                        }
                        if (DateTime.Today > lastWithdrawDate)
                        {
                            totalWithdrawals = 0;
                            lastWithdrawDate = DateTime.Today;
                        }
                        if (balance < amount)
                        {
                            throw new InvalidOperationException("Insufficient funds.");
                        }
                        balance -= amount;
                        totalWithdrawals += amount;
                        Console.WriteLine($"Withdrawal of {amount:C} successful. New balance: {balance:C}");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input format.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
            Console.WriteLine();
        }
    }



    public void DisplayBankInfo()
    {
        Console.WriteLine();
        Console.WriteLine("-------------Bank Account Information------------");
        Console.WriteLine($"Account Type: {accountType}");
        GetAccountNumber();
        if (creditCard == "Y")
        {
            string creditCardType = income > 50000 ? "Platinum" : "Gold";
            Console.WriteLine($"Credit Card: {creditCardType}");
        }
        DepositWithdraw();
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

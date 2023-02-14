using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount bankAccountProgram = new BankAccount();
            bankAccountProgram.RunBankAccountProgram();
            Console.ReadLine();
        }
    }
}

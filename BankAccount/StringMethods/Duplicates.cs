using System;

class Duplicates

{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter a string: ");
            string inputString = Console.ReadLine();

            string result = string.Empty;
            foreach (char c in inputString)
            {
                if (result.IndexOf(c) == -1)
                {
                    result += c;
                }
            }

            Console.WriteLine("After removing duplicates: " + result);

            Console.Write("Do you want to continue? (y/n): ");
            string response = Console.ReadLine().ToLower();

            if (response != "y")
            {
                break;
            }
        }
    }
}

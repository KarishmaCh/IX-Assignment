using System;

class Reverse

{
    static void Main()
    {
        while (true)
        {
            Console.Write("Enter a string: ");
            string input = Console.ReadLine();
            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                char[] chars = words[i].ToCharArray();
                Array.Reverse(chars);
                words[i] = new string(chars);
            }
            string output = string.Join(" ", words);
            Console.WriteLine(output);
            Console.Write("Do you want to continue? (y/n): ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "n")
            {
                break;
            }
        }
    }
}

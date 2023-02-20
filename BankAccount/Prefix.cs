using System;

public class Prefix
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Enter a list of strings separated by spaces:");
            string input = Console.ReadLine();
            char[] delimiters = { ' ', '\t' };
            string[] strs = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string prefix = LongestCommonPrefix(strs);
            if (prefix == "")
            {
                Console.WriteLine("Not found");
            }
            else
            {
                Console.WriteLine("Longest common prefix: " + prefix);
            }

            Console.WriteLine("Press 'Enter' to continue, or type 'exit' to exit.");
            string response = Console.ReadLine();
            if (response.ToLower() == "exit")
            {
                break;
            }
        }
        Console.ReadKey();
    }

    public static string LongestCommonPrefix(string[] strs)
    {
        if (strs == null || strs.Length == 0)
        {
            return "";
        }

        string prefix = strs[0];

        for (int i = 1; i < strs.Length; i++)
        {
            while (strs[i].IndexOf(prefix) != 0)
            {
                prefix = prefix.Substring(0, prefix.Length - 1);
                if (prefix.Length == 0)
                {
                    return "";
                }
            }
        }

        return prefix;
    }
}



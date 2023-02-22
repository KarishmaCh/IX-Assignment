using System;

public class Class1
{
    static void Main()
    {
        do
        {
            Console.Write("Enter a string: ");
            string input = Console.ReadLine();

            Console.WriteLine($"Original string: {input}");

            Console.Write("Enter a string method to perform (Length, ToUpper, ToLower, Substring, Replace, Split, Trim, IndexOf): ");
            string method = Console.ReadLine();

            switch (method.ToLower())
            {
                case "length":
                    Console.WriteLine($"Length: {input.Length}");
                    break;
                case "toupper":
                    Console.WriteLine($"Uppercase: {input.ToUpper()}");
                    break;
                case "tolower":
                    Console.WriteLine($"Lowercase: {input.ToLower()}");
                    break;
                case "substring":
                    Console.Write("Enter a starting index: ");
                    int start = int.Parse(Console.ReadLine());
                    Console.Write("Enter a length: ");
                    int length = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Substring({start}, {length}): {input.Substring(start, length)}");
                    break;
                case "replace":
                    Console.Write("Enter a string to replace: ");
                    string oldStr = Console.ReadLine();
                    Console.Write("Enter a replacement string: ");
                    string newStr = Console.ReadLine();
                    Console.WriteLine($"Replace(\"{oldStr}\", \"{newStr}\"): {input.Replace(oldStr, newStr)}");
                    break;
                case "split":
                    Console.Write("Enter a delimiter: ");
                    string delimiterStr = Console.ReadLine();
                    char[] delimiter = delimiterStr.ToCharArray();
                    string[] split = input.Split(delimiter);
                    Console.Write("Split:");
                    foreach (string s in split)
                    {
                        Console.Write($" \"{s}\"");
                    }
                    Console.WriteLine();
                    break;
                case "trim":
                    Console.WriteLine($"Trim: \"{input.Trim()}\"");
                    break;
                case "indexof":
                    Console.Write("Enter a search string: ");
                    string search = Console.ReadLine();
                    int index = input.IndexOf(search);
                    if (index >= 0)
                    {
                        Console.WriteLine($"IndexOf(\"{search}\"): {index}");
                    }
                    else
                    {
                        Console.WriteLine($"IndexOf(\"{search}\"): not found");
                    }
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Invalid string method.");
                    break;
            }

            Console.Write("Do you want to perform another operation? (yes or no): ");
        } while (Console.ReadLine().ToLower() == "yes");
    }
}



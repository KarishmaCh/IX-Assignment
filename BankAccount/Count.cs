using System;

class Count

{
    static void Main(string[] args)
    {
        Console.Write("Enter a paragraph: ");
        string input = Console.ReadLine();

        int lineCount = CountLines(input);
        int statementCount = CountStatements(input);

        Console.WriteLine($"Number of lines: {lineCount}");
        Console.WriteLine($"Number of statements: {statementCount}");
        Console.ReadKey();
    }

    static int CountLines(string text)
    {
        string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        return lines.Length;
    }

    static int CountStatements(string text)
    {
        string[] statements = text.Split(new[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);
        return statements.Length;
    }

}
    


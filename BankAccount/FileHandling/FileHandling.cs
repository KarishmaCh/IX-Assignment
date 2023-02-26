
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

   public  class FileHandling
  {
      public  static void Main(string[] args)
    {

        Console.WriteLine("Welcome to the File Handling");
        Console.WriteLine("You can upload files with the following extensions: png, txt, xlsx, and jpg.");

        List<string> validExtensions = new List<string> { ".png", ".txt", ".xlsx", ".jpg" };
        List<string> validFiles = new List<string>();

        while (true)
        {
            Console.Write("Enter the file path to upload or enter 'done' to stop: ");
            string filePath = Console.ReadLine();

            if (filePath.ToLower() == "done")
            {
                break;
            }

            string extension = Path.GetExtension(filePath);

            if (!validExtensions.Contains(extension))
            {
                Console.WriteLine("Invalid file extension. Please upload a file with a valid extension.");
                continue;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist. Please enter a valid file path.");
                continue;
            }

            if (validFiles.Contains(filePath))
            {
                Console.WriteLine("File already uploaded. Please select a different file.");
                continue;
            }

            validFiles.Add(filePath);
            Console.WriteLine("File uploaded successfully!");
        }

        if (validFiles.Count == 0)
        {
            Console.WriteLine("No valid files uploaded. Exiting the program...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("\nValid files uploaded:");
        for (int i = 0; i < validFiles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {validFiles[i]}");
        }

        while (true)
        {
            Console.WriteLine("\nPlease select the file to write:");
            for (int i = 0; i < validFiles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {validFiles[i]}");
            }

            int selectedFileIndex;
            while (true)
            {
                Console.Write("Enter the file number to write: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out selectedFileIndex) || selectedFileIndex < 1 || selectedFileIndex > validFiles.Count)
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to a valid file.");
                    continue;
                }

                string selectedFilePath = validFiles[selectedFileIndex - 1];
                string selectedFileExtension = Path.GetExtension(selectedFilePath);

                if (selectedFileExtension == ".png" || selectedFileExtension == ".jpg")
                {
                    Console.WriteLine("Cannot write to an image file. Please select a text file to write to.");
                    continue;
                }

                Console.WriteLine($"\nYou selected {selectedFilePath}.");
                break;
            }

            Console.Write("Enter text to write to the file: ");
            string text = Console.ReadLine();

            try
            {
                using (StreamWriter writer = new StreamWriter(validFiles[selectedFileIndex - 1], true))
                {
                    writer.WriteLine(text);
                    Console.WriteLine("Text written to file successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }

            while (true)
            {
                Console.Write("Do you want to edit another file? (yes/no): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "yes")
                {
                    break;
                }
                else if (input.ToLower() == "no")
                {
                    Console.WriteLine("Exiting the program...");
                    Environment.Exit(0);
                    Console.ReadLine();


                }
            }
        }
    }
} 





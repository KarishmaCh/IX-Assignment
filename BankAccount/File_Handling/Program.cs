using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Handling
{
    internal class Program
    {
        
            static void Main(string[] args)
            {
            string sourceFileName = "";
            string targetFileName = "";

            do
            {
                Console.WriteLine("Enter the source file path");
                sourceFileName = Console.ReadLine();

                Console.WriteLine("Enter the target file path");
                targetFileName = Console.ReadLine();

                // Check if source file exists
                if (!File.Exists(sourceFileName))
                {
                    Console.WriteLine("Source file does not exist. Please enter a valid file path.");
                    continue;
                }

               

                break; // Exit loop if valid file paths are entered

            } while (true);





            try
            {
                //Copy data from one file to another
                File.Copy(sourceFileName, targetFileName);

                //Create new file at run time by accepting location and file name from user.
                string dirPath = Environment.CurrentDirectory;
                    string fileName = "myNewFile";
                    
                    File.Create(Path.Combine(dirPath, fileName)).Dispose(); //create & close

                //Read all the text from file, Display it to user and ask user to provide a word 
                //That exists in the file and then replace it at all places
                string text = File.ReadAllText(targetFileName);
                Console.WriteLine(text);
                Console.WriteLine("File Copy Operation Completed!");
                string wordToBeReplaced = "";
                Console.WriteLine("Which word you want to replace?");
                wordToBeReplaced = Console.ReadLine();
                string newWordToBeReplaced = "";
                Console.WriteLine("New Word To Be Replaced");
                newWordToBeReplaced = Console.ReadLine();

                // Surround the word to be replaced with spaces to make sure it is a whole word
                string searchWord = " " + wordToBeReplaced + " ";
                // Surround the new word with spaces to make sure it is a whole word
                string replaceWord = " " + newWordToBeReplaced + " ";

                // Replace all occurrences of the word with the new word
                text = text.Replace(searchWord, replaceWord);

                File.WriteAllText(targetFileName, text);



               
                //Read last line of file and display it to user on console along with name of the file.
                string filename = targetFileName;

                    string lastLine = File.ReadLines(filename).Last();
                    Console.WriteLine("\nFile Name: " + filename + "\nLast Line: " + lastLine);
                Console.ReadKey();
            }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Unable to locate the file : " + ex.Message);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine("The directory was not found :" + ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("Unauthorized Access : " + ex.Message);
                }
              
            Console.ReadKey();
        }
        }
    }

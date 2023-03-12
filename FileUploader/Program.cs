using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter the paths of the files to upload (separated by commas): ");
            string input = Console.ReadLine();

            Console.Write("Enter the destination folder to upload the files to: ");
            string destinationFolder = Console.ReadLine();

            List<string> files = new List<string>(input.Split(','));

            Console.WriteLine($"Uploading {files.Count} files to {destinationFolder}...");

            List<Task<string>> tasks = new List<Task<string>>();

            foreach (string file in files)
            {
                tasks.Add(UploadFile(file.Trim(), destinationFolder));
            }

            string[] results = await Task.WhenAll(tasks);

            Dictionary<string, long> times = new Dictionary<string, long>();

            for (int i = 0; i < files.Count; i++)
            {
                string fileName = Path.GetFileName(files[i].Trim());
                times[fileName] = long.Parse(results[i]);
            }

            Array.Sort(results, files.ToArray());

            Console.WriteLine("Files uploaded in the following order (fastest to slowest):");

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file.Trim());
                Console.WriteLine($"{fileName} ({times[fileName]} ms)");
            }
        }

        static async Task<string> UploadFile(string filePath, string destinationFolder)
        {
            string fileName = Path.GetFileName(filePath);
            string destinationPath = Path.Combine(destinationFolder, fileName);

            try
            {
                if (File.Exists(destinationPath))
                {
                    Console.Write($"A file with the name '{fileName}' already exists in the destination folder. Do you want to overwrite it? (y/n): ");
                    string answer = Console.ReadLine().Trim().ToLower();

                    if (answer != "y")
                    {
                        int i = 1;
                        while (true)
                        {
                            string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)} ({i}){Path.GetExtension(fileName)}";
                            destinationPath = Path.Combine(destinationFolder, newFileName);
                            if (!File.Exists(destinationPath))
                            {
                                Console.WriteLine($"Uploading file {fileName} as {newFileName}...");
                                break;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Uploading file {fileName}...");
                    }
                }
                else
                {
                    Console.WriteLine($"Uploading file {fileName}...");
                }

                var watch = System.Diagnostics.Stopwatch.StartNew();

                using (FileStream sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                    }
                }

                watch.Stop();
                long elapsedMs = watch.ElapsedMilliseconds;
                return elapsedMs.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading {fileName}: {ex.Message}");
                return "0";
            }

        }


    }
}

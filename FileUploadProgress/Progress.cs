using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadProgress
{
    internal class Progress
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter source file path: ");
                string sourcePath = Console.ReadLine();
                if (!File.Exists(sourcePath))
                {
                    Console.WriteLine("Source file does not exist!");
                    return;
                }

                Console.WriteLine("Enter destination file path: ");
                string destinationPath = Console.ReadLine();
                if (File.Exists(destinationPath))
                {
                    Console.WriteLine("Destination file already exists. Do you want to overwrite it? (Y/N)");
                    string answer = Console.ReadLine();
                    if (answer.ToUpper() != "Y")
                    {
                        return;
                    }
                }

                FileInfo fileInfo = new FileInfo(sourcePath);
                long fileSize = fileInfo.Length;

                using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                    {
                        byte[] buffer = new byte[1024 * 1024];
                        int bytesSent = 0;
                        int bytesRead;

                        while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await destinationStream.WriteAsync(buffer, 0, bytesRead);

                            bytesSent += bytesRead;
                            double percentage = (double)bytesSent / fileSize * 100;
                            Console.WriteLine($"File upload progress: {percentage.ToString("0.00")}%");
                        }

                        Console.WriteLine("File upload completed successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}


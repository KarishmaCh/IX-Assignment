namespace FileHandling.Model
{
    public class FileInputModel
    {
        public string? DirectoryName { get; set; } //Directory name where file needs to be saved
        public string? FileName { get; set; } //The name of the uploaded file
        public IFormFile? FileContent { get; set; } //Actual file content
    }
}

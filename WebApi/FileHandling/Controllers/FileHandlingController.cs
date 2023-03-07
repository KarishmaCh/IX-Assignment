using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileHandlingController : ControllerBase
    {
        private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

        [HttpGet("directory/{name}")]
        public IActionResult CreateDirectory(string name)
        {
            try
            {
                if (!Directory.Exists(_basePath))
                    Directory.CreateDirectory(_basePath);

                if (Directory.Exists(Path.Combine(_basePath, name)))
                    return BadRequest("Directory already exists!");

                Directory.CreateDirectory(Path.Combine(_basePath, name));
                return Ok("Directory created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("file/{directoryName}")]
        public IActionResult UploadFile(string directoryName, IFormFile file)
        {
            try
            {
                long fileSizeLimit = 10 * 1024 * 1024; // 10MB
                var allowedExtensions = new[] { ".txt", ".xls", ".xlsx", ".pdf", ".png", ".jpeg", ".jpg" };

                var fileExtension = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension.ToLower()))
                    return BadRequest($"Only these types of files are allowed: {string.Join(", ", allowedExtensions)}");

                if (file.Length < fileSizeLimit)
                    return BadRequest($"File size should be greater than 10MB!");

                if (!Directory.Exists(Path.Combine(_basePath, directoryName)))
                    return BadRequest($"Directory with name {directoryName} does not exist!");

                var filePath = Path.Combine(_basePath, directoryName, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok($"File uploaded successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("directory/{name}")]
        public IActionResult DeleteDirectory(string name)
        {
            try
            {
                var fullPath = Path.Combine(_basePath, name);

                if (!Directory.Exists(fullPath))
                    return BadRequest($"Directory with name {name} does not exist!");

                var files = Directory.GetFiles(fullPath);
                if (files.Length > 0)
                {
                    return BadRequest($"Directory must be empty before deleting! Files count: {files.Length}");
                }

                Directory.Delete(fullPath, true);

                return Ok($"Directory deleted successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

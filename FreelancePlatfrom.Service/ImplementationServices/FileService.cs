using FreelancePlatfrom.Service.AbstractionServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FreelancePlatform.Service.ImplementationServices
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(string location, IFormFile file)
        {
            var relativePath = $"/{location}/";
            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, location);
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{extension}";

            if (file.Length > 0)
            {
                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }
                var fullPath = Path.Combine(absolutePath, fileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }
                return $"{relativePath}{fileName}";
            }
            else
            {
                throw new Exception("File is empty");
            }
        }
    }
}
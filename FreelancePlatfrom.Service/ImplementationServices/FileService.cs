//using FreelancePlatfrom.Service.AbstractionServices;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.IO;
//using System.Threading.Tasks;

//namespace FreelancePlatform.Service.ImplementationServices
//{
//    public class FileService : IFileService
//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public FileService(IWebHostEnvironment webHostEnvironment)
//        {
//            _webHostEnvironment = webHostEnvironment;
//        }

//        public async Task<string> UploadFileAsync(string location, IFormFile file)
//        {
//            var relativePath = $"/{location}/";
//            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, location);
//            var extension = Path.GetExtension(file.FileName);
//            var fileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{extension}";

//            if (file.Length > 0)
//            {
//                if (!Directory.Exists(absolutePath))
//                {
//                    Directory.CreateDirectory(absolutePath);
//                }
//                var fullPath = Path.Combine(absolutePath, fileName);
//                using (var fileStream = new FileStream(fullPath, FileMode.Create))
//                {
//                    await file.CopyToAsync(fileStream);
//                    await fileStream.FlushAsync();
//                }
//                return $"{relativePath}{fileName}";
//            }
//            else
//            {
//                throw new Exception("File is empty");
//            }
//        }
//        public async Task<string> UploadFileAsync2(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                throw new Exception("File is empty");

//            var extension = Path.GetExtension(file.FileName).ToLower();
//            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
//            var allowedVideoExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv" };

//            string folder = allowedImageExtensions.Contains(extension) ? "Uploads/Images"
//                          : allowedVideoExtensions.Contains(extension) ? "Uploads/Videos"
//                          : throw new Exception("Unsupported file type");

//            long maxFileSize = allowedImageExtensions.Contains(extension) ? 5 * 1024 * 1024 : 50 * 1024 * 1024;
//            if (file.Length > maxFileSize)
//                throw new Exception($"File is too large. Max size allowed is {maxFileSize / (1024 * 1024)} MB");

//            var fileName = $"{Guid.NewGuid():N}{extension}";
//            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, folder);
//            var relativePath = $"/{folder}/{fileName}";

//            Directory.CreateDirectory(absolutePath); // Safe even if exists
//            var fullPath = Path.Combine(absolutePath, fileName);

//            using var stream = new FileStream(fullPath, FileMode.Create);
//            await file.CopyToAsync(stream);
//            await stream.FlushAsync();

//            return relativePath.Replace("\\", "/");
//        }

//    }
//}

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
        public async Task<string> UploadFileAsync2(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("File is empty");

            var extension = Path.GetExtension(file.FileName).ToLower();
            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var allowedVideoExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv" };

            string folder = allowedImageExtensions.Contains(extension) ? "Uploads/Images"
                          : allowedVideoExtensions.Contains(extension) ? "Uploads/Videos"
                          : throw new Exception("Unsupported file type");

            long maxFileSize = allowedImageExtensions.Contains(extension) ? 5 * 1024 * 1024 : 50 * 1024 * 1024;
            if (file.Length > maxFileSize)
                throw new Exception($"File is too large. Max size allowed is {maxFileSize / (1024 * 1024)} MB");

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, folder);
            var relativePath = $"/{folder}/{fileName}";

            Directory.CreateDirectory(absolutePath); // Safe even if exists
            var fullPath = Path.Combine(absolutePath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();

            return relativePath.Replace("\\", "/");
        }

    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Service.AbstractionServices
{
    public interface IFileService
    {
        /// <summary>
        /// Uploads a file to the specified directory and returns the file path.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <param name="directory">The directory to upload the file to.</param>
        /// <returns>The path of the uploaded file.</returns>
        Task<string> UploadFileAsync(string location , IFormFile file);
        Task<string> UploadFileAsync2(IFormFile file);


    }
}

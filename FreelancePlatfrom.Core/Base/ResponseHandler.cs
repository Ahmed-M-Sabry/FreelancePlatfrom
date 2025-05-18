using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Base
{

    public class ResponseHandler
    {

        public ResponseHandler()
        {

        }

        public ApiResponse<T> ValidationFailed<T>(List<string> errors)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = "Validation failed",
                Errors = errors
            };
        }

        public ApiResponse<T> Deleted<T>(string Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully" ?? Message
            };
        }

        public ApiResponse<T> Success<T>(T entity, object Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Get Successfully",
                Meta = Meta
            };
        }

        public ApiResponse<T> Unauthorized<T>()
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized"
            };
        }

        public ApiResponse<T> BadRequest<T>(string Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }

        public ApiResponse<T> UnprocessableEntity<T>(string Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? "Can't Add" : Message
            };
        }

        public ApiResponse<T> NotFound<T>(string message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }

        public ApiResponse<T> Created<T>(T entity, object Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created",
                Meta = Meta
            };
        }

        public ApiResponse<T> NoContent<T>(string message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Succeeded = false,
                Message = message == null ? "No Content" : message
            };
        }
    }

}

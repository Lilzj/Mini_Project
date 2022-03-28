using Newtonsoft.Json;

namespace Onboarding.DTOs.Response
{
    public class BaseResponseDto<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public BaseResponseDto(int statusCode, bool success, string msg, T data)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
        }
        public BaseResponseDto()
        {
        }

        /// <summary>
        /// Sets the data to the appropriate response
        /// at run time
        /// </summary>
        public static BaseResponseDto<T> Fail(string errorMessage, int statusCode = 404)
        {
            return new BaseResponseDto<T> { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static BaseResponseDto<T> Success(string successMessage, T data, int statusCode = 200)
        {
            return new BaseResponseDto<T> { Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

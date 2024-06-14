namespace ColorPaletteGeneratorApi.Models
{
    public class ApiException : Exception
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public ApiException(string errorMessage)
        {
            ErrorMessage = errorMessage;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ApiException(string errorMessage, int statusCode)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public ApiException()
        {
        }

    }
}

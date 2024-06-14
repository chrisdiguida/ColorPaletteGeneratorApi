namespace ColorPaletteGeneratorApi.Models
{
    public class ApiException : Exception
    {
        public string ErrorMessage { get; set; }
        public string InternalErrorMessage { get; set; }
        public Guid? AppUserRequesterId { get; set; }
        public int StatusCode { get; set; }

        public ApiException(string errorMessage)
        {
            ErrorMessage = errorMessage;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ApiException(string errorMessage, Guid appUserRequesterId)
        {
            ErrorMessage = errorMessage;
            AppUserRequesterId = appUserRequesterId;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ApiException(string errorMessage, Guid appUserRequesterId, int statusCode)
        {
            ErrorMessage = errorMessage;
            AppUserRequesterId = appUserRequesterId;
            StatusCode = statusCode;
        }

        public ApiException(string errorMessage, int statusCode)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public ApiException(string errorMessage, string internalErrorMessage, int statusCode)
        {
            ErrorMessage = errorMessage;
            InternalErrorMessage = internalErrorMessage;
            StatusCode = statusCode;
        }

        public ApiException(string errorMessage, string internalErrorMessage)
        {
            ErrorMessage = errorMessage;
            InternalErrorMessage = internalErrorMessage;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ApiException(string errorMessage, string internalErrorMessage, Guid appUserRequesterId)
        {
            ErrorMessage = errorMessage;
            InternalErrorMessage = internalErrorMessage;
            AppUserRequesterId = appUserRequesterId;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public ApiException()
        {
        }

    }
}

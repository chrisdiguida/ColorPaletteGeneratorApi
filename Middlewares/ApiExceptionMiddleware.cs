using AutoMapper;
using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Models;
using System.Text.Json;

namespace ColorPaletteGeneratorApi.Middlewares
{
    public class ApiExceptionMiddleware(RequestDelegate next, IMapper mapper, ILogger<ApiExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ApiExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                ApiExceptionDto apiExceptionDto;
                string errorMessage;

                switch (ex)
                {
                    case ApiException apiEx:
                        apiExceptionDto = _mapper.Map<ApiExceptionDto>(apiEx);
                        errorMessage = apiEx.ErrorMessage;
                        break;
                    default:
                        apiExceptionDto = new ApiExceptionDto
                        {
                            StatusCode = StatusCodes.Status500InternalServerError,
                            ErrorMessage = "An error has occurred."
                        };
                        errorMessage = ex.GetBaseException().Message;
                        break;
                }

                errorMessage = $"An error has occurred while executing the HTTP request. Error: {errorMessage} Stack Trace: {ex.StackTrace}";
                _logger.LogError(errorMessage);

                httpContext.Response.StatusCode = apiExceptionDto.StatusCode;
                httpContext.Response.ContentType = "application/json";
                string json = JsonSerializer.Serialize(apiExceptionDto);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}

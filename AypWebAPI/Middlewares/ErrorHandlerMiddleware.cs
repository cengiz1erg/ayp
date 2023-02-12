using AypWebAPI.Models.Exceptions;
using System.Net;
using System.Text.Json;

namespace AypWebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseMessage = "";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseMessage = e?.Message;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseMessage = e?.Message;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseMessage = "Custom Internal Situation";
                        break;
                }

                var result = JsonSerializer.Serialize(new { 
                    status = response.StatusCode,
                    //message = error?.Message 
                    message =  responseMessage
                });
                await response.WriteAsync(result);
            }
        }
    }
}

using System.Net;
using System.Text.Json;
using TransitSystem.Core.Domain.Exceptions;

namespace TransitSystem.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // Por defecto: 500
            var message = "Ocurrió un error inesperado en el servidor.";

            // Mapeo de nuestras Excepciones de Dominio a códigos HTTP
            if (exception is InsufficientFundsException || exception is CardExpiredException)
            {
                code = HttpStatusCode.BadRequest; // 400
                message = exception.Message;
            }
            else if (exception is InvalidCardTypeException)
            {
                code = HttpStatusCode.UnprocessableEntity; // 422
                message = exception.Message;
            }
            else if (exception is ArgumentException)
            {
                code = HttpStatusCode.BadRequest; // 400
                message = exception.Message;
            }

            var result = JsonSerializer.Serialize(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
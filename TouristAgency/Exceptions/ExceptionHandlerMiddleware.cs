using System.Net;
using System.Text.Json;

namespace TouristAgency.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Nastavi sa sledećim middleware-om
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                var e when e.GetType().IsGenericType &&
                           e.GetType().GetGenericTypeDefinition() == typeof(EntityAlreadyExistsException<>)
                    => (int)HttpStatusCode.Conflict, // HTTP 409 - Conflict

                var e when e.GetType().IsGenericType &&
                           e.GetType().GetGenericTypeDefinition() == typeof(EntityNotFoundException<>)
                    => (int)HttpStatusCode.NotFound, // HTTP 404 - Not Found

                var e when e.GetType().IsGenericType &&
                           e.GetType().GetGenericTypeDefinition() == typeof(EntityInsertException<>)
                    => (int)HttpStatusCode.BadRequest, // HTTP 400 - Bad Request

                var e when e.GetType().IsGenericType &&
                           e.GetType().GetGenericTypeDefinition() == typeof(DataRetrievalException<>)
                    => (int)HttpStatusCode.InternalServerError, // HTTP 500 - Internal Server Error

                var e when e.GetType().IsGenericType &&
                           e.GetType().GetGenericTypeDefinition() == typeof(SingleEntityRetrievalException<>)
                    => (int)HttpStatusCode.NotFound, // HTTP 404 - Not Found

                var e when e is PasswordsNotMatchingException
                    => (int)HttpStatusCode.BadRequest, // HTTP 400 - Bad Request (jer korisnik pogrešno unosi podatke)

                var e when e is InvalidUserIdException // Dodajemo novi case za InvalidUserIdException
             => (int)HttpStatusCode.NotFound,

                _ => (int)HttpStatusCode.InternalServerError // HTTP 500 - Internal Server Error
            };

            var response = new
            {
                StatusCode = statusCode,
                Message = ex.Message
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

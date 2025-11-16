using System.Net;

namespace api_biblioteca.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Tenta executar a proxima requisição
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Define o status da resposta HTTP
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode;
            string errorMessage;

            // Traduz o tipo de erro C# para um código HTTP
            switch (exception)
            { 
                case BusinessRuleException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = exception.Message;
                    break;
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = exception.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorMessage = exception.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorMessage = "Ocorreu um erro inesperado.";
                    break;
            }

            // Monta a resposta JSON
            context.Response.StatusCode = (int)statusCode;
            var result = System.Text.Json.JsonSerializer.Serialize(new { error = errorMessage });

            return context.Response.WriteAsync(result);
        }
    }
}

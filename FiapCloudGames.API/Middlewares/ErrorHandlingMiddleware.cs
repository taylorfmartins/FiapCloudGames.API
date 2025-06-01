using System.Net;
using System.Text.Json;
using System.Diagnostics;

namespace FiapCloudGames.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation(
                    "Iniciando requisição: {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                await _next(context);

                sw.Stop();
                _logger.LogInformation(
                    "Requisição finalizada: {Method} {Path} - Status: {StatusCode} - Tempo: {ElapsedMilliseconds}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    sw.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                sw.Stop();
                _logger.LogError(e,
                    "Erro na requisição: {Method} {Path} - Tempo: {ElapsedMilliseconds}ms",
                    context.Request.Method,
                    context.Request.Path,
                    sw.ElapsedMilliseconds);
                
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new 
            { 
                error = "Ocorreu uma exceção não tratada.",
                message = exception.Message,
                type = exception.GetType().Name
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

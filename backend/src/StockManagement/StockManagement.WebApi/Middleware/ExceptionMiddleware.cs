using FluentValidation;
using StockManagement.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace StockManagement.WebApi.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                await ReturnCodeWithLogging(ex, context, (int)HttpStatusCode.NotFound);
            }
            catch (NotEnoughMoneyException ex)
            {
                await ReturnCodeWithLogging(ex, context, (int)HttpStatusCode.BadRequest);
            }
            catch (ValidationException ex)
            {
                await ReturnCodeWithLogging(ex, context, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await ReturnCodeWithLogging(ex, context, (int)HttpStatusCode.InternalServerError);
            }
        }

        private async Task ReturnCodeWithLogging<T>(T ex, HttpContext context, int statusCode) where T : Exception
        {
            var endpoint = context.GetEndpoint()?.DisplayName;

            logger.LogError(ex, "Exception in {displayName}", endpoint);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await using var writer = new StreamWriter(context.Response.Body);
            await writer.WriteLineAsync(JsonSerializer.Serialize(ex.Message));
        }
    }
}

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PiRiS.Business.Exceptions;

namespace PiRiS.Api.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private static readonly ActionDescriptor _emptyActionDescriptor = new();
        private static readonly RouteData _emptyRouteData = new();

        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly ILogger<ErrorWrappingMiddleware> _logger;

        public ErrorWrappingMiddleware(
            RequestDelegate next,
            IActionResultExecutor<ObjectResult> executor,
            ILogger<ErrorWrappingMiddleware> logger)
        {
            _next = next;
            _executor = executor;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (ServiceException ex)
            {
                await WriteProblemDetails(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                try
                {
                    _logger.LogWarning($"{ex}. TraceId: {context.TraceIdentifier}");

                    await WriteProblemDetails(context, StatusCodes.Status500InternalServerError, "Internal error");

                    return;
                }
                catch (Exception inner)
                {
                    _logger.LogError(inner, "Unexpected error.");
                }

                throw;
            }
        }

        private Task WriteProblemDetails(HttpContext context, int statusCode, string? message = null)
        {
            var routeData = context.GetRouteData() ?? _emptyRouteData;

            var actionContext = new ActionContext(context, routeData, _emptyActionDescriptor);

            var result = new ObjectResult(message)
            {
                StatusCode =  statusCode
            };

            result.ContentTypes.Add("application/problem+json");
            result.ContentTypes.Add("application/problem+xml");

            return _executor.ExecuteAsync(actionContext, result);
        }

        

        
    }
}

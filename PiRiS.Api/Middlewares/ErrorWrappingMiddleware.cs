using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

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
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                try
                {
                    _logger.LogWarning($"{ex}. TraceId: {context.TraceIdentifier}");

                    await WriteProblemDetails(context);

                    return;
                }
                catch (Exception inner)
                {
                    _logger.LogError(inner, "Unexpected error.");
                }

                throw;
            }
        }

        private Task WriteProblemDetails(HttpContext context)
        {
            var routeData = context.GetRouteData() ?? _emptyRouteData;

            var actionContext = new ActionContext(context, routeData, _emptyActionDescriptor);

            var result = new ObjectResult(context.Response)
            {
                StatusCode =  context.Response.StatusCode,
            };

            result.ContentTypes.Add("application/problem+json");
            result.ContentTypes.Add("application/problem+xml");

            return _executor.ExecuteAsync(actionContext, result);
        }

        

        
    }
}

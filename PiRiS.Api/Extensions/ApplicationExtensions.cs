using PiRiS.Api.Middlewares;

namespace PiRiS.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<ErrorWrappingMiddleware>();
        }
    }
}

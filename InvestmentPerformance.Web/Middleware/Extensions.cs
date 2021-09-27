using Microsoft.AspNetCore.Builder;

namespace InvestmentPerformance.Web.Middleware
{
    public static class Extensions
    {
        public static void AddGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
using AccomodationBookingMVC.Middleware;

namespace AccomodationBookingMVC.Extensions
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DbInitializerMiddleware>();
        }
    }
}

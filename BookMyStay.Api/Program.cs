using BookMyStay.Api.Common;

namespace BookMyStay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // init static app config
            Helper.AppConfig = new AppConfig(builder.Configuration);

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}

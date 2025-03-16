using ProductRating.Data.Configurations;

namespace ProductRating.WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            builder.Services.Configure<MainControllerOptions>(builder.Configuration.GetSection("Configurations:MainController"));
            builder.Services.Configure<RecognitionControllerOptions>(builder.Configuration.GetSection("Configurations:RecognitionController"));

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Main/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Main}/{action=Main}/{id?}");

            app.Run();
        }
    }
}
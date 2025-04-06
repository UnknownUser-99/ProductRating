using ProductRating.Contracts.Authorization;
using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.Configurations;
using ProductRating.Services.Authorization;
using ProductRating.Services.HttpRequest;

namespace ProductRating.WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            builder.Services.Configure<CookieServiceOptions>(builder.Configuration.GetSection("Configurations:CookieService"));
            builder.Services.Configure<AuthorizationControllerOptions>(builder.Configuration.GetSection("Configurations:AuthorizationController"));
            builder.Services.Configure<MainControllerOptions>(builder.Configuration.GetSection("Configurations:MainController"));
            builder.Services.Configure<RecognitionControllerOptions>(builder.Configuration.GetSection("Configurations:RecognitionController"));

            builder.Services.AddScoped<ICookieService, CookieService>();

            builder.Services.AddScoped<AuthTokenHandler>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<IAuthRequestService, AuthRequestService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7066/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            });
            builder.Services.AddHttpClient<IProductRecognitionRequestService, ProductRecognitionRequestService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7066/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenHandler>();

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

            app.MapControllerRoute(
                name: "registration",
                pattern: "registration",
                defaults: new { controller = "Registration", action = "Registration" });

            app.Run();
        }
    }
}
using ProductRating.Contracts.Authorization;
using ProductRating.Contracts.HttpRequest;
using ProductRating.Contracts.ModelFactory;
using ProductRating.Data.Configurations;
using ProductRating.Services.Authorization;
using ProductRating.Services.HttpRequest;
using ProductRating.Services.ModelFactory;

namespace ProductRating.WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            builder.Services.Configure<UrlAPIOptions>(builder.Configuration.GetSection("Configurations:UrlAPI"));
            builder.Services.Configure<CookieServiceOptions>(builder.Configuration.GetSection("Configurations:CookieService"));
            builder.Services.Configure<RecognitionControllerOptions>(builder.Configuration.GetSection("Configurations:RecognitionController"));

            builder.Services.AddScoped<ICookieService, CookieService>();

            builder.Services.AddScoped<AuthTokenHandler>();

            builder.Services.AddSingleton<IRecognitionModelService, RecognitionModelService>();
            builder.Services.AddSingleton<IProductModelService, ProductModelService>();
            builder.Services.AddSingleton<IReviewModelService, ReviewModelService>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            var urlApi = builder.Configuration
                .GetSection("Configurations:UrlAPI")
                .Get<UrlAPIOptions>();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient<IAuthRequestService, AuthRequestService>(client =>
            {
                client.BaseAddress = new Uri(urlApi.AuthRequest);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .ConfigurePrimaryHttpMessageHandler(() => handler);

            builder.Services.AddHttpClient<IProductRecognitionRequestService, ProductRecognitionRequestService>(client =>
            {
                client.BaseAddress = new Uri(urlApi.ProductRecognitionRequest);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(() => handler);

            builder.Services.AddHttpClient<IProductRequestService, ProductRequestService>(client =>
            {
                client.BaseAddress = new Uri(urlApi.ProductRequest);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(() => handler);

            builder.Services.AddHttpClient<IReviewRequestService, ReviewRequestService>(client =>
            {
                client.BaseAddress = new Uri(urlApi.ReviewRequest);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(() => handler);

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
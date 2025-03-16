using ProductRating.Contracts.Authorization;
using ProductRating.Contracts.Database;
using ProductRating.Contracts.DTO;
using ProductRating.Contracts.ProductRecognition;
using ProductRating.Data.Configurations;
using ProductRating.Data.Entities.Database;
using ProductRating.Services.Authorization;
using ProductRating.Services.Database;
using ProductRating.Services.DTO;
using ProductRating.Services.ProductRecognition;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductRating.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ProductRecognitionServiceOptions>(builder.Configuration.GetSection("Configurations:ProductRecognitionService"));
            builder.Services.Configure<HashServiceOptions>(builder.Configuration.GetSection("Configurations:HashService"));
            builder.Services.Configure<JWTServiceOptions>(builder.Configuration.GetSection("Configurations:JWTService"));
            builder.Services.Configure<AuthControllerOptions>(builder.Configuration.GetSection("Configurations:AuthController"));

            builder.Services.AddDbContext<PRDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRecognitionService, ProductRecognitionService>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddSingleton<IHashService, HashService>();
            builder.Services.AddSingleton<IProductRecognitionDTOService, ProductRecognitionDTOService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
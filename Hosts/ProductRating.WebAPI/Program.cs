using ProductRating.Contracts.Authorization;
using ProductRating.Contracts.Database;
using ProductRating.Contracts.DTO;
using ProductRating.Contracts.ProductRecognition;
using ProductRating.Data.Configurations;
using ProductRating.Data.Database;
using ProductRating.Services.Authorization;
using ProductRating.Services.Database;
using ProductRating.Services.DTO;
using ProductRating.Services.ProductRecognition;
using ProductRating.WebAPI.Filters;
using ProductRating.WebAPI.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ProductRating.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("https://localhost:7066");

            builder.Services.Configure<ProductRecognitionServiceOptions>(builder.Configuration.GetSection("Configurations:ProductRecognitionService"));
            builder.Services.Configure<HashServiceOptions>(builder.Configuration.GetSection("Configurations:HashService"));
            builder.Services.Configure<JWTServiceOptions>(builder.Configuration.GetSection("Configurations:JWTService"));
            builder.Services.Configure<AuthControllerOptions>(builder.Configuration.GetSection("Configurations:AuthController"));
            builder.Services.Configure<RegistrationFilterOptions>(builder.Configuration.GetSection("Configurations:RegistrationFilter"));
            builder.Services.Configure<AuthorizationFilterOptions>(builder.Configuration.GetSection("Configurations:AuthorizationFilter"));
            builder.Services.Configure<AddReviewFilterOptions>(builder.Configuration.GetSection("Configurations:AddReviewFilter"));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddDbContext<PRDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductRecognitionService, ProductRecognitionService>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IRecognitionHistoryService, RecognitionHistoryService>();

            builder.Services.AddScoped<RegistrationFilter>();
            builder.Services.AddScoped<AuthorizationFilter>();
            builder.Services.AddScoped<VerificationFilter>();
            builder.Services.AddScoped<AddReviewFilter>();

            builder.Services.AddSingleton<IHashService, HashService>();
            builder.Services.AddSingleton<IProductRecognitionDTOService, ProductRecognitionDTOService>();
            builder.Services.AddSingleton<IProductDTOService, ProductDTOService>();
            builder.Services.AddSingleton<IReviewDTOService, ReviewDTOService>();
            builder.Services.AddSingleton<IAuthDTOService, AuthDTOService>();
            builder.Services.AddSingleton<IErrorDTOService, ErrorDTOService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "¬ведите Token."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            var app = builder.Build();

            app.UseMiddleware<AuthMiddleware>();

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
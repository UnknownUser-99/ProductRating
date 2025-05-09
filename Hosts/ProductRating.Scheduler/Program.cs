using ProductRating.Contracts.HttpRequest;
using ProductRating.Contracts.UpdateRating;
using ProductRating.Data.Configurations;
using ProductRating.Services.HttpRequest;
using ProductRating.Services.UpdateRating;
using ProductRating.Scheduler.Jobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using Quartz.Spi;

namespace ProductRating.Scheduler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.Configure<AuthTokenSchedulerHandlerOptions>(builder.Configuration.GetSection("Configurations:AuthTokenSchedulerHandler"));

            builder.Services.AddScoped<AuthTokenSchedulerHandler>();

            builder.Services.AddSingleton<IRatingCalculatorService, RatingCalculatorService>();
            builder.Services.AddSingleton<IUpdateRatingService, UpdateRatingService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient<IReviewRequestService, ReviewRequestService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7066/api/Review/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenSchedulerHandler>();

            builder.Services.AddHttpClient<IProductRatingRequestService, ProductRatingRequestService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7066/api/ProductRating/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenSchedulerHandler>();

            builder.Services.AddTransient<UpdateInitialRatingJob>();

            builder.Services.AddSingleton<IJobFactory, MicrosoftDependencyInjectionJobFactory>();
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            builder.Services.AddQuartz(q =>
            {
                var updateInitialRatingJobKey = new JobKey("UpdateInitialRatingJob");
                var updateInitialRatingTriggerKey = new TriggerKey("UpdateInitialRatingTrigger");

                q.AddJob<UpdateInitialRatingJob>(updateInitialRatingJobKey, j => j
                    .WithIdentity(updateInitialRatingJobKey)
                    .StoreDurably()
                    .WithDescription("Update initial rating")
                );

                q.AddTrigger(t => t
                    .WithIdentity(updateInitialRatingTriggerKey)
                    .ForJob(updateInitialRatingJobKey)
                    .StartNow()
                );
            });

            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            var app = builder.Build();

            await app.RunAsync();
        }
    }
}
using ProductRating.Contracts.HttpRequest;
using ProductRating.Contracts.UpdateRating;
using ProductRating.Data.Configurations;
using ProductRating.Services.HttpRequest;
using ProductRating.Services.UpdateRating;
using ProductRating.Scheduler.Jobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
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

            var urlApi = builder.Configuration
                .GetSection("Configurations:UrlAPIScheduler")
                .Get<UrlAPISchedulerOptions>();

            builder.Services.AddHttpClient<IReviewRequestService, ReviewRequestService>(client =>
            {
                client.BaseAddress = new Uri(urlApi.ReviewRequest);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenSchedulerHandler>();

            builder.Services.AddHttpClient<IProductRatingRequestService, ProductRatingRequestService>(client =>
            {
                client.BaseAddress = new Uri(urlApi.ProductRatingRequest);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddHttpMessageHandler<AuthTokenSchedulerHandler>();

            builder.Services.AddSingleton<IJobFactory, MicrosoftDependencyInjectionJobFactory>();
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            builder.Services.AddTransient<UpdateInitialRatingJob>();
            builder.Services.AddTransient<UpdateOverallRatingJob>();
            builder.Services.AddTransient<UpdateYearlyRatingJob>();
            builder.Services.AddTransient<UpdateMonthlyRatingJob>();

            builder.Services.AddQuartz(q =>
            {
                var updateInitialRatingJobKey = new JobKey("UpdateInitialRatingJob");
                var updateOverallRatingJobKey = new JobKey("UpdateOverallRatingJob");
                var updateYearlyRatingJobKey = new JobKey("UpdateYearlyRatingJob");
                var updateMonthlyRatingJobKey = new JobKey("UpdateMonthlyRatingJob");

                var updateInitialRatingTriggerKey = new TriggerKey("UpdateInitialRatingTrigger");
                var updateOverallRatingTriggerKey = new TriggerKey("UpdateOverallRatingTrigger");
                var updateYearlyRatingTriggerKey = new TriggerKey("UpdateYearlyRatingTrigger");
                var updateMonthlyRatingTriggerKey = new TriggerKey("UpdateMonthlyRatingTrigger");

                q.AddJob<UpdateInitialRatingJob>(updateInitialRatingJobKey, j => j
                    .WithIdentity(updateInitialRatingJobKey)
                    .StoreDurably()
                    .WithDescription("Update initial rating")
                );
                q.AddJob<UpdateOverallRatingJob>(updateOverallRatingJobKey, j => j
                    .WithIdentity(updateOverallRatingJobKey)
                    .StoreDurably()
                    .WithDescription("Update overall rating")
                );
                q.AddJob<UpdateYearlyRatingJob>(updateYearlyRatingJobKey, j => j
                    .WithIdentity(updateYearlyRatingJobKey)
                    .StoreDurably()
                    .WithDescription("Update yearly rating")
                );
                q.AddJob<UpdateMonthlyRatingJob>(updateMonthlyRatingJobKey, j => j
                    .WithIdentity(updateMonthlyRatingJobKey)
                    .StoreDurably()
                    .WithDescription("Update monthly rating")
                );

                q.AddTrigger(t => t
                    .WithIdentity(updateInitialRatingTriggerKey)
                    .ForJob(updateInitialRatingJobKey)
                    .StartNow()
                );
                q.AddTrigger(t => t
                    .WithIdentity(updateOverallRatingTriggerKey)
                    .ForJob(updateOverallRatingJobKey)
                    .WithCronSchedule("0 0 1 * * ? *")
                );
                q.AddTrigger(t => t
                    .WithIdentity(updateYearlyRatingTriggerKey)
                    .ForJob(updateYearlyRatingJobKey)
                    .WithCronSchedule("0 0 1 1 1 ? *")
                );
                q.AddTrigger(t => t
                    .WithIdentity(updateMonthlyRatingTriggerKey)
                    .ForJob(updateMonthlyRatingJobKey)
                    .WithCronSchedule("0 0 1 1 * ? *")
                );
            });

            builder.Services.AddQuartzHostedService(q =>
            {
                q.WaitForJobsToComplete = true;
            });

            var app = builder.Build();

            await app.RunAsync();
        }
    }
}
using ProductRating.Contracts.UpdateRating;
using ProductRating.Data.UpdateRating;
using Quartz;

namespace ProductRating.Scheduler.Jobs
{
    public class UpdateMonthlyRatingJob : IJob
    {
        private readonly IUpdateRatingService _updateRatingService;

        public UpdateMonthlyRatingJob(IUpdateRatingService updateRatingService)
        {
            _updateRatingService = updateRatingService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var result = await _updateRatingService.UpdateRatingAsync(UpdateRatingType.MonthlyRating);

            await Task.CompletedTask;
        }
    }
}
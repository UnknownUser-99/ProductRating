using ProductRating.Contracts.UpdateRating;
using ProductRating.Data.UpdateRating;
using Quartz;

namespace ProductRating.Scheduler.Jobs
{
    public class UpdateOverallRatingJob : IJob
    {
        private readonly IUpdateRatingService _updateRatingService;

        public UpdateOverallRatingJob(IUpdateRatingService updateRatingService)
        {
            _updateRatingService = updateRatingService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var result = await _updateRatingService.UpdateRatingAsync(UpdateRatingType.OverallRating);

            await Task.CompletedTask;
        }
    }
}
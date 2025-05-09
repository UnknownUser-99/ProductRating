using ProductRating.Contracts.UpdateRating;
using ProductRating.Data.UpdateRating;
using Quartz;

namespace ProductRating.Scheduler.Jobs
{
    public class UpdateInitialRatingJob : IJob
    {
        private readonly IUpdateRatingService _updateRatingService;

        public UpdateInitialRatingJob(IUpdateRatingService updateRatingService)
        {
            _updateRatingService = updateRatingService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var result = await _updateRatingService.UpdateRatingAsync(UpdateRatingType.InitialRating);

            await Task.CompletedTask;
        }
    }
}
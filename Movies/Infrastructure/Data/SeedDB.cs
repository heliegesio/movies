using MediatR;
using Movies.Application.Commands.ProducerCommands.Request;

namespace Movies.Infrastructure.Data
{
    public class SeedDB
    {
        private readonly MoviesDbContext _dbContext;
        private readonly IMediator _mediator;

        public SeedDB(
            MoviesDbContext dbContext,
            IMediator mediator
            )
        {
            _dbContext = dbContext;
            _mediator = mediator;

        }

        public async Task Seed()
        {
            await SeedTransactionDevelopment();
        }

        private async Task SeedTransactionDevelopment()
        {


            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await SeedDataDevelopment();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        private async Task SeedDataDevelopment()
        {
            #region Producer

            for (int i = 0; i < 20; i++)
            {
                var producerRequest = new CreateProducerRequest()
                {
                    Name = DataGenerator.GenerateRandomName(),
                    FollowingWin = DataGenerator.GenerateRandomNumber(10, 40),
                    Interval = DataGenerator.GenerateRandomNumber(1, 10),
                    PreviousWin = DataGenerator.GenerateRandomNumber(3, 10),
                };

                await _mediator.Send(producerRequest);
            }

            #endregion

        }

    }
}

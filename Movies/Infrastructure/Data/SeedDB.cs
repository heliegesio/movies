using AutoMapper;
using MediatR;
using Movies.Application.Commands.ProducerCommands.Request;
using Movies.Infrastructure.Repositories;

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

            var producerRequest = new CreateProducerRequest()
            {
                Name = NameGenerator.GenerateRandomName(),
                FollowingWin = 1,
                Interval = 2,
                PreviousWin = 3
            };

            await _mediator.Send(producerRequest);

            #endregion

        }


    }
}

using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;

namespace Movies.Infrastructure.Data
{
    public class SeedDB
    {
        private readonly MoviesDbContext _dbContext;
        private readonly IProducerRepository _repositoryProducer;


       

        public SeedDB(
            MoviesDbContext dbContext,
            IProducerRepository repositoryProducer)
        {
            _dbContext = dbContext;
            _repositoryProducer = repositoryProducer;
           

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



                await _dbContext.CommitAsync();
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
            var producerInput = new Producer()
            {
                Name = "Teste",
                FollowingWin=1,
                Interval=2,
                PreviousWin=3
            };

          
            _repositoryProducer.Adicionar(producerInput);
            #endregion

           


        }

      
    }
}

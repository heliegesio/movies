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
            string filePath = "arquivo.json"; // Altere para o caminho do seu arquivo

            // Lê o conteúdo do arquivo
            var jsonData = File.ReadAllText(filePath);

            // Desserializa o JSON para o objeto C#
            var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonData);

            // min - max
            var producerRequest = new CreateProducerRequest()
            {
                Name = "Producer 1",
                Interval = 1,
                PreviousWin = 2008,
                FollowingWin = 2009,
            };

            await _mediator.Send(producerRequest);

            producerRequest = new CreateProducerRequest()
            {
                Name = "Producer 2",
                Interval = 1,
                PreviousWin = 2018,
                FollowingWin = 2019,
            };

            await _mediator.Send(producerRequest);

            producerRequest = new CreateProducerRequest()
            {
                Name = "Producer 1",
                Interval = 99,
                PreviousWin = 1900,
                FollowingWin = 1999,
            };

            await _mediator.Send(producerRequest);

            producerRequest = new CreateProducerRequest()
            {
                Name = "Producer 2",
                Interval = 99,
                PreviousWin = 2000,
                FollowingWin = 2099,
            };

            await _mediator.Send(producerRequest);

            //#region Producer


            //for (int i = 0; i < 20; i++)
            //{
            //    var producerRequest = new CreateProducerRequest()
            //    {
            //        Name = DataGenerator.GenerateRandomName(),
            //        FollowingWin = DataGenerator.GenerateRandomNumber(10, 40),
            //        Interval = DataGenerator.GenerateRandomNumber(1, 10),
            //        PreviousWin = DataGenerator.GenerateRandomNumber(3, 10),
            //    };

            //    await _mediator.Send(producerRequest);
            //}

            //#endregion

        }

    }
}

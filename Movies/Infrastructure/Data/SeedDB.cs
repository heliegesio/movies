using AutoMapper;
using MediatR;
using Movies.Application.Commands.ProducerCommands.Request;
using Movies.Domain.Models;
using Newtonsoft.Json;

namespace Movies.Infrastructure.Data
{
    public class SeedDB
    {
        private readonly MoviesDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SeedDB(
            MoviesDbContext dbContext,
            IMediator mediator,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _mapper = mapper;

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
            
            #region Imporação do arquivo
            string filePath = "ListaMinMax.json";

            // Lê o conteúdo do arquivo
            var jsonData = File.ReadAllText(filePath);

            // Desserializa o JSON
            var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonData);

            //percorre a lista Min para inserir na base
            foreach (var item in rootObject.Min)
            {
                var producer = _mapper.Map<CreateProducerRequest>(item);
                await _mediator.Send(producer);

            }

            //percorre a lista Max para inserir na base
            foreach (var item in rootObject.Max)
            {
                var producer = _mapper.Map<CreateProducerRequest>(item);
                await _mediator.Send(producer);

            }
            
            #endregion

            //#region Producer criar 99 dados de teste


            //for (int i = 0; i < 99; i++)
            //{
            //    var producerRequest = new CreateProducerRequest()
            //    {
            //        Name = DataGenerator.GenerateRandomName(),
            //        Interval = DataGenerator.GenerateRandomNumber(1, 99),
            //        PreviousWin = DataGenerator.GenerateRandomNumber(1900, 2008),
            //        FollowingWin = DataGenerator.GenerateRandomNumber(2009, 2099),
            //    };

            //    await _mediator.Send(producerRequest);
            //}

            //#endregion

        }

    }
}

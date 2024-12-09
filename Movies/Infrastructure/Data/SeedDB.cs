using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Commands;
using Movies.Core.API;
using Movies.Domain.Models;
using Movies.Domain.Repositories;
using System.Data;

namespace Movies.Infrastructure.Data
{
    public class SeedDB
    {
        private readonly MoviesDbContext _dbContext;
        private readonly IProducerRepository _repositoryProducer;
        private readonly IEnvironment _environment;

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


       

        public SeedDB(
            MoviesDbContext dbContext,
            IProducerRepository repositoryProducer,
            IEnvironment environment,
            IMapper mapper,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _repositoryProducer = repositoryProducer;
            _mapper = mapper;
            _mediator = mediator;
            _environment = environment;

        }

        public async Task Seed()
        {
            await SeedTransactionDevelopment();
        }

        private async Task SeedTransactionDevelopment()
        {
            if (!_environment.IsDevelopment())
                return;

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
            var producerInput = new CreateProducerCommand()
            {
                Producer = "Teste",
                FollowingWin=1,
                Interval=2,
                PreviousWin=3
            };

            var producer = _mapper.Map<Producer>(producerInput);
            _repositoryProducer.Adicionar(producer);
            #endregion

           


        }

      
    }
}

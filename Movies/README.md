# movies

A aplicação foi desenvolvida de acodo com o padrão CQRS
O bando de dados usado é o SqLite

A importação do arquivo é fieta no inicio ao startar a aplicação no Program.cs
Para essa abplicação estou usando o Seed, que além de importar o arquivo também me permite criar um grande numnero de dados na base para fazer testes

Program.cs linha 36

            #region
                        var seed = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedDB>();
                        await seed.Seed();
            #endregion

            
O Seed se encontra em Movies/Infrastructure/Data/SeedDB.cs
Na linha 54

Na linha 81 tem uma funcionaldade comentada que permite inserir massa de registro na base

            #region Producer criar 99 dados de teste
            
                        for (int i = 0; i < 99; i++)
                        {
                            var producerRequest = new CreateProducerRequest()
                            {
                                Name = DataGenerator.GenerateRandomName(),
                                Interval = DataGenerator.GenerateRandomNumber(1, 99),
                                PreviousWin = DataGenerator.GenerateRandomNumber(1900, 2008),
                                FollowingWin = DataGenerator.GenerateRandomNumber(2009, 2099),
                            };
            
                            await _mediator.Send(producerRequest);
                        }

            #endregion

A arquiteura se basea em code first permitindo manuteções na base com maior facilidade
Por isso pode ser necessário rodar os comandos abaixo

            #region
            
                        dotnet tool install --global dotnet-ef
                        
                        dotnet ef database update
            
            #endregion

Para rodar o projeto basta executar o comando abaixo e abrir o browser no endereço: https://localhost:7039/swagger

            #region
            
                        dotnet run --launch-profile https
            
            #endregion
            
No Program.cs também tem uma configuração para sempre limar a base

            #region
            
                        var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<MoviesDbContext>();
                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.Migrate();
            
            #endregion

Para fazer os desavios o caminho foi juntar todos os dados em uma tabela e a partir desse resultado fazer os calculos:

Obter o produtor com maior intervalo entre dois prêmios consecutivos (esssa fiquei ná dúvida, não entendi bem)
O o resultado do teste está em Movies/Application/Queries/ProducerQuerys/ListarProducerMaxIntervaloQueryHandler.cs

o que obteve dois prêmios mais rápido
O resultado do teste está em Movies/Application/Queries/ProducerQuerys/ListarProducerMixIntervaloQueryHandler.cs

Ainda é preciso melhorar os comentários nó codigo para melhor entendimento



using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Repositories;
using System.Data;

namespace Movies.Infrastructure.Data
{
    public class SeedDB
    {
        private readonly MoviesDbContext _dbContext;
        private readonly IProducerRepository _repositoryEstabelecimento;
        
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public Guid estabelecimentoId = new Guid("a05a8b4a-020d-4295-afff-cd2a0563a2eb");
        public Guid clienteId = new Guid("629f2b91-d1d5-498d-adb6-f19c72025362");
        public Guid planoId10000 = new Guid("aaa0f2d4-ef90-43a9-878a-74e7658f5d24");
        public Guid planoId20000 = new Guid("6cb0c6dd-a84b-40a0-8ec5-5093ead60fe1");
        public Guid planoId7000 = new Guid("fc51e3f4-c226-4212-898e-c96417d48e28");
        public Guid planoId1000 = new Guid("d7c6c662-63dc-4616-98a8-1835328a8f6a");
        public Guid planoId18000 = new Guid("e97a3b8a-d830-4af3-88c4-a600f3598186");
        public Guid planoId1500 = new Guid("835df2f4-f641-40b1-ac55-5920336181d7");
        public Guid produtoId0 = new Guid("42ddb00d-cd45-462a-86fa-c40fbc6ffbec");

        public SeedDB(
            BusinessDbContext dbContext,
            IEnvironment environment,
            IEstabelecimentoRepository repositoryEstabelecimento,
            IClienteRepository repositoryCliente,
            IGrupoEconomicoRepository repositoryGrupoEconomico,
            IProdutoRepository repositoryProduto,
            IPlanoRepository repositoryPlano,
            IESitefRepository repositoryESitef,
            IESitefPagamentoRepository eSitePagamentoRepository,
            ILinkPagamentoRepository repositoryLinkPagamento,
            IAssinaturaRepository repositoryAssinatura,
            IMapper mapper,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _eSitePagamentoRepository = eSitePagamentoRepository;
            _repositoryEstabelecimento = repositoryEstabelecimento;
            _repositoryESitef = repositoryESitef;
            _environment = environment;
            _repositoryCliente = repositoryCliente;
            _repositoryGrupoEconomico = repositoryGrupoEconomico;
            _repositoryProduto = repositoryProduto;
            _repositoryPlano = repositoryPlano;
            _repositoryLinkPagamento = repositoryLinkPagamento;
            _repositoryAssinatura = repositoryAssinatura;
            _mapper = mapper;
            _mediator = mediator;

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

            await using var transactionComp = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (_environment.IsDevelopment())
                {
                    await SeedCompDataDevelopment();
                }
                await _dbContext.CommitAsync();
                await transactionComp.CommitAsync();
            }
            catch (Exception)
            {
                await transactionComp.RollbackAsync();
                throw;
            }
        }

        private async Task SeedDataDevelopment()
        {
            #region Grupo Economico
            var grupoInput = new CriarGrupoEconomicoCommandInput()
            {
                Nome = "Grupo teste lyncas"
            };

            var grupo = _mapper.Map<GrupoEconomico>(grupoInput);
            _repositoryGrupoEconomico.Adicionar(grupo);

            var grupoId = grupo.Id;
            #endregion

            #region Estabelecimento 
            var estabelecimentoId2 = new Guid();
            var cnpj = "31772316000125";
            if (!await _repositoryEstabelecimento.ObterAsNoTracking().AnyAsync(x => x.CpfCnpj.Numero == cnpj))
            {
                var estabelecimentoInput = new CriarEstabelecimentoCommandInput()
                {
                    CpfCnpj = cnpj,
                    RazaoSocial = "Razão Social Teste Lyncas",
                    NomeFantasia = "Nome Fantasia Teste Lyncas",
                    MCC = "1234",
                    GrupoEconomicoId = grupoId,
                    EnviaLinkRecorrencia = true,
                    LiberarAntecipacaoRecorrencia = true,
                    LimiteInadimplencia = 300,
                    FaturamentoMensal = 50000,
                    LimiteMaximoInicial = 10000,
                    DataInicioAntecipacao = new DateTime(2022, 6, 1),
                    SoftDescriptor = "Razão Social Teste Lyncas",
                    ValidadeLimiteInicialDias = 90,
                    PercentualAntecipacao = 70,
                    TaxaCreditoAVista = Convert.ToDecimal(2.09),
                    Endereco = new Application.Commands.Shared.Endereco.EnderecoCommandInput()
                    {
                        Cep = "01311100",
                        Logradouro = "Av. Paulista",
                        Numero = "807",
                        Bairro = "Bela Vista",
                        Complemento = "",
                        Cidade = "São Paulo",
                        Uf = "SP",
                        Pais = "Brasil"
                    },
                    Contatos = new List<ContatoCommandInput>()
                    {
                        new ContatoCommandInput() {
                            Nome="Contato teste 1",
                            NumeroTelefone="11988758657",
                            Email="emailcontatoteste@teste.lyncas.com.br",
                            TipoContato =TipoContatoEnum.Contato,
                            Ativo = true
                        }
                    },
                    ContasBancarias = new List<ContaBancariaCommandInput>()
                    {
                        new ContaBancariaCommandInput()
                        {
                            Favorecido = "Conta bancaria teste",
                            CpfCnpj = "72217382039",
                            Banco = "Banco do Brasil",
                            TipoConta = TipoContaBancariaEnum.ContaCorrente,
                            Agencia = "45533",
                            Conta = "0256644",
                            Ativo = true
                        }
                    }
                };

                var estabelecimento = _mapper.Map<Estabelecimento>(estabelecimentoInput);
                estabelecimento.Id = estabelecimentoId;

                _repositoryEstabelecimento.Adicionar(estabelecimento);
                estabelecimentoId = estabelecimento.Id;

                var cnpj2 = "07876696000163";

                var estabelecimentoInput2 = new CriarEstabelecimentoCommandInput()
                {
                    CpfCnpj = cnpj2,
                    RazaoSocial = "Razão Social 2 Teste Lyncas",
                    NomeFantasia = "Nome Fantasia 2 Teste Lyncas",
                    MCC = "12345",
                    GrupoEconomicoId = grupoId,
                    EnviaLinkRecorrencia = true,
                    LiberarAntecipacaoRecorrencia = true,
                    FaturamentoMensal = 50000,
                    LimiteMaximoInicial = 10000,
                    DataInicioAntecipacao = new DateTime(2023, 1, 1),
                    SoftDescriptor = "Razão Social 2 Teste Lyncas",
                    LimiteInadimplencia = 90,
                    Endereco = new Application.Commands.Shared.Endereco.EnderecoCommandInput()
                    {
                        Cep = "01311100",
                        Logradouro = "Av. Paulista",
                        Numero = "807",
                        Bairro = "Bela Vista",
                        Complemento = "",
                        Cidade = "São Paulo",
                        Uf = "SP",
                        Pais = "Brasil"
                    },
                    Contatos = new List<ContatoCommandInput>()
                    {
                        new ContatoCommandInput() {
                            Nome="Contato teste 2",
                            NumeroTelefone="11988758657",
                            Email="emailcontatoteste2@teste.lyncas.com.br",
                            TipoContato =TipoContatoEnum.Socio
                        }
                    },
                    ContasBancarias = new List<ContaBancariaCommandInput>()
                    {
                        new ContaBancariaCommandInput()
                        {
                            Favorecido = "Conta bancaria teste 2",
                            CpfCnpj = "72217382039",
                            Banco = "Banco do Brasil",
                            TipoConta = TipoContaBancariaEnum.ContaCorrente,
                            Agencia = "45532",
                            Conta = "0256642"
                        }
                    }
                };

                var estabelecimento2 = _mapper.Map<Estabelecimento>(estabelecimentoInput2);
                estabelecimento2.Id = new Guid("dbbab565-e06e-4fff-bcd1-a4ae8c9f66dd");

                _repositoryEstabelecimento.Adicionar(estabelecimento2);
                estabelecimentoId2 = estabelecimento2.Id;
            }
            else
            {
                var estabelecimento = await _repositoryEstabelecimento.ObterAsNoTracking().Where(x => x.CpfCnpj.Numero == "31772316000125").FirstOrDefaultAsync();
                estabelecimentoId = estabelecimento!.Id;
            }
            #endregion

            #region Cliente 
            if (!await _repositoryCliente.ObterAsNoTracking().AnyAsync(x => x.EstabelecimentoId == estabelecimentoId))
            {
                var clienteInput = new CriarClienteCommandInput()
                {
                    Nome = "Cliente teste Lyncas",
                    Email = "emailclienteteste@teste.lyncas.com.br",
                    Cpf = "91574863010",
                    NumeroTelefone = "11955687459",
                    EstabelecimentoId = estabelecimentoId,
                    Endereco = new Application.Commands.Shared.Endereco.EnderecoCommandInput()
                    {
                        Cep = "01311100",
                        Logradouro = "Av. Paulista",
                        Numero = "807",
                        Bairro = "Bela Vista",
                        Complemento = "",
                        Cidade = "São Paulo",
                        Uf = "SP",
                        Pais = "Brasil"
                    }
                };

                var cliente = _mapper.Map<Cliente>(clienteInput);
                cliente.Id = clienteId;
                _repositoryCliente.Adicionar(cliente);
                clienteId = cliente.Id;
            }
            #endregion

            #region Produto
            var produtoId0 = new Guid("42ddb00d-cd45-462a-86fa-c40fbc6ffbec");
            var produtoId1 = new Guid("1be5d4c7-1368-4203-bbfb-01f21a976aad");
            var produtoId2 = new Guid("4b93fb16-8380-4cec-ad90-b67f2edb3b37");
            for (var i = 0; i < 3; i++)
            {
                var nomeProduto = string.Format("Produto {0} teste lyncas", i);
                var produtoBanco = await _repositoryProduto.ObterAsNoTracking().Where(x => x.Nome == nomeProduto).FirstOrDefaultAsync();
                if (produtoBanco is null)
                {
                    var produtoInput = new CriarProdutoCommandInput()
                    {
                        Nome = nomeProduto,
                        Valor = 100 + i,
                        EstabelecimentoId = estabelecimentoId
                    };

                    var produto = _mapper.Map<Produto>(produtoInput);
                    switch (i)
                    {
                        case 0:
                            produto.Id = produtoId0;
                            break;
                        case 1:
                            produto.Id = produtoId1;
                            break;
                        case 2:
                            produto.Id = produtoId2;
                            break;
                    }

                    _repositoryProduto.Adicionar(produto);

                    switch (i)
                    {
                        case 0:
                            produtoId0 = produto.Id;
                            break;
                        case 1:
                            produtoId1 = produto.Id;
                            break;
                        case 2:
                            produtoId2 = produto.Id;
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 0:
                            produtoId0 = produtoBanco.Id;
                            break;
                        case 1:
                            produtoId1 = produtoBanco.Id;
                            break;
                        case 2:
                            produtoId2 = produtoBanco.Id;
                            break;
                    }
                }
            }
            #endregion

            #region Plano
            var planoNome = "Plano teste lyncas 10000";

            if (!await _repositoryPlano.ObterAsNoTracking().AnyAsync(x => x.Nome == planoNome))
            {
                #region 10000
                //inicio criar plano Id 10000
                var entrada10000 = new CriarPlanoCommandInput()
                {
                    Nome = "Plano teste lyncas 10000",
                    AplicarCarenciaPlano = true,
                    AplicarTaxaExtra = true,
                    DescricaoTaxaExtra = "descricao teste",
                    ValorTaxa = 0,
                    ValorPlano = 8000,
                    Frequencia = TipoFrequenciaEnum.Mensal,
                    Parcelas = 5,
                    TaxaExtraParcelada = false,
                    EstabelecimentoId = estabelecimentoId,
                    Produtos = new List<PlanoProdutoCommandInput>()
                    {
                       new PlanoProdutoCommandInput()
                       {
                            ProdutoId = produtoId0,
                            Quantidade = 1,
                            Valor=2000
                       }
                    }
                };

                var map10000 = _mapper.Map<Plano>(entrada10000);
                map10000.Ativo = true;
                _repositoryPlano.Adicionar(map10000);
                map10000.Id = planoId10000;
                planoId10000 = map10000.Id;
                //fim criar plano Id
                #endregion

                #region 20000
                //inicio criar plano Id 20000
                var entrada20000 = new CriarPlanoCommandInput()
                {
                    Nome = "Plano teste lyncas 20000",
                    AplicarCarenciaPlano = true,
                    AplicarTaxaExtra = true,
                    DescricaoTaxaExtra = "descricao teste",
                    ValorTaxa = 0,
                    ValorPlano = 18000,
                    Frequencia = TipoFrequenciaEnum.Mensal,
                    Parcelas = 10,
                    TaxaExtraParcelada = false,
                    EstabelecimentoId = estabelecimentoId,
                    Produtos = new List<PlanoProdutoCommandInput>()
                    {
                       new PlanoProdutoCommandInput()
                       {
                            ProdutoId = produtoId0,
                            Quantidade = 1,
                            Valor=2000
                       }
                    }
                };

                var map20000 = _mapper.Map<Plano>(entrada20000);
                map20000.Ativo = true;
                _repositoryPlano.Adicionar(map20000);
                map20000.Id = planoId20000;
                planoId20000 = map20000.Id;
                //fim criar plano Id
                #endregion

                #region 7000
                //inicio criar plano Id 7000
                var entrada7000 = new CriarPlanoCommandInput()
                {
                    Nome = "Plano teste lyncas 7000",
                    AplicarCarenciaPlano = true,
                    AplicarTaxaExtra = true,
                    DescricaoTaxaExtra = "descricao teste",
                    ValorTaxa = 0,
                    ValorPlano = 4000,
                    Frequencia = TipoFrequenciaEnum.Mensal,
                    Parcelas = 10,
                    TaxaExtraParcelada = false,
                    EstabelecimentoId = estabelecimentoId,
                    Produtos = new List<PlanoProdutoCommandInput>()
                    {
                       new PlanoProdutoCommandInput()
                       {
                            ProdutoId = produtoId0,
                            Quantidade = 1,
                            Valor=3000
                       }
                    }
                };

                var map7000 = _mapper.Map<Plano>(entrada7000);
                map7000.Ativo = true;
                _repositoryPlano.Adicionar(map7000);
                map7000.Id = planoId7000;
                planoId7000 = map7000.Id;
                //fim criar plano Id
                #endregion

                #region 1000
                //inicio criar plano Id 1000
                var entrada1000 = new CriarPlanoCommandInput()
                {
                    Nome = "Plano teste lyncas 1000",
                    AplicarCarenciaPlano = true,
                    AplicarTaxaExtra = true,
                    DescricaoTaxaExtra = "descricao teste",
                    ValorTaxa = 0,
                    ValorPlano = 900,
                    Frequencia = TipoFrequenciaEnum.Trimestral,
                    Carencia = 2,
                    Parcelas = 10,
                    TaxaExtraParcelada = false,
                    EstabelecimentoId = estabelecimentoId,
                    Produtos = new List<PlanoProdutoCommandInput>()
                    {
                       new PlanoProdutoCommandInput()
                       {
                            ProdutoId = produtoId0,
                            Quantidade = 1,
                            Valor=100
                       }
                    }
                };

                var map1000 = _mapper.Map<Plano>(entrada1000);
                map1000.Ativo = true;
                _repositoryPlano.Adicionar(map1000);
                map1000.Id = planoId1000;
                planoId1000 = map1000.Id;
                //fim criar plano Id
                #endregion

                #region 18000
                //inicio criar plano Id 18000
                var entrada18000 = new CriarPlanoCommandInput()
                {
                    Nome = "Plano teste lyncas 18000",
                    AplicarCarenciaPlano = true,
                    AplicarTaxaExtra = true,
                    DescricaoTaxaExtra = "descricao teste",
                    ValorTaxa = 0,
                    ValorPlano = 17000,
                    Frequencia = TipoFrequenciaEnum.Semestral,
                    Carencia = 2,
                    Parcelas = 5,
                    TaxaExtraParcelada = false,
                    EstabelecimentoId = estabelecimentoId,
                    Produtos = new List<PlanoProdutoCommandInput>()
                    {
                       new PlanoProdutoCommandInput()
                       {
                            ProdutoId = produtoId0,
                            Quantidade = 1,
                            Valor=1000
                       }
                    }
                };

                var map18000 = _mapper.Map<Plano>(entrada18000);
                map18000.Ativo = true;
                _repositoryPlano.Adicionar(map18000);
                map18000.Id = planoId18000;
                planoId18000 = map18000.Id;
                #endregion

                #region 1500
                //inicio criar plano Id 1500
                var entrada1500 = new CriarPlanoCommandInput()
                {
                    Nome = "Plano teste lyncas 1500",
                    AplicarCarenciaPlano = true,
                    AplicarTaxaExtra = true,
                    DescricaoTaxaExtra = "descricao teste",
                    ValorTaxa = 0,
                    ValorPlano = 1000,
                    Frequencia = TipoFrequenciaEnum.Anual,
                    Carencia = 2,
                    Parcelas = 5,
                    TaxaExtraParcelada = false,
                    EstabelecimentoId = estabelecimentoId,
                    Produtos = new List<PlanoProdutoCommandInput>()
                    {
                       new PlanoProdutoCommandInput()
                       {
                            ProdutoId = produtoId0,
                            Quantidade = 1,
                            Valor=500
                       }
                    }
                };

                var map1500 = _mapper.Map<Plano>(entrada1500);
                map1500.Ativo = true;
                _repositoryPlano.Adicionar(map1500);
                map1500.Id = planoId1500;
                planoId1500 = map1500.Id;
                //fim criar plano Id
                #endregion

                #region generico
                var planoId1 = new Guid();
                var entrada1 = new CriarPlanoCommandInput()
                {
                    Nome = "Plano teste lyncas parcela taxa",
                    AplicarCarenciaPlano = true,
                    AplicarTaxaExtra = true,
                    DescricaoTaxaExtra = "descricao teste",
                    ValorTaxa = 200,
                    ValorPlano = 100,
                    Frequencia = TipoFrequenciaEnum.Mensal,
                    Parcelas = 8,
                    TaxaExtraParcelada = true,
                    EstabelecimentoId = estabelecimentoId,
                    Produtos = new List<PlanoProdutoCommandInput>()
                    {
                       new PlanoProdutoCommandInput()
                       {
                            ProdutoId = produtoId0,
                            Quantidade = 10,
                            Valor=10
                       }
                    }
                };

                var map1 = _mapper.Map<Plano>(entrada1);
                map1.Ativo = true;
                map1.Id = new Guid("8e6cc8e3-ad42-4b35-9d63-c85f0849a1dc");
                _repositoryPlano.Adicionar(map1);
                planoId1 = map1.Id;
                #endregion

            }
            #endregion


        }

        private async Task SeedCompDataDevelopment()
        {


            #region LinkPagamento

            if (!await _repositoryLinkPagamento.ObterAsNoTracking().AnyAsync(x => x.EstabelecimentoId == estabelecimentoId))
            {
                #region 000

                var linkPagamentoInput000 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 000 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId10000,
                    Valor = 2000,
                    Desconto = 0,
                    Parcelas = 5,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=2000
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput000);

                #endregion 000

                #region 001

                var linkPagamentoInput001 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 001 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId20000,
                    Valor = 2000,
                    Desconto = 0,
                    Parcelas = 10,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=2000
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput001);

                #endregion 001

                #region 002

                var linkPagamentoInput002 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 002 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId7000,
                    Valor = 700,
                    Desconto = 0,
                    Parcelas = 10,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=3000
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput002);

                #endregion 002

                #region 003

                var linkPagamentoInput003 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 003 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId10000,
                    Valor = 2000,
                    Desconto = 0,
                    Parcelas = 5,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=20000
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput003);

                #endregion 003

                #region 004

                var linkPagamentoInput004 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 004 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId1000,
                    Valor = 100,
                    Desconto = 0,
                    Parcelas = 10,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=100
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput004);

                #endregion

                #region 005

                var linkPagamentoInput005 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 005 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId1000,
                    Valor = 100,
                    Desconto = 0,
                    Parcelas = 10,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=100
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput005);

                #endregion 005

                #region 006

                var linkPagamentoInput006 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 006 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId1000,
                    Valor = 100,
                    Desconto = 0,
                    Parcelas = 10,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=100
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput006);

                #endregion 006

                #region 007

                var linkPagamentoInput007 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 007 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId20000,
                    Valor = 2000,
                    Desconto = 0,
                    Parcelas = 10,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=3000
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput007);

                #endregion 007

                #region 008

                var linkPagamentoInput008 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 008 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId18000,
                    Valor = 3600,
                    Desconto = 0,
                    Parcelas = 5,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=3000
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput008);

                #endregion 008

                #region 009

                var linkPagamentoInput009 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 009 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId1500,
                    Valor = 300,
                    Desconto = 0,
                    Parcelas = 5,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=500
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput009);

                #endregion 009

                #region 010

                var linkPagamentoInput010 = new CriarLinkPagamentoCommandInput()
                {
                    Descricao = "Link Pagamento 010 teste lyncas",
                    EstabelecimentoId = estabelecimentoId,
                    ClienteId = clienteId,
                    PlanoId = planoId20000,
                    Valor = 2000,
                    Desconto = 0,
                    Parcelas = 5,
                    IntegrarESitef = false,
                    TipoLinkPagamento = TipoLinkPagamentoEnum.Recorrencia,
                    TipoCobranca = TipoLinkPagamentoCobrancaEnum.ValorAvulso,
                    TipoCliente = TipoLinkPagamentoClienteEnum.ClienteCadastrado,
                    LinkPagamentoProdutos = new List<LinkPagamentoProdutoCommandInput>()
                    {
                       new LinkPagamentoProdutoCommandInput()
                       {
                            ProdutoId=produtoId0,
                            Quantidade=1,
                            Valor=2000
                       }
                    }
                };

                await _mediator.Send(linkPagamentoInput010);

                #endregion 010
            }

            #endregion
            string pedido = "1";
            string entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            var commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            var result = await _mediator.Send(commandInput);

            pedido = "2";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "3";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "4";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "5";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "6";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "7";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "8";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "9";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "10";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            pedido = "11";
            entidade = "s=1&rede=Redecard&agendamento.status=CON&agendamento.numeroexecucoes=0&tipoFinanciamento=4&mensagem=Nao+existe+conf.&valor=3875&binCartao=341529&agendamento.valor=3875&acquirerdId=1005&nsuesitef=221221055455270&parcelas=1&agendamento.proximadata=21%2F01%2F2023&nsu=null&autorizadora=3&nit=c15181a69a399247e23e16dcfbfcb7416d01099b81a3668619869924575fdb91&pedido=" + pedido + "&agendamento.sid=481bdef647efbdca667f347c49d2ee9aa9225891aa253b04a6f8c404f6b58e3c&tipoPagamento=C&agendamento.totalexecucoes=8&finalCartao=7805&status=CON";

            commandInput = new SalvarPagamentoESitefCommandInput(entidade);
            result = await _mediator.Send(commandInput);

            var assinatura004 = await _repositoryAssinatura
                    .Obter()
                    .Where(x => x.LinkPagamento!.Descricao == "Link Pagamento 004 teste lyncas")
                    .FirstOrDefaultAsync();
            assinatura004!.Situacao = TipoSituacaoAssinaturaEnum.Cancelada;

            _repositoryAssinatura.Update(assinatura004);

            var assinatura005 = await _repositoryAssinatura
                    .Obter()
                    .Where(x => x.LinkPagamento!.Descricao == "Link Pagamento 005 teste lyncas")
                    .FirstOrDefaultAsync();
            assinatura005!.Situacao = TipoSituacaoAssinaturaEnum.PendenteCancelamento;

            _repositoryAssinatura.Update(assinatura005);

            var assinatura006 = await _repositoryAssinatura
                    .Obter()
                    .Where(x => x.LinkPagamento!.Descricao == "Link Pagamento 006 teste lyncas")
                    .FirstOrDefaultAsync();
            assinatura006!.Situacao = TipoSituacaoAssinaturaEnum.EmAtraso;

            _repositoryAssinatura.Update(assinatura006);

            var assinatura007 = await _repositoryAssinatura
                    .Obter()
                    .Where(x => x.LinkPagamento!.Descricao == "Link Pagamento 007 teste lyncas")
                    .FirstOrDefaultAsync();
            assinatura007!.Situacao = TipoSituacaoAssinaturaEnum.EmAtraso;

            _repositoryAssinatura.Update(assinatura007);


            var assinatura002 = await _repositoryAssinatura
                    .Obter()
                    .Where(x => x.LinkPagamento!.Descricao == "Link Pagamento 002 teste lyncas")
                    .FirstOrDefaultAsync();
            assinatura002!.Situacao = TipoSituacaoAssinaturaEnum.EmAtraso;

            _repositoryAssinatura.Update(assinatura002);


        }
    }
}

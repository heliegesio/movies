using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Teste.IntegrationTests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Movies.Teste
{
    [TestClass]
    public class ProducerTest : MovieApiTests
    {

        [TestMethod]
        public void BuscarProducers()
        {
            Console.WriteLine("teste");
            var retorno = http("Producer", "get");
            if (retorno == null)
            {
                Assert.Fail(erro);

                Console.WriteLine($"Erro: {erro}");
            }
            else
            {
                Console.WriteLine("http ok");
            }


            var retornoAPI = JsonConvert.DeserializeObject<JObject>(retorno);

            var paginacao = retornoAPI["result"]["paginacao"];
            if (paginacao == null)
            {
                Assert.Fail("N�o retornou a se��o de pagina��o");
            }

            // Aqui voc� pode verificar se "totalDeElementos" existe antes de acessar
            var totalDeElementosToken = paginacao["totalDeElementos"];
            if (totalDeElementosToken == null)
            {
                Assert.Fail("N�o retornou o total de elementos");
            }

            // Verifica se � um n�mero e se o total de elementos � igual a 4
            if (totalDeElementosToken.Type == JTokenType.Integer && (int)totalDeElementosToken == 4)
            {
                Console.WriteLine($"Total de Elementos: {totalDeElementosToken}");
            }
            else
            {
                Console.WriteLine($"Total de Elementos: {(int)totalDeElementosToken}, esperado: 4");
                Assert.Fail("Total de elementos n�o � igual a 4");
            }


            var itens = retornoAPI.SelectToken("result.itens");
            Assert.IsNotNull(itens, "O campo 'itens' n�o foi encontrado no resultado.");

            if (itens.Type == JTokenType.Array && ((JArray)itens).Count != 4)
            {
                Assert.Fail("N�o retornou o resultado esperado da lista");
            }
            else
            {
                Console.WriteLine(itens);

                Console.WriteLine(itens);
            }

        }

        [TestMethod]
        public void BuscarComMaiorIntevalo()
        {

            var retorno = http("Producer/ProdutorComMaiorIntevalo", "get");
            if (retorno == null)
                Assert.Fail(erro);
            else
                Console.WriteLine("http ok");

            var retornoAPI = JsonConvert.DeserializeObject<JObject>(retorno);
            var result = retornoAPI.SelectToken("result");

            if (result == null)
            {
                Assert.Fail("N�o retornou o resultado esperado");
            }
            else
            {
                Console.WriteLine(result);
            }

        }

        [TestMethod]
        public void BuscarComMenorIntevalo()
        {

            var retorno = http("Producer/ProdutorComMenorIntevalo", "get");
            if (retorno == null)
                Assert.Fail(erro);
            else
                Console.WriteLine("http ok");

            var retornoAPI = JsonConvert.DeserializeObject<JObject>(retorno);

            var result = retornoAPI.SelectToken("result");

            if (result == null)
            {
                Assert.Fail("N�o retornou o resultado esperado");
            }
            else
            {
                Console.WriteLine(result);
            }

        }
    }
}

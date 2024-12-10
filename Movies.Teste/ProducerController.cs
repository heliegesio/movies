using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Teste.IntegrationTests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Movies.Teste
{
    [TestClass]
    public class ProducerTest : MovieApiTests
    {      

        [TestMethod]
        public void BuscarProducers()
        {

            var retorno = http("Producer", "get");
            if (retorno == null)
                Assert.Fail(erro);
            else
                Trace.WriteLine("http ok");

            var retornoAPI = JsonConvert.DeserializeObject<JObject>(retorno);

            var paginacao = retornoAPI["result"]["paginacao"];
            if (paginacao == null)
            {
                Assert.Fail("Não retornou a seção de paginação");
            }

            // Aqui você pode verificar se "totalDeElementos" existe antes de acessar
            var totalDeElementosToken = paginacao["totalDeElementos"];
            if (totalDeElementosToken == null)
            {
                Assert.Fail("Não retornou o total de elementos");
            }

            // Verifica se é um número e se o total de elementos é igual a 4
            if (totalDeElementosToken.Type == JTokenType.Integer && (int)totalDeElementosToken == 4)
            {
                Trace.WriteLine($"Total de Elementos: {totalDeElementosToken}");
            }
            else
            {
                Trace.WriteLine($"Total de Elementos: {(int)totalDeElementosToken}, esperado: 4");
                Assert.Fail("Total de elementos não é igual a 4");
            }


            var itens = retornoAPI.SelectToken("result.itens");
            Assert.IsNotNull(itens, "O campo 'itens' não foi encontrado no resultado.");

            if (itens.Type == JTokenType.Array && ((JArray)itens).Count != 4)
            {
                Assert.Fail("Não retornou o resultado esperado da lista");
            }
            else
            {
                Trace.WriteLine(itens);
            }

        }
    }
}

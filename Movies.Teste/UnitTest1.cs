using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Teste.IntegrationTests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Movies.Teste
{
    [TestClass]
    public class UnitTest1 : MovieApiTests
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        [TestMethod]
        public void Inserir()
        {

            var retorno = http("Producer", "get");
            var retornoAPI = JsonConvert.DeserializeObject<JObject>(retorno);
            Trace.WriteLine(retornoAPI["result"]);

        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Teste.IntegrationTests;

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

            http("Producer", "get");


        }
    }
}

using System;
using System.IO;
using System.Net;

namespace Movies.Teste.IntegrationTests
{
    public class MovieApiTests
    {
        public string token = "";
        public string urlAPI = "https://localhost:7039/";

        public string http(string url, string tipo, object send=null)
        {

            string urlsend = urlAPI;
            urlsend += url;

            string retorno = "";



            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlsend);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = tipo;

                if (tipo != "get")
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {

                        streamWriter.Write(send);
                    }
                }
                WebResponse res = httpWebRequest.GetResponse();
                HttpWebResponse response = (HttpWebResponse)res;
                if ((int)response.StatusCode > 226 || response.StatusCode == HttpStatusCode.NotFound)
                {
                    string status = "ERROR: " + response.StatusCode.ToString();
                }
                else
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        retorno = streamReader.ReadToEnd();
                    }

                }
            }
            catch (WebException ex)
            {
                string ret = "";

                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    ret = reader.ReadToEnd();
                }
                ret += url;
                ret += send;
                throw new Exception(ret);
            }
            catch (Exception ex)
            {

                string status = "ERROR: Something bad happend: " + ex.ToString();
            }



            return retorno;
        }
    }
}

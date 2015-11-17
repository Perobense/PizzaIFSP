using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Threading;
public class ConsultaCep
{
    //public DataPrev.Domain.SistemaModel.ErroModel RequisicaoRV(long NumeroBeneficio)
    //{
    //    DataPrev.Domain.SistemaModel.ErroModel Retorno = new Domain.SistemaModel.ErroModel();
    //    try
    //    {

    //        Domain.SistemaModel.IpModel IpList = new Domain.SistemaModel.IpModel();
    //        if (System.Configuration.ConfigurationManager.AppSettings["Consulta"] == "Lote")
    //            IpList = Rep.IpsLiberados();
    //        if (System.Configuration.ConfigurationManager.AppSettings["Consulta"] == "Sistema")
    //            IpList = Rep.IpsLiberados2();
    //        WebProxy proxyObj = new WebProxy(IpList.IP, IpList.Porta);
    //        proxyObj.Credentials = CredentialCache.DefaultCredentials;
    //        Retorno.Retorno3 = IpList.IP;

    //        Uri link = new Uri("http://www010.dataprev.gov.br/CWS/BIN/CWS.asp");
    //        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(link);
    //        request.ProtocolVersion = HttpVersion.Version10;
    //        request.Proxy = proxyObj;
    //        request.Credentials = CredentialCache.DefaultCredentials;

    //        CookieContainer _Cookies = new CookieContainer();
    //        request.CookieContainer = _Cookies;
    //        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
    //        request.Referer = "http://www3.dataprev.gov.br/cws/contexto/hiscre/hiscrenet2.asp";

    //        request.Method = "POST";
    //        request.ContentType = "application/x-www-form-urlencoded";
    //        byte[] byteArray = Encoding.UTF8.GetBytes("C_1=BLR00.11&C_2=&C_3=" + NumeroBeneficio + "&layout=8%2C69%2C10%2C8%2C1&submit=Transmite");
    //        request.ContentLength = byteArray.Length;

    //        var h = request.CookieContainer.GetCookies(request.RequestUri);
    //        Stream dataStream = request.GetRequestStream();
    //        dataStream.Write(byteArray, 0, byteArray.Length);
    //        int d = dataStream.ReadTimeout;
    //        dataStream.Close();

    //        //Fonte extrato retorno
    //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //        dataStream = response.GetResponseStream();
    //        StreamReader reader = new StreamReader(dataStream);
    //        Retorno.Fonte = reader.ReadToEnd();

    //        response.Close();
    //        request.Abort();
    //    }
    //    catch (Exception e)
    //    {
    //        Retorno.Fonte = e.Message;
    //    }
    //    Thread.Sleep(20000);

    //    return Retorno;
    //}

    public static void Main()
    {
        string i = "010010010";
        while (1==1)
        {
            string PrimeiroValor = i.Substring(0, 1);

            int valor =  int.Parse(i) + 1;

            i = PrimeiroValor + valor.ToString();

            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create("http://m.correios.com.br/movel/buscaCepConfirma.do?tipoCep=&cepTemp=&metodo=buscarCep&cepEntrada=" + i.ToString()+"");
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            //request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}


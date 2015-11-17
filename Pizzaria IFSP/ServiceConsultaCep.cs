using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPrev.Infra;
using System.Collections.Specialized;
using DataPrev.Domain.Interfaces.Service;
using System.Net;
using System.IO;
using System.Threading;
public class ConsultaCep
{
    public DataPrev.Domain.SistemaModel.ErroModel RequisicaoRV(long NumeroBeneficio)
    {
        DataPrev.Domain.SistemaModel.ErroModel Retorno = new Domain.SistemaModel.ErroModel();
        try
        {

            Domain.SistemaModel.IpModel IpList = new Domain.SistemaModel.IpModel();
            if (System.Configuration.ConfigurationManager.AppSettings["Consulta"] == "Lote")
                IpList = Rep.IpsLiberados();
            if (System.Configuration.ConfigurationManager.AppSettings["Consulta"] == "Sistema")
                IpList = Rep.IpsLiberados2();
            WebProxy proxyObj = new WebProxy(IpList.IP, IpList.Porta);
            proxyObj.Credentials = CredentialCache.DefaultCredentials;
            Retorno.Retorno3 = IpList.IP;

            Uri link = new Uri("http://www010.dataprev.gov.br/CWS/BIN/CWS.asp");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(link);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Proxy = proxyObj;
            request.Credentials = CredentialCache.DefaultCredentials;

            CookieContainer _Cookies = new CookieContainer();
            request.CookieContainer = _Cookies;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Referer = "http://www3.dataprev.gov.br/cws/contexto/hiscre/hiscrenet2.asp";

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] byteArray = Encoding.UTF8.GetBytes("C_1=BLR00.11&C_2=&C_3=" + NumeroBeneficio + "&layout=8%2C69%2C10%2C8%2C1&submit=Transmite");
            request.ContentLength = byteArray.Length;

            var h = request.CookieContainer.GetCookies(request.RequestUri);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            int d = dataStream.ReadTimeout;
            dataStream.Close();

            //Fonte extrato retorno
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            Retorno.Fonte = reader.ReadToEnd();

            response.Close();
            request.Abort();
        }
        catch (Exception e)
        {
            Retorno.Fonte = e.Message;
        }
        Thread.Sleep(20000);

        return Retorno;
    }
}

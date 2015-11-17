using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class Util
{
    public bool ExportarParaExcel(DataTable dt)
    {
        bool retorno = false;
        
        string nomeArquivo = "Relatorio de Vendas";

        try
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();

            foreach (DataColumn column in dt.Columns)
            {
                context.Response.Write(column.ColumnName + ";");
            }
            context.Response.Write(Environment.NewLine);

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo + ".csv");
            context.Response.End();

            retorno = true;
        }
        catch (Exception ex)
        {
            //Email.EnviaMensagem("Não foi possível exportar os registros para a planilha excel." + "\n\n"
            //                    + "Mensagem do erro: " + ex.Message);
        }
        return retorno;
    }

    public string Main()
    {
        // Create a request using a URL that can receive a post. 
        WebRequest request = WebRequest.Create("http://m.correios.com.br/movel/buscaCepConfirma.do?tipoCep=&cepTemp=&metodo=buscarCep&cepEntrada="+"02077020");
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

        return "Vish";
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;

public class Util
{
    //public static bool ExportarParaExcel(DataTable dt)
    //{
    //    bool retorno = false;
    //    Guid id = Guid.NewGuid();
    //    string nomeArquivo = "RetornoArquivo_" + id;

    //    try
    //    {
    //        HttpContext context = HttpContext.Current;
    //        context.Response.Clear();

    //        foreach (DataColumn column in dt.Columns)
    //        {
    //            context.Response.Write(column.ColumnName + ";");
    //        }
    //        context.Response.Write(Environment.NewLine);

    //        foreach (DataRow row in dt.Rows)
    //        {
    //            for (int i = 0; i < dt.Columns.Count; i++)
    //            {
    //                context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");
    //            }
    //            context.Response.Write(Environment.NewLine);
    //        }
    //        context.Response.ContentType = "text/csv";
    //        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo + ".csv");
    //        context.Response.End();

    //        retorno = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        //Email.EnviaMensagem("Não foi possível exportar os registros para a planilha excel." + "\n\n"
    //        //                    + "Mensagem do erro: " + ex.Message);
    //    }
    //    return retorno;
    //}
}

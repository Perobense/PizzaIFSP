using Domain;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace FuturePasta.Controllers
{
    public class GerenciamentoController : Controller
    {
        //
        // GET: /Gerenciamento/

        public ActionResult Index()
        {
            string retorno = "";

            return View("Index", retorno);
        }

        public ActionResult CadastroPizza()
        {
            return View();
        }

        public string CadastroProduto(string Variacao, string Ingredientes, string Nome, string Valor, string SubVariacao, string Tamanho, string Produto, string QtdEstoque, HttpPostedFileBase file)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            MemoryStream ms = new MemoryStream();
            string retorno = "";
            bool Validacao = false;
            try
            {
                if (Produto == "PIZZA")
                {
                    if ((Variacao != "") && (!string.IsNullOrWhiteSpace(Variacao))
                    && (Ingredientes != "") && (!string.IsNullOrWhiteSpace(Ingredientes))
                    && (Nome != "") && (!string.IsNullOrWhiteSpace(Nome))
                    && (Valor != "") && (!string.IsNullOrWhiteSpace(Valor))
                    && (SubVariacao != "") && (!string.IsNullOrWhiteSpace(SubVariacao))
                    && (Produto != "") && (!string.IsNullOrWhiteSpace(Produto))
                    && (Tamanho != "") && (!string.IsNullOrWhiteSpace(Tamanho)))
                    {
                        Validacao = true;
                    }
                }
                else
                {
                    if ((Tamanho != "") && (!string.IsNullOrWhiteSpace(Tamanho))
                    && (Nome != "") && (!string.IsNullOrWhiteSpace(Nome))
                    && (Valor != "") && (!string.IsNullOrWhiteSpace(Valor))
                    && (Produto != "") && (!string.IsNullOrWhiteSpace(Produto)))
                    {
                        Validacao = true;

                    }
                }

                var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

                if (IdFornecedor != null)
                {
                    if (Validacao)
                    {
                        Domain.ProdutoModel Prd = new Domain.ProdutoModel();
                        Persistence.MPRepository rp = new Persistence.MPRepository();

                        if (file != null)
                        {
                            Prd.FotoInteira = new byte[file.ContentLength];
                            file.InputStream.Read(Prd.FotoInteira, 0, file.ContentLength);
                        }

                        int PrdTipo = 0;
                        int PrdVaricacao = 0;
                        int PrdTamanho = 0;
                        int PrdSubVariacao = 0;


                        string RetPrd = rp.BuscaDadosProduto(Produto, Variacao, SubVariacao, Tamanho);

                        var z = RetPrd.Split('|');
                        if (z.Count() > 1)
                        {
                            PrdTipo = int.Parse(z[0]);
                            PrdVaricacao = int.Parse(z[1]);
                            PrdTamanho = int.Parse(z[2]);
                            PrdSubVariacao = int.Parse(z[3]);
                        }
                        else
                        {
                            retorno = z[0];
                        }

                        //else if (Produto == "BEBIDA")
                        //{
                        //    string RetPrd = rp.BuscaDadosProduto(Produto, Variacao, SubVariacao, Tamanho);

                        //    var z = RetPrd.Split('|');
                        //    if (z.Count() > 1)
                        //    {

                        //        PrdTipo = int.Parse(z[0]);
                        //        PrdVaricacao = int.Parse(z[1]);
                        //        PrdTamanho = int.Parse(z[2]);
                        //        PrdSubVariacao = int.Parse(z[3]);
                        //    }
                        //    else
                        //    {
                        //        retorno = z[0];
                        //    }
                        //}
                        //else if (Produto == "PIZZA")
                        //{
                        //    string RetPrd = rp.BuscaDadosProduto(Produto, Variacao, SubVariacao, Tamanho);

                        //    var z = RetPrd.Split('|');
                        //    if (z.Count() > 1)
                        //    {

                        //        PrdTipo = int.Parse(z[0]);
                        //        PrdVaricacao = int.Parse(z[1]);
                        //        PrdTamanho = int.Parse(z[2]);
                        //        PrdSubVariacao = int.Parse(z[3]);
                        //    }
                        //    else
                        //    {
                        //        retorno = z[0];
                        //    }
                        //}
                        //else if (Produto == "MASSA")
                        //{
                        //    string RetPrd = rp.BuscaDadosProduto(Produto, Variacao, SubVariacao, Tamanho);

                        //    var z = RetPrd.Split('|');
                        //    if (z.Count() > 1)
                        //    {

                        //        PrdTipo = int.Parse(z[0]);
                        //        PrdVaricacao = int.Parse(z[1]);
                        //        PrdTamanho = int.Parse(z[2]);
                        //        PrdSubVariacao = int.Parse(z[3]);
                        //    }
                        //    else
                        //    {
                        //        retorno = z[0];
                        //    }
                        //}
                        //else if (Produto == "JAPONES")
                        //{
                        //    string RetPrd = rp.BuscaDadosProduto(Produto, Variacao, SubVariacao, Tamanho);

                        //    var z = RetPrd.Split('|');
                        //    if (z.Count() > 1)
                        //    {

                        //        PrdTipo = int.Parse(z[0]);
                        //        PrdVaricacao = int.Parse(z[1]);
                        //        PrdTamanho = int.Parse(z[2]);
                        //        PrdSubVariacao = int.Parse(z[3]);
                        //    }
                        //    else
                        //    {
                        //        retorno = z[0];
                        //    }
                        //}
                        //else if (Produto == "LANCHES")
                        //{
                        //    string RetPrd = rp.BuscaDadosProduto(Produto, Variacao, SubVariacao, Tamanho);

                        //    var z = RetPrd.Split('|');
                        //    if (z.Count() > 1)
                        //    {

                        //        PrdTipo = int.Parse(z[0]);
                        //        PrdVaricacao = int.Parse(z[1]);
                        //        PrdTamanho = int.Parse(z[2]);
                        //        PrdSubVariacao = int.Parse(z[3]);
                        //    }
                        //    else
                        //    {
                        //        retorno = z[0];
                        //    }
                        //}

                        if (retorno == "")
                        {
                            Prd.Tipo = PrdTipo;
                            Prd.Ingredientes = Ingredientes;
                            Prd.Nome = Nome;
                            Prd.Valor = decimal.Parse(Valor);
                            Prd.IdFonecedor = int.Parse(IdFornecedor.ToString());
                            Prd.Variacao = PrdVaricacao;
                            Prd.Tamanho = PrdTamanho;
                            Prd.SubVariacao = PrdSubVariacao;
                            Prd.QtdEstoque = int.Parse(QtdEstoque);

                            var x = rp.InserirProduto(Prd);

                            if (x)
                            {
                                retorno = Nome + " foi cadastrada com sucesso";
                            }
                            else
                            {
                                retorno = Nome + "  já foi cadastrada.";
                            }
                        }
                    }
                }
                else
                {
                    retorno = "Preencher todos os campos.";
                }
            }
            catch (Exception ex)
            {
                retorno = "Ops! Algo deu errado. Nosso suporte está verificando";
            }
            return retorno;

        }

        public string BuscarProduto(string Dados, string Meia)
        {
            string retorno = "";
            string stringretorno = "";
            string valor = "";
            Domain.ProdutoModel ret = new Domain.ProdutoModel();
            //Dados = IdProduto|idvendedor|tipopizza|Variacoes(meia/inteira)|Tamanho
            //Caso Meia == true o campo dados vira com os dados da primeira pizza sem a borda concatenado com barra "/" mais os dados da segunda piza E
            // Apenas na segunda pizza sera enviada a Borda

            if (Meia == "true")
            {
                //MONTAR RETORNO COM A DESCRICAO DA PIZZA SENDO =  ID'S CONCATENADOS ^ NOME DA PRIMEIRA+NOME DA SEGUNDA ^ VALOR MAIS ALTO É O VALOR DA PIZZA ^ TIPO
                Domain.ProdutoModel Prd = new Domain.ProdutoModel();
                Persistence.MPRepository rp = new Persistence.MPRepository();

                var Metade = Dados.Split('/');

                foreach (var count in Metade)
                {
                    var x = count.Split('^');

                    if (x[0] != null)
                    {
                        Prd.IdProduto = int.Parse(x[0]);

                        Prd.IdFonecedor = int.Parse(x[1]);

                        Prd.Tipo = int.Parse(x[2]);

                        Prd.Variacao = int.Parse(x[3]);

                        Prd.Tamanho = int.Parse(x[4]);

                    }
                    ret = rp.BuscarProduto(Prd);
                    var Borda = "";
                    if (x.Count() > 5)
                    {
                        switch (x[5])
                        {
                            case "1":
                                Borda = "Sem Borda";
                                break;
                            case "2":
                                Borda = "Borda de Cheedar";
                                break;
                            case "3":
                                Borda = "Borda de Catupiry";
                                break;

                        }

                        stringretorno = stringretorno + ret.IdProduto + "^" + ret.Nome + "^" + ret.Valor + "^" + ret.Tipo + "^" + Borda;
                    }
                    else
                    {
                        stringretorno = stringretorno + ret.IdProduto + "^" + ret.Nome + "^" + ret.Valor + "^" + ret.Tipo;
                    }
                }
                var splitretorno = stringretorno.Split('^');

                if (double.Parse(splitretorno[2]) > double.Parse(splitretorno[5]))
                {
                    valor = splitretorno[2];
                }
                else
                {
                    valor = splitretorno[5];
                }

                retorno = splitretorno[0] + "," + splitretorno[3] + "^" + "1/2" + splitretorno[1] + " 1/2" + splitretorno[4] + "^" + valor + "^" + splitretorno[3] + "^" + splitretorno[7];
            }
            else
            {
                try
                {
                    Domain.ProdutoModel Prd = new Domain.ProdutoModel();
                    Persistence.MPRepository rp = new Persistence.MPRepository();

                    var x = Dados.Split('^');

                    if (x[0] != null)
                    {
                        Prd.IdProduto = int.Parse(x[0]);

                        Prd.IdFonecedor = int.Parse(x[1]);

                        Prd.Tipo = int.Parse(x[2]);

                        Prd.Variacao = int.Parse(x[3]);

                        Prd.Tamanho = int.Parse(x[4]);

                    }
                    ret = rp.BuscarProduto(Prd);
                    var Borda = "";
                    if (x.Count() > 5)
                    {
                        switch (x[5])
                        {
                            case "1":
                                Borda = "Sem Borda";
                                break;
                            case "2":
                                Borda = "Borda de Cheedar";
                                break;
                            case "3":
                                Borda = "Borda de Catupiry";
                                break;

                        }

                        retorno = ret.IdProduto + "^" + ret.Nome + "^" + ret.Valor + "^" + ret.Tipo + "^" + Borda;
                    }
                    else
                    {
                        retorno = ret.IdProduto + "^" + ret.Nome + "^" + ret.Valor + "^" + ret.Tipo;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
                }
            }
            return retorno;

        }

        public string BuscarBebida(string Dados)
        {
            Domain.ProdutoModel ret = new Domain.ProdutoModel();
            //Dados = IdProduto|idvendedor|tipopizza|Variacoes(meia/inteira)|Tamanho
            string retorno = "";
            try
            {
                Domain.ProdutoModel Prd = new Domain.ProdutoModel();
                Persistence.MPRepository rp = new Persistence.MPRepository();

                if (Dados != null)
                {
                    Prd.IdProduto = int.Parse(Dados);

                    ret = rp.BuscarBebida(Prd);

                    retorno = ret.IdProduto + "^" + ret.Nome + "^" + ret.Valor + "^" + ret.Tipo;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
            }
            return retorno;

        }

        public ActionResult BuscaImagem(string Dados)
        {
            string ret = "";
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            List<Domain.ProdutoModel> prd = new List<ProdutoModel>();
            Persistence.MPRepository rp = new Persistence.MPRepository();

            var Id = Dados.Split('^');

            prd = rp.BuscarProdutoPorID(Id[0]);
            foreach (var x in prd)
            {
                if (x.FotoInteira != null)
                {
                    return File(x.FotoInteira, "image/jpg");
                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        public string FinalizarPedido(string Dados)
        {
            var x = Dados.Split(';');
            var tabela = "<table id='pedido' class='table table-striped'>";
            tabela = tabela + "<tr><th>Pizza</th><th>Quantidade</th><th>Preço Unitário</th></tr>";
            for (int i = 0; i < x.Count(); i++)
            {
                var t = x[i].Split('^');
                tabela = tabela + "<tr><td>" + t[1] + "</td><td>" + t[4] + "</td><td>" + t[2] + "</td></tr>";
            }
            tabela = tabela + "</table>";

            return tabela;

        }

        public string Requisicao(string Dados)
        {
            var x = Dados.Split('\n');
            var Pais = x[0];
            var Cidade = x[1];
            var Ip = x[2];

            string tabela = "OK";

            return tabela;

        }

        public ActionResult CadastroEsfiha(string Tipo, string Ingredientes, string Nome, string Valor, HttpPostedFileBase file)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            MemoryStream ms = new MemoryStream();

            try
            {
                if ((Tipo != "") && (!string.IsNullOrWhiteSpace(Tipo))
                    && (Ingredientes != "") && (!string.IsNullOrWhiteSpace(Ingredientes))
                    && (Nome != "") && (!string.IsNullOrWhiteSpace(Nome))
                    && (Valor != "") && (!string.IsNullOrWhiteSpace(Valor)))
                {
                    Domain.ProdutoModel Prd = new Domain.ProdutoModel();
                    Persistence.MPRepository rp = new Persistence.MPRepository();


                    Prd.FotoInteira = new byte[file.ContentLength];
                    file.InputStream.Read(Prd.FotoInteira, 0, file.ContentLength);

                    Prd.Tipo = 2;
                    Prd.Ingredientes = Ingredientes;
                    Prd.Nome = Nome;
                    Prd.Valor = decimal.Parse(Valor);
                    Prd.IdFonecedor = 1;

                    var x = rp.InserirProduto(Prd);

                    if (x)
                    {
                        ViewBag.ret = "Esfiha de " + Nome + " foi cadastrada com sucesso!";
                    }
                    else
                    {
                        ViewBag.ret = "Esta Esfiha de " + Nome + "  já está cadastrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
            }
            return View();
        }

        public ActionResult CadastroBebida(string Tipo, string Nome, string Valor, HttpPostedFileBase file)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            MemoryStream ms = new MemoryStream();

            try
            {
                if ((Tipo != "") && (!string.IsNullOrWhiteSpace(Tipo))
                    && (Nome != "") && (!string.IsNullOrWhiteSpace(Nome))
                    && (Valor != "") && (!string.IsNullOrWhiteSpace(Valor)))
                {
                    Domain.ProdutoModel Prd = new Domain.ProdutoModel();
                    Persistence.MPRepository rp = new Persistence.MPRepository();

                    if (file != null)
                    {
                        Prd.FotoInteira = new byte[file.ContentLength];
                        file.InputStream.Read(Prd.FotoInteira, 0, file.ContentLength);
                    }
                    Prd.Tipo = 3;
                    Prd.Nome = Nome;
                    Prd.Valor = decimal.Parse(Valor);
                    Prd.IdFonecedor = 1;

                    var x = rp.InserirProduto(Prd);

                    if (x)
                    {
                        ViewBag.ret = "A bebida " + Nome + " foi cadastrada com sucesso!";
                    }
                    else
                    {
                        ViewBag.ret = "Esta bebida: " + Nome + "  já está cadastrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
            }
            return View();
        }

        public ActionResult MostraPedido()
        {

            List<Domain.PedidoModel> ret = new List<Domain.PedidoModel>();
            Persistence.MPRepository rp = new Persistence.MPRepository();
            try
            {
                DateTime Data = DateTime.Now;

                var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

                if (IdFornecedor != null)
                {
                    ret = rp.MostraPedido(int.Parse(IdFornecedor.ToString()), Data);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
            }
            return View("MostraPedido", ret);
        }

        public string MostrarDetalhePedido(string Dados)
        {
            string Retorno = "";

            Domain.PedidoModel ret = new Domain.PedidoModel();
            Persistence.MPRepository rp = new Persistence.MPRepository();
            Domain.ClienteModel retCli = new Domain.ClienteModel();

            ret = rp.MostrarDetalhePedido(int.Parse(Dados));

            retCli = rp.BuscaDadosCliente(ret.IdCliente);


            Retorno = Retorno + "********************    Informações do pedido " + ret.NumeroPedido + "   ********************" + "<br>" + "<br>";
            Retorno = Retorno + " Numero do pedido:" + ret.NumeroPedido + "&nbsp&nbsp&nbsp&nbsp";
            Retorno = Retorno + " Data: " + ret.Data.ToString().Substring(0, 10) + "&nbsp&nbsp&nbsp&nbsp";
            Retorno = Retorno + " Hora: " + ret.Hora + "<br><br>";
            Retorno = Retorno + " Itens do Pedido:<br>";

            foreach (var item in ret.ItensPedido)
            {
                Retorno = Retorno + rp.BuscaNomeProdutos(item.IdProduto, item.Quantidade) + "<br>";
            }
            Retorno = Retorno + "<br>Total: R$ " + ret.ValorTotal + "<br>";
            Retorno = Retorno + " Observações: " + ret.Observacoes + "<br>";
            Retorno = Retorno + " Forma de Pagamento: " + ret.FormaPagamento + "<br>";

            if (ret.NotaFiscal == true)
            {
                Retorno = Retorno + " Nota Fiscal: SIM <br>";
            }
            else
            {
                Retorno = Retorno + " Nota Fiscal: NÃO <br>";
            }

            Retorno = Retorno + " Desconto: " + ret.Desconto + "<br><br>";

            Retorno = Retorno + "********************    Dados do cliente    ********************" + "<br>" + "<br>";
            Retorno = Retorno + " Endereço: " + retCli.Rua + ","+retCli.Numero+"<br>";
            Retorno = Retorno + " CEP: " + retCli.Cep + "<br>";
            Retorno = Retorno + " Telefone: " + retCli.Telefones + "<br>";
            Retorno = Retorno + " Email:" + retCli.Email + "<br>";
            Retorno = Retorno + " Complemento: " + retCli.Complemento + "<br>";
            Retorno = Retorno + " CPF: " + retCli.Cpf + "<br>";

            return Retorno;
        }

        public string DeParaMeioPagamento(int TipoPagamento)
        {
            string retorno = "";

            switch (TipoPagamento)
            {
                case 1:
                    retorno = "Cartão de Credito";
                    break;

                case 2:
                    retorno = "Cartão de Debito";
                    break;

                case 3:
                    retorno = "Dinheiro";
                    break;


            }

            return retorno;
        }

        public ActionResult ListaBebidas(string Dados)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            Domain.ProdutoModel prd = new Domain.ProdutoModel();
            Persistence.MPRepository rp = new Persistence.MPRepository();

            prd = rp.BuscarBebidas(int.Parse(Dados));

            if (prd.FotoInteira != null)
            {
                return File(prd.FotoInteira, "image/jpg");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Bebidas()
        {
            List<Domain.ProdutoModel> ret = new List<Domain.ProdutoModel>();
            Persistence.MPRepository rp = new Persistence.MPRepository();
            try
            {
                ret = rp.MostraBebidasFull();
            }
            catch (Exception ex)
            {
                ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
            }

            return View(ret);
        }

        public ActionResult ListarBebidasFull()
        {
            List<Domain.ProdutoModel> ret = new List<Domain.ProdutoModel>();
            Persistence.MPRepository rp = new Persistence.MPRepository();
            try
            {
                ret = rp.MostraBebidasFull();
            }
            catch (Exception ex)
            {
                ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
            }
            return View();
        }

        public ActionResult BuscaImagemBebida(string id)
        {
            string ret = "";
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            List<Domain.ProdutoModel> prd = new List<ProdutoModel>();
            Persistence.MPRepository rp = new Persistence.MPRepository();

            prd = rp.BuscarProdutoPorID(id);
            foreach (var x in prd)
            {
                if (x.FotoInteira != null)
                {
                    return File(x.FotoInteira, "image/jpg");
                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult Login(string Login, string Senha)
        {
            Domain.FornecedorModel ret = new Domain.FornecedorModel();
            Persistence.MPRepository rp = new Persistence.MPRepository();

            if (Login != null || Senha != null)
            {
                try
                {
                    //Valida Usuário
                    ret = rp.Login(Login, Senha);
                }

                catch (Exception ex)
                {
                    ViewBag.Ret = "Ops! Algo deu errado.";
                }

                if (ret.Usuario != null)
                {
                    //Armazena e Redireciona utilizador
                    if (ret.TipoUtilizador == true)
                    {
                        System.Web.HttpContext.Current.Session["SessionFornecedor"] = ret.IdUtilizador;
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["SessionFornecedor"] = 0;
                    }
                        return View("Index", ret);
                }

                else
                {
                    ViewBag.Ret = "Login ou senha incorreto!";
                    return View("Login", ret);
                }
            }
            else
            {
                return View("Login", ret);
            }
        }

        public bool ValidaStatus(string Dados)
        {
            bool retorno = false;

            Persistence.MPRepository rp = new Persistence.MPRepository();

            retorno = rp.ValidaStatus(int.Parse(Dados));

            return retorno;
        }

        public ActionResult AtualizarCardapio()
        {
            Persistence.MPRepository Rp = new Persistence.MPRepository();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
            List<Domain.ProdutoModel> prd = new List<Domain.ProdutoModel>();

            if (IdFornecedor != null)
            {
                prd = Rp.ListaProdutos(int.Parse(IdFornecedor.ToString()), 0).ToList();

            }

            return View(prd);
        }

        public string AlterarCardapio(string Dados, string Tipo)
        {
            string Retorno = "";

            Domain.ProdutoModel ret = new Domain.ProdutoModel();
            Persistence.MPRepository rp = new Persistence.MPRepository();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
            if (IdFornecedor != null)
            {
                ret = rp.BuscaDadosCardapio(int.Parse(Dados));

                //Retorno = "" + ret.Nome + "^" + ret.Ingredientes + "^" + ret.Valor + "^" + DeParaTipo("Tipo", ret.Tipo.ToString()) + "^" + DeParaTipo("Tipo", ret.Tamanho.ToString()) + "^" + DeParaTipo("Tipo", ret.Variacao.ToString()) + "^" +ret.Promocao+ "";
                Retorno = "" + ret.Nome + "^" + ret.Ingredientes + "^" + ret.Valor + "^" + DeParaTipo("SubVariacao", ret.SubVariacao.ToString()) + "^" + DeParaTipo("Tamanho", ret.Tamanho.ToString()) + "^" + DeParaTipo("Variacao", ret.Variacao.ToString()) + "^" + ret.Promocao + "^" + ret.Status + "^" + ret.IdFornecedorProduto + "^" + ret.Tipo + "^" + ret.ValorPromocao;
            }
            return Retorno;
        }

        public int DeParaTipo(string CaseDepara, string ValorDePara)
        {
            int Retorno = 0;

            switch (CaseDepara)
            {
                // Tipo = 1 - pizza 2- esfiha 3- refrigerante
                // Tamanho = 1 - grande - 2 media - 3 pequena
                // Variação = 1 - inteira  2 - meia
                // SubVarição = 1 - Doce 2 - Salgada

                /*
                 
                0 <option value="1">Doce</option>
                1 <option value="2">Salgada</option>

                0 <option value="1">Pequena</option>
                1 <option value="2">Media</option>
                2 <option value="3">Grande</option>

                0 <option value="1">Inteira</option>
                1 <option value="2">Meia</option>    
                
                */

                case "Tipo":
                    if (ValorDePara == "1")
                    {
                        // Retorno = "Pizza";
                        Retorno = 0;
                    }
                    else if (ValorDePara == "2")
                    {
                        // Retorno = "Esfiha";
                        Retorno = 1;
                    }
                    else if (ValorDePara == "3")
                    {
                        // Retorno = "Refrigerante";
                        Retorno = 2;
                    }
                    break;

                case "Tamanho":
                    if (ValorDePara == "3")
                    {
                        //Retorno = "Grande";
                        Retorno = 2;
                    }
                    else if (ValorDePara == "2")
                    {
                        // Retorno = "Media";
                        Retorno = 1;
                    }
                    else if (ValorDePara == "1")
                    {
                        //Retorno = "Pequena";
                        Retorno = 0;
                    }
                    break;

                case "SubVariacao":
                    if (ValorDePara == "1")
                    {
                        // Retorno = "Inteira";
                        Retorno = 0;
                    }
                    else if (ValorDePara == "2")
                    {
                        // Retorno = "Meia";
                        Retorno = 1;
                    }
                    break;

                case "Variacao":
                    if (ValorDePara == "1")
                    {
                        // Retorno = "Salgada";
                        Retorno = 1;
                    }
                    else if (ValorDePara == "2")
                    {
                        // Retorno = "Doce";
                        Retorno = 0;
                    }
                    break;

            }

            return Retorno;
        }

        public ActionResult AtualizarTempoPreparo()
        {
            string retorno = "";

            return View("AtualizarTempoPreparo", retorno);
        }

        public ActionResult AtualizarAreaEntrega()
        {
            List<Domain.FornecedorCepModel> retorno = new List<FornecedorCepModel>();
            Persistence.MPRepository rp = new Persistence.MPRepository();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                retorno = rp.BuscarCep(int.Parse(IdFornecedor.ToString()));
            }

            return View(retorno);
        }

        public ActionResult AtualizarCombo()
        {
            return View();
        }

        public ActionResult RelatoriosVenda()
        {
            string retorno = "";

            return View("RelatoriosVenda", retorno);
        }

        public ActionResult CadastroProdutos()
        {
            string retorno = "";

            return View("CadastroProdutos", retorno);
        }

        public ActionResult CadastroFuncionarios()
        {
            MPRepository rp = new MPRepository();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
            List<Domain.FornecedorDeliveryModel> retorno = new List<FornecedorDeliveryModel>();
            if (IdFornecedor != null) 
            {
                retorno = rp.ListaFuncionario(int.Parse(IdFornecedor.ToString()));
            }
            return View(retorno);
        }

        public ActionResult Cep()
        {
            return View();
        }

        public string PesquisarCep(string CEP)
        {
            Util u = new Util();

            var x = u.Main();

            CEP = CEP.Replace("-", "");
            Persistence.MPRepository rp = new Persistence.MPRepository();
            List<Domain.FornecedorModel> Ret = new List<Domain.FornecedorModel>();
            string stringretorno = "";
            Ret = rp.BuscarCepFornecedor(CEP);

            foreach (var fornecedor in Ret)
            {
                if (stringretorno != "")
                {
                    stringretorno = stringretorno + "^" + fornecedor.IdUtilizador;
                }
                else
                {
                    stringretorno = fornecedor.IdUtilizador.ToString();
                }
            }

            return stringretorno;
        }

        public ActionResult ListaFornecedor(string ID)
        {
            Persistence.MPRepository rp = new Persistence.MPRepository();
            List<Domain.FornecedorModel> Ret = new List<Domain.FornecedorModel>();

            Ret = rp.ListarFornecedor(ID);

            return View(Ret);
        }

        public ActionResult CadastroFornecedor()
        {
            Persistence.MPRepository rp = new Persistence.MPRepository();
            List<Domain.FornecedorModel> Ret = new List<Domain.FornecedorModel>();

            Ret = rp.ListarFornecedor("");

            return View(Ret);
        }

        public ActionResult CadastroUsuario(string ID, string nome, string cnpj, string NomeContato, string Cep, string Telefone, string Rua, string Numero, string Email, string Usuario,
             string Senha, string Diretorio, string Referencia, string Bairro, string Cidade, string IngredientePadrao, string VendaOnline, string Status, string Voucher)
        {

            Persistence.MPRepository rp = new Persistence.MPRepository();
            Domain.FornecedorModel Ret = new Domain.FornecedorModel();

            if (ID == null)
            {
                return View(Ret);
            }

            if ((ID != null) && (string.IsNullOrWhiteSpace(nome)) && (string.IsNullOrWhiteSpace(cnpj)))
            {
                Ret = rp.BuscaFornecedor(ID);
            }
            else
            {
                if (ID != "0")
                {
                    Ret.IdUtilizador = int.Parse(ID);
                }

                Ret.Bairro = Bairro;
                Ret.Cep = Cep;
                Ret.Cidade = Cidade;
                Ret.Cnpj = cnpj;
                Ret.Diretorio = Diretorio;
                Ret.Email = Email;
                Ret.IngredientePadrao = false;
                Ret.Nome = nome;
                Ret.NomeContato = NomeContato;
                Ret.Numero = int.Parse(Numero);
                Ret.Referencia = Referencia;
                Ret.Rua = Rua;
                Ret.Senha = Senha;
                Ret.Status = false;
                Ret.Telefone = Telefone;
                Ret.Usuario = Usuario;
                Ret.VendaOnline = false;
                Ret.Voucher = "no";

                if ((string.IsNullOrWhiteSpace(nome)) || (string.IsNullOrWhiteSpace(cnpj)) || (string.IsNullOrWhiteSpace(NomeContato)) || (string.IsNullOrWhiteSpace(Telefone))
                 || (string.IsNullOrWhiteSpace(Rua)) || (string.IsNullOrWhiteSpace(Numero)) || (string.IsNullOrWhiteSpace(Email)) || (string.IsNullOrWhiteSpace(Usuario))
                 || (string.IsNullOrWhiteSpace(Senha)) || (string.IsNullOrWhiteSpace(Bairro))
                 )
                {
                    ViewBag.Retorno = "Preencha todos os campos.";
                    return View(Ret);
                }
                string x = rp.CadastraAlteraFornecedor(Ret);

                ViewBag.Retorno = x;
            }

            return View(Ret);
        }

        public string LogoutSessao()
        {
            string retorno = "";

            System.Web.HttpContext.Current.Session.Remove("SessionFornecedor");

            System.Web.HttpContext.Current.Session.Remove("ManualCarrinho");
            return retorno;
        }

        public string RetornaCarrinho()
        {
            string Retorno = (string)System.Web.HttpContext.Current.Session["Carrinho"];

            return Retorno;
        }

        public string DadoSession(string Dados, string Acao)
        {
            string retorno = "";

            if (Acao == "Adicionar")
            {
                string Carrinho = (string)System.Web.HttpContext.Current.Session["Carrinho"];
                if (string.IsNullOrEmpty(Carrinho))
                {
                    Carrinho = Dados;
                }
                else
                {
                    Carrinho = Carrinho + "=" + Dados;
                }
                System.Web.HttpContext.Current.Session["Carrinho"] = Carrinho;
            }
            else if (Acao == "Excluir")
            {
                string newcarrinho = "";
                string Carrinho = (string)System.Web.HttpContext.Current.Session["Carrinho"];
                if (!string.IsNullOrEmpty(Carrinho))
                {
                    var ListaProdutos = Carrinho.Split('=');
                    var ProdutoRequisicao = Dados.Split('|');

                    for (int x = 0; x < ListaProdutos.Length; x++)
                    {
                        var Produto = ListaProdutos[x].Split('|');
                        if (Produto[1] != ProdutoRequisicao[1])
                        {
                            if (newcarrinho == "")
                            {
                                newcarrinho = ListaProdutos[x];
                            }
                            else
                            {
                                newcarrinho = newcarrinho + "=" + ListaProdutos[x];
                            }
                        }
                    }

                    System.Web.HttpContext.Current.Session["Carrinho"] = newcarrinho;
                }
            }
            else
            {
                string Carrinho = (string)System.Web.HttpContext.Current.Session["Carrinho"];
                if (!string.IsNullOrEmpty(Carrinho))
                {
                    string newcarrinho = "";
                    var ListaProdutos = Carrinho.Split('=');
                    var ProdutoRequisicao = Dados.Split('|');

                    for (int x = 0; x < ListaProdutos.Length; x++)
                    {
                        var splitproduto = ListaProdutos[x];
                        var Produto = splitproduto.Split('|');

                        if (Produto[1] == ProdutoRequisicao[1])
                        {
                            if (newcarrinho == "")
                            {
                                newcarrinho = ProdutoRequisicao[0] + "|" + ProdutoRequisicao[1] + "|" + ProdutoRequisicao[2];
                            }
                            else
                            {
                                newcarrinho = newcarrinho + "=" + ProdutoRequisicao[0] + "|" + ProdutoRequisicao[1] + "|" + ProdutoRequisicao[2];
                            }
                        }
                        else
                        {
                            if (newcarrinho == "")
                            {
                                newcarrinho = splitproduto;
                            }
                            else
                            {
                                newcarrinho = newcarrinho + "=" + splitproduto;
                            }
                        }
                    }

                    System.Web.HttpContext.Current.Session["Carrinho"] = newcarrinho;
                }
            }
            return retorno;
        }

        public string CarregaTemposDados()
        {
            string Retorno = "";

            Persistence.MPRepository Rp = new Persistence.MPRepository();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
            if (IdFornecedor != null)
            {
                var x = Rp.BuscaFornecedor(IdFornecedor.ToString());

                Retorno = x.TempoEntrega + "||" + x.TempoPreparo;
            }
            else
            {
                Retorno = "ERRAUT";
            }


            return Retorno;
        }

        public bool AlterarTempoCadastro(string TempoPreparo, string TempoEntrega)
        {
            bool Retorno = false;

            Persistence.MPRepository Rp = new Persistence.MPRepository();
            //pegando id fornecedor na sessao
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
            Domain.FornecedorModel Fornecedor = new FornecedorModel();

            if (IdFornecedor != null)
            {
                //update no forneedor com o tempo de preparo
                Retorno = Rp.UpdateFornecedor(int.Parse(IdFornecedor.ToString()), Fornecedor, "TempoPreparoEntrega", TempoEntrega, TempoPreparo);
            }
            return Retorno;
        }

        public string AtualizaBusca(string Dados, string Tipo, string Status, string Variacao, string Tamanhos, string SubVariacao)
        {
            string retorno = "";

            List<Domain.ProdutoModel> ret = new List<Domain.ProdutoModel>();
            try
            {
                Domain.ProdutoModel Prd = new Domain.ProdutoModel();
                Persistence.MPRepository rp = new Persistence.MPRepository();
                var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

                if (IdFornecedor != null)
                {
                    ret = rp.AtualizaBusca(Dados, int.Parse(Tipo), Status, int.Parse(IdFornecedor.ToString()), Variacao, Tamanhos, SubVariacao);

                    foreach (var x in ret)
                    {
                        if (retorno == "")
                        {
                            retorno = "" + x.IdProduto + "^" + x.Nome + "^" + x.Valor + "^" + x.Ingredientes + "";
                        }
                        else
                        {
                            retorno = retorno + "||" + x.IdProduto + "^" + x.Nome + "^" + x.Valor + "^" + x.Ingredientes + "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Ret = "Ops! Algo deu errado. Nosso suporte está verificando";
            }

            return retorno;

        }

        public string BuscaTipoProdutos()
        {
            string retorno = "";

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                Persistence.MPRepository rp = new Persistence.MPRepository();
                List<ProdutoModel> ret = new List<ProdutoModel>();

                retorno = rp.BuscaTipoProdutos(int.Parse(IdFornecedor.ToString()));

            }

            return retorno;
        }

        public bool AtualizarCadastroProduto(string IdProduto, string Promocao, string Status, string variacoes, string Tamanho, string Tipo, string Valor, string Ingredientes, string Sabor, string SubVariacao, string NovoValor)
        {
            bool retorno = false;

            Persistence.MPRepository rp = new Persistence.MPRepository();
            ProdutoModel ret = new ProdutoModel();

            if (IdProduto == "")
            {
                IdProduto = "0";
            }

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null))
            {
                ret.IdFonecedor = int.Parse(IdFornecedor.ToString());
                ret.IdFornecedorProduto = int.Parse(IdProduto);
                ret.Nome = Sabor;
                ret.Ingredientes = Ingredientes;
                ret.Valor = decimal.Parse(Valor);
                ret.Tipo = int.Parse(Tipo);
                ret.Status = bool.Parse(Status);
                ret.Promocao = bool.Parse(Promocao);
                if (NovoValor != "")
                {
                    ret.ValorPromocao = decimal.Parse(NovoValor);
                }
                var x = rp.AtualizarCadastroProduto(ret);
            }

            return retorno;
        }

      
        public string AtualizaBuscaCEP(string Dados, string Tipo)
        {
            string TabelaRetorno = "";

            Persistence.MPRepository rp = new Persistence.MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null) && (!string.IsNullOrWhiteSpace(Dados)))
            {
                var ret = rp.AtualizaBuscaCEP(Tipo, Dados, int.Parse(IdFornecedor.ToString()));

                TabelaRetorno = "<table id='TabelaProdutos' style='margin-top: 30px; margin-left:440px; text-align:center; border-radius: 5px 5px 5px 5px; border: solid 1px #000; height: auto; width: 650px; margin: 2px 2px 2px 2px;'>";
                TabelaRetorno = TabelaRetorno + "<thead>" +
                    "<th>CEP</th>" +
                    "<th>Rua</th>" +
                    "<th>Bairro</th>" +
                    "<th>Taxa</th>" +
                    "<th></th>" +
                     "</thead>";

                if (ret.Count() > 0)
                {
                    foreach (var x1 in ret)
                    {
                        if (x1.Cep == "-1")
                        {
                            TabelaRetorno = "<table id='TabelaProdutos' style='margin-top: 30px; margin-left:440px; text-align:center; border-radius: 5px 5px 5px 5px; border: solid 1px #000; height: auto; width: 650px; margin: 2px 2px 2px 2px;''>";

                            TabelaRetorno = TabelaRetorno + "<tr style='border-bottom: 1px  solid #000;'>";
                            TabelaRetorno = TabelaRetorno + "<td style='border-bottom: 1px  solid #000;'>";
                            TabelaRetorno = TabelaRetorno + "<div class='saborvalor' style='width: 100%;'>";
                            TabelaRetorno = TabelaRetorno + "O CEP " + Dados + " já esta cadastrado para este estabelecimento.";
                            TabelaRetorno = TabelaRetorno + "</div>";
                            TabelaRetorno = TabelaRetorno + "</td>";
                            TabelaRetorno = TabelaRetorno + "</tr>";
                        }
                        else
                        {
                            TabelaRetorno = TabelaRetorno + "<tbody><tr style='border-bottom: 1px  solid #000; text-align: center; margin: 2px 2px 2px 2px;'>";
                            TabelaRetorno = TabelaRetorno + "<td onclick='this.disabled=true'>" + x1.Cep + "</td>";
                            TabelaRetorno = TabelaRetorno + "<td onclick='this.disabled=true'>" + x1.Rua + "</td>";
                            TabelaRetorno = TabelaRetorno + "<td onclick='this.disabled=true'>" + x1.Bairro + "</td>";
                            TabelaRetorno = TabelaRetorno + "<td onclick='this.disabled=true'><input type='text'  onkeypress='return mascara(event,this)' size='4' style='width:40px;' id='txtTaxa" + x1.Cep + "' ></td>";
                            TabelaRetorno = TabelaRetorno + "<td><input type='button' value='Adicionar'onclick='adicionarCEP()'></td>";
                            TabelaRetorno = TabelaRetorno + "</tr></tbody>";
                        }
                    }
                    TabelaRetorno = TabelaRetorno + "</table>";
                }
                else
                {
                    TabelaRetorno = "<table id='TabelaProdutos' style='margin-left:440px; text-align:center; border-radius: 5px 5px 5px 5px; border: solid 1px #000; height: auto; width: 650px; margin: 2px 2px 2px 2px;''>";

                    TabelaRetorno = TabelaRetorno + "<tr style='border-bottom: 1px  solid #000;'>";
                    TabelaRetorno = TabelaRetorno + "<td style='border-bottom: 1px  solid #000;'>";
                    TabelaRetorno = TabelaRetorno + "<div class='saborvalor' style='width:100%;'>";
                    TabelaRetorno = TabelaRetorno + "Nenhum Registro encontrado.";
                    TabelaRetorno = TabelaRetorno + "</div>";
                    TabelaRetorno = TabelaRetorno + "</td>";
                    TabelaRetorno = TabelaRetorno + "</tr>";

                    TabelaRetorno = TabelaRetorno + "</table>";
                }
            }

            return TabelaRetorno;
        }

        public string adicionarCEP(string Dados, string Taxa)
        {
            string Retorno = "";

            Persistence.MPRepository rp = new Persistence.MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null) && (!string.IsNullOrWhiteSpace(Dados)) && (!string.IsNullOrWhiteSpace(Taxa)))
            {
                Retorno = rp.InserirCEP(Dados, int.Parse(IdFornecedor.ToString()), Taxa);
            }

            return Retorno;
        }

        public string RemoveCEP(string Dados)
        {
            string Retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                Retorno = rp.RemoverCepFornecedor(int.Parse(IdFornecedor.ToString()), Dados);
            }

            return Retorno;
        }

        public string BuscaProdutos(string Dados)
        {
            string Retorno = "";

            MPRepository rp = new MPRepository();
            List<Domain.ProdutoModel> Ret = new List<ProdutoModel>();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                Ret = rp.BuscarProdutosPorFornecedor(int.Parse(IdFornecedor.ToString()), Dados);
            }
            int contador = 0;

            foreach (var x in Ret)
            {
                contador = contador + 1;
                if (contador == Ret.Count())
                {
                    Retorno = Retorno + x.Nome + "^" + x.Ingredientes + "^" + x.Valor + "^" + x.IdFornecedorProduto;
                }
                else
                {
                    Retorno = Retorno + x.Nome + "^" + x.Ingredientes + "^" + x.Valor + "^" + x.IdFornecedorProduto + "|";
                }
            }

            return Retorno;
        }

        public string SalvaCombo(string Dados, string NomeCombo, string ValorCombo)
        {
            string retorno = "";
            MPRepository rp = new MPRepository();
            string Ids = "";

            var split = Dados.Split('|');

            for (int i = 0; i < split.Length; i++)
            {
                var Concat = split[i].Split('^');
                Ids = Ids + Concat[3] + ";";
            }

            Ids = Ids.Substring(0, Ids.Length - 1);

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                var x = rp.SalvarCombo(Ids, int.Parse(IdFornecedor.ToString()), NomeCombo, ValorCombo);
                if (x == "")
                {
                    retorno = "Sucesso";
                }
                else
                {
                    retorno = x;
                }
            }

            return retorno;
        }

        public string MostraCombos()
        {
            string retorno = "<table id='Combos' style='margin-top: 30px; margin-left:440px; text-align:center; border-radius: 5px 5px 5px 5px; border: solid 1px #000; height: auto; width: 650px; margin: 2px 2px 2px 2px;'>" +
            "<thead><th>Nome</th><th>Valor</th>" +
            "<th>Produto 1</th><th>Produto 2</th><th>Produto 3</th><th>Produto 4</th><th>Produto 5</th>" +
            "<th>Status</th><th>Excluir</th></thead>";
            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                var x = rp.MostraCombos(int.Parse(IdFornecedor.ToString()));

                foreach (var ret in x)
                {
                    retorno = retorno + "<tbody><tr>";
                    retorno = retorno + "<td>" + ret.NomeCombo + "</td>";
                    retorno = retorno + "<td>" + ret.ValorCombo + "</td>";

                    foreach (var prd in ret.Produtos)
                    {
                        retorno = retorno + "<td>" + prd.Nome + "</td>";
                    }

                    if (ret.Produtos.Count != 5)
                    {
                        var td = 5 - ret.Produtos.Count;

                        for (int count = 0; count < td; count++)
                        {
                            retorno = retorno + "<td></td>";
                        }
                    }
                    var tds =

                    retorno = retorno + "<td>" + ret.Status + "</td>";
                    retorno = retorno + "<td onclick='ExcluirCombo(" + ret.IdCombo + ")'>X</td>";
                    retorno = retorno + "</tr></tbody>";
                }
            }
            retorno = retorno + "</table>";
            return retorno;
        }

        public string ExcluirCombo(string Dados)
        {
            string retorno = "";
            MPRepository rp = new MPRepository();

            //delete combo
            var x = rp.DeletarCombo(int.Parse(Dados));

            return retorno;
        }

        public string GerarRelatorio(string DataInicio, string DataFim, string Status, string Produto, string Valor, string Fornecedor, string TipoProduto)
        {
            string retorno = "";
            List<Domain.PedidoModel> ret = new List<Domain.PedidoModel>();
            MPRepository rp = new MPRepository();
            string tBody = "";
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
            decimal ValorTotal = 0;
            if (IdFornecedor != null)
            {
                if (Valor == "Selecione...")
                {
                    Valor = "";
                }
                if (Fornecedor == "Selecione...")
                {
                    Fornecedor = "0";
                }
                if (TipoProduto == "#")
                {
                    TipoProduto = "0";
                }
                ret = rp.GeraRelatorio(int.Parse(IdFornecedor.ToString()), DataInicio, DataFim, Status, Produto, Valor, int.Parse(Fornecedor), int.Parse(TipoProduto));

                foreach (var k in ret)
                {
                    DateTime dt = DateTime.ParseExact(k.Data.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    string s = dt.ToString("dd/MM/yyyy");

                    string RetStatus = "";
                    if (k.Status == "1")
                    {
                        RetStatus = "Finalizado";
                    }
                    else if (k.Status == "2")
                    {
                        RetStatus = "Cancelado";
                    }
                    //Monta HTML baseado no select 
                    tBody = tBody + "<tbody><tr><td>" + k.NumeroPedido + "</td><td>" + RetStatus + "</td><td>" + s + "</td><td>" + k.Hora + "</td><td>" + k.ValorTotal + "</td><td>" + NomesProdutos(k.ItensPedido) + "</td><td>" + rp.BuscaNomeDelivery(k.IdDelivery) + "</td></tr><tbody>";
                    ValorTotal = ValorTotal + decimal.Parse(k.ValorTotal);
                }
                //Monta tabela do relatorio 
                retorno = "<div id='Cabecalho' class='Cabecalho'>Relatório de Pedidos</div >" +
                "<br><br><label>Quantidade de Pedidos: " + ret.Count() + "</label>&nbsp&nbsp&nbsp<label>Valor Total: " + ValorTotal + "</label>" +
                "&nbsp&nbsp&nbsp<label>Gerado em: " + DateTime.Now + "</label>&nbsp&nbsp&nbsp<input type='button' value='Excel' onclick='GerarExcel()'><br><br>";
                retorno = retorno + "<table id='tblRelatorio' class='tblRelatorio' ><thead><th>Nº</th><th>Status</th><th>Data</th><th>Hora</th><th>Valor</th><th>Produtos</th><th>Entregador</th></thead>";
                retorno = retorno + tBody;
                retorno = retorno + "</table>";
            }

            return retorno;
        }

        public string NomesProdutos(List<Domain.ItemPedidoModel> ItensPedido)
        {
            string retorno = "";
            MPRepository rp = new MPRepository();

            foreach (var item in ItensPedido)
            {
                if (retorno == "")
                {
                    retorno = item.Quantidade + "x&nbsp" + rp.BuscaNomeProduto(item.IdProduto) + "<br>";
                }
                else
                {
                    retorno = retorno + item.Quantidade + "x&nbsp" + rp.BuscaNomeProduto(item.IdProduto) + "<br>";
                }
            }

            return retorno;
        }

        public string ExibeFoto()
        {
            string retorno = "";

            return retorno;
        }

        public string CadastraFuncionario(string Nome, string Cpf, string TpTransporte, string cnh, string Placa)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                //insert funcionario
                retorno = rp.CadastraFuncionario(int.Parse(IdFornecedor.ToString()), Nome, Cpf, TpTransporte, cnh, Placa);
            }
            return retorno;
        }

        public ActionResult GeraProdutoFornecedor()
        {
            return View();
        }

        public string ListaTiposProduto()
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            int i = 0;

            retorno = rp.BuscaTipoProdutos(i);

            return retorno;
        }

        public string ListaTodosFornecedor()
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            retorno = rp.ListaFornecedores();

            return retorno;
        }

        public string CarregaTamanho(string Tipo)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null))
            {
                //metodo pega tamanho
                retorno = rp.GetTamanhoTipo(Tipo, int.Parse(IdFornecedor.ToString()));
            }
            return retorno;
        }

        public string CarregaTamanhoId(string Tipo)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null))
            {
                retorno = rp.GetTamanhoTipoId(Tipo, int.Parse(IdFornecedor.ToString()));
            }
            return retorno;
        }

        public string CarregaBordas(string Tipo)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null))
            {
                retorno = rp.GetBordas(int.Parse(IdFornecedor.ToString()));
            }
            return retorno;
        }

        public string CarregaSubVariacao(string Tipo)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null))
            {
                //pega a subvariacao de acordo com o tipo
                retorno = rp.GetCarregaSubVariacao(Tipo);
            }
            return retorno;
        }

        public string CarregaVariacao(string Tipo)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if ((IdFornecedor != null))
            {
                //pega variacao de acordo tipo
                retorno = rp.GetCarregaVariacao(Tipo);
            }
            return retorno;
        }

        public string SalvarProdutosFornecedor(string Tamanhos, string Fornecedor, string TipoProduto, string SubVariacao)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            retorno = rp.SalvarProdutosFornecedor(Tamanhos, Fornecedor, TipoProduto, SubVariacao);

            return retorno;
        }

        public JsonResult AutocompleteSuggestions(string term, string SltTipoProduto)
        {
            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (SltTipoProduto == "#")
            {
                SltTipoProduto = "0";
            }

            var namelist = rp.GetProdutosFornecedor(int.Parse(IdFornecedor.ToString()), term, int.Parse(SltTipoProduto));

            return Json(namelist, JsonRequestBehavior.AllowGet);
        }

        //public Chart getGrafico(string DtInicio, string DtFim)
        //{
        //    MPRepository rp = new MPRepository();
        //    if (string.IsNullOrWhiteSpace(DtInicio))
        //    {
        //        string DtDiaPrimeiro = DateTime.Now.Date.ToString("dd/MM/yyyy");

        //        DtDiaPrimeiro = DtDiaPrimeiro.Substring(2, 8);

        //        DtDiaPrimeiro = "01" + DtDiaPrimeiro;

        //        DtInicio = DtDiaPrimeiro.ToString();
        //    }

        //    if (string.IsNullOrWhiteSpace(DtFim))
        //    {
        //        DtFim = DateTime.Now.Date.ToString("dd/MM/yyyy");
        //    }

        //    List<Domain.PedidoGraficoModel> ListaProdutos = new List<PedidoGraficoModel>();
        //    var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
        //    if (IdFornecedor != null)
        //    {
        //        ListaProdutos = rp.GeraGrafico(int.Parse(IdFornecedor.ToString()), DtInicio, DtFim, "", "", "");
        //    }
        //    if (ListaProdutos.Count() > 0)
        //    {
        //        var myChart = new Chart(width: 800, height: 400)
        //         .AddTitle("Os 5 Produtos Vendidos")
        //         .AddSeries(chartType: "column")
        //         .DataBindTable(dataSource: ListaProdutos, xField: "Data")
        //         .Write();
        //        return myChart;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public string ListaFuncionario()
        {
            string retorno = "";
            MPRepository rp = new MPRepository();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];
            List<Domain.FornecedorDeliveryModel> Listaretorno = new List<FornecedorDeliveryModel>();
            if (IdFornecedor != null)
            {
                Listaretorno = rp.ListaFuncionario(int.Parse(IdFornecedor.ToString()));

                foreach (var f in Listaretorno)
                {
                    if (retorno == "")
                    {
                        retorno = f.IdFornecedorTransporte + "^" + f.Nome;
                    }
                    else
                    {
                        retorno = retorno + "|" + f.IdFornecedorTransporte + "^" + f.Nome;
                    }
                }
            }
            return retorno;
        }

        public string AlteraStatusPedido(string Pedido, string Status)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            int IdStatus = DeParaStatus(Status);

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                retorno = rp.AlterarStatusPedido(int.Parse(Pedido), IdStatus, int.Parse(IdFornecedor.ToString()));
            }

            return retorno;
        }

        public int DeParaStatus(string Status)
        {
            int retorno = 9;

            switch (Status)
            {
                case "Em Preparo":
                    retorno = 3;
                    break;

                case "Aguardando Atendimento":
                    retorno = 0;
                    break;

                case "Saiu Para Entrega":
                    retorno = 1;
                    break;
                case "Cancelado":
                    retorno = 2;
                    break;
                case "Finalizado":
                    retorno = 1;
                    break;
            }

            return retorno;
        }

        public ActionResult ExportarExcel(string Table)
        {

            var spli = Table.Split('|');

            bool retorno = false;

            var ListaPedido = new List<PedidoModel>();
            var ListaPedidoExcel = new List<PedidoExcelModel>();
            DataTable dt = new DataTable();
            MPRepository rp = new MPRepository();

            Util u = new Util();

            var x = u.Main();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                string Valor = spli[4];
                if (Valor == "Selecione...")
                {
                    Valor = "";
                }
                string Fornecedor = spli[5];
                if (Fornecedor == "Selecione...")
                {
                    Fornecedor = "0";
                }
                string TipoProduto = spli[6];
                if ((TipoProduto == "") || (TipoProduto == "#"))
                {
                    TipoProduto = "0";
                }
                //Gera relatorio
                ListaPedido = rp.GeraRelatorio(int.Parse(IdFornecedor.ToString()), spli[0], spli[1], spli[2], spli[3], Valor, int.Parse(Fornecedor), int.Parse(TipoProduto));

                foreach (var Pedido in ListaPedido)
                {   
                    //Adiciona o relatorio para o objeto(model)
                    Domain.PedidoExcelModel Obj = new PedidoExcelModel();

                    var splitdt = Pedido.Data.ToString().Split(' ');

                    Obj.NumeroPedido = Pedido.NumeroPedido;
                    Obj.Cep = Pedido.Cep;
                    Obj.Rua = Pedido.Rua;
                    Obj.Numero = Pedido.Numero;
                    Obj.Data = splitdt[0];
                    Obj.Hora = Pedido.Hora;
                    Obj.Desconto = Pedido.Desconto;
                    Obj.Observacoes = Pedido.Observacoes;
                    Obj.Troco = Pedido.Troco;
                    Obj.ValorTotal = Pedido.ValorTotal;


                    if (Pedido.NotaFiscal)
                    {
                        Obj.NotaFiscal = "SIM";
                    }
                    else
                    {
                        Obj.NotaFiscal = "NAO";
                    }


                    if (Pedido.FormaPagamento == "1")
                    {
                        Obj.FormaPagamento = "Cartao de Credito";
                    }
                    if (Pedido.FormaPagamento == "2")
                    {
                        Obj.FormaPagamento = "Cartao de Debito";
                    }
                    if (Pedido.FormaPagamento == "3")
                    {
                        Obj.FormaPagamento = "Dinheiro";
                    }


                    if (Pedido.Status == "1")
                    {
                        Obj.Status = "Finalizado";
                    }
                    else
                    {
                        Obj.Status = "Cancelado";
                    }

                    foreach (var Item in Pedido.ItensPedido)
                    {
                        if (string.IsNullOrWhiteSpace(Obj.ItensPedido))
                        {
                            Obj.ItensPedido = rp.BuscaNomeProdutos(Item.IdProduto, Item.Quantidade).Replace("&nbsp&nbsp", "  ");
                        }
                        else
                        {
                            Obj.ItensPedido = Obj.ItensPedido + "  - " + rp.BuscaNomeProdutos(Item.IdProduto, Item.Quantidade).Replace("&nbsp&nbsp", "  ");
                        }
                    }

                    //Adiciona valores p lista
                    ListaPedidoExcel.Add(Obj);
                }

                //Converte a lista p/ datatable (grid)
                dt = ConvertToDataTable(ListaPedidoExcel);

                //Gera excel a partir do datatable
                retorno = u.ExportarParaExcel(dt);
            }
            return View();
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public ActionResult GerarPDF(string Table)
        {

            var spli = Table.Split('|');

            string retorno = "";
            string tBody = "";
            decimal ValorTotal = 0;
            var ListaPedido = new List<PedidoModel>();

            MPRepository rp = new MPRepository();

            Util u = new Util();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                string Valor = spli[4];
                if (Valor == "Selecione...")
                {
                    Valor = "";
                }
                string Fornecedor = spli[5];
                if (Fornecedor == "Selecione...")
                {
                    Fornecedor = "0";
                }
                string TipoProduto = spli[6];
                if ((TipoProduto == "") || (TipoProduto == "#"))
                {
                    TipoProduto = "0";
                }
                var ret = rp.GeraRelatorio(int.Parse(IdFornecedor.ToString()), spli[0], spli[1], spli[2], spli[3], Valor, int.Parse(Fornecedor), int.Parse(TipoProduto));

                foreach (var k in ret)
                {
                    DateTime dt = DateTime.ParseExact(k.Data.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    string s = dt.ToString("dd/MM/yyyy");

                    string RetStatus = "";
                    if (k.Status == "1")
                    {
                        RetStatus = "Finalizado";
                    }
                    else if (k.Status == "2")
                    {
                        RetStatus = "Cancelado";
                    }

                    tBody = tBody + "<tbody><tr><td>" + k.NumeroPedido + "</td><td>" + RetStatus + "</td><td>" + s + "</td><td>" + k.Hora + "</td><td>" + k.ValorTotal + "</td><td>" + NomesProdutos(k.ItensPedido) + "</td><td>" + rp.BuscaNomeDelivery(k.IdDelivery) + "</td></tr><tbody>";
                    ValorTotal = ValorTotal + decimal.Parse(k.ValorTotal);
                }

                retorno = "<div id='Cabecalho' class='Cabecalho'>Relatório de Pedidos</div >" +
                "<br><br><label>Quantidade de Pedidos: " + ret.Count() + "</label>&nbsp&nbsp&nbsp<label>Valor Total: " + ValorTotal + "</label>" +
                "&nbsp&nbsp&nbsp<label>Gerado em: " + DateTime.Now + "</label>&nbsp&nbsp&nbsp<input type='button' value='Excel' onclick='GerarExcel()'><br><br>";
                retorno = retorno + "<table id='tblRelatorio' class='tblRelatorio' ><thead><th>Numero Pedido</th><th>Status</th><th>Data</th><th>Hora</th><th>Valor</th><th>Produtos</th><th>Entregador</th></thead>";
                retorno = retorno + tBody;
                retorno = retorno + "</table>";
            }
            return new RazorPDF.PdfResult(retorno, "RelatoriosVenda");
        }

        public string BuscarPedido(string Dados)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var x1 = rp.BuscarPedido(Dados);

            retorno = "<table><thead>";
            retorno = retorno + "<tr>";
            retorno = retorno + "<th>Numero</th>";
            retorno = retorno + "<th>Data</th>";
            retorno = retorno + "<th>Status</th>";
            retorno = retorno + "<th></th>";
            retorno = retorno + "</tr>";
            retorno = retorno + "</thead>";
            retorno = retorno + "<tbody>";

            if (x1.Status == "0")
            {
                retorno = retorno + "<tr id='LineWhite'>";
                retorno = retorno + "<td>" + x1.NumeroPedido + "</td>";
                retorno = retorno + "<td>" + x1.Data.ToString("dd/MM/yyyy") + " " + x1.Hora + "</td>";
                retorno = retorno + "<td>";
                retorno = retorno + "<select name='Tipo' id=" + x1.NumeroPedido + " onchange='AlteraPedido('this')' id=" + x1.NumeroPedido + ">";
                retorno = retorno + "<option value=" + x1.NumeroPedido + ">Aguardando Atendimento</option>";
                retorno = retorno + "<option value=" + x1.NumeroPedido + ">Em Preparo</option>";
                retorno = retorno + "<option value=" + x1.NumeroPedido + ">Saiu Para Entrega</option>";
                retorno = retorno + "<option value=" + x1.NumeroPedido + ">Cancelado</option>";
                retorno = retorno + "</select>";
                retorno = retorno + "</td>";
                retorno = retorno + "<td><a href='#' onclick='MostrarDetalhePedido(" + x1.IdPedidoFull + ")'>Detalhes...<a/></td>";
                retorno = retorno + "</tr>";
            }
            else if (x1.Status == "3")
            {
                retorno = retorno + "<tr id='LineGreen'>";
                retorno = retorno + "<td>" + x1.NumeroPedido + "</td>";
                retorno = retorno + "<td>" + x1.Data.ToString("dd/MM/yyyy") + " " + x1.Hora + "</td>";
                retorno = retorno + "<td>";
                retorno = retorno + "<select name='Tipo' id=" + x1.NumeroPedido + " onchange='AlteraPedido(this)' >";
                retorno = retorno + "<option value=" + x1.NumeroPedido + ">Em Preparo</option>";
                retorno = retorno + "<option value=" + x1.NumeroPedido + ">Saiu Para Entrega</option>";
                retorno = retorno + "<option value=" + x1.NumeroPedido + ">Cancelado</option>";
                retorno = retorno + "</select>";
                retorno = retorno + "</td>";
                retorno = retorno + "<td><a href='#' onclick='MostrarDetalhePedido(" + x1.IdPedidoFull + ")'>Detalhes...<a/></td>";
                retorno = retorno + "</tr>";
            }
            else if ((x1.Status == "1") || (x1.Status == "2"))
            {
                string Status = "";

                if (x1.Status == "1")
                {
                    Status = "Finalizado";

                    retorno = retorno + "<tr id='LineRed'>";
                    retorno = retorno + "<td>" + x1.NumeroPedido + "</td>";
                    retorno = retorno + "<td>" + x1.Data.ToString("dd/MM/yyyy") + " " + x1.Hora + "</td>";
                    retorno = retorno + "<td>" + Status + "</td>";
                    retorno = retorno + "<td><a href='#' onclick='MostrarDetalhePedido(" + x1.IdPedidoFull + ")'>Detalhes...<a/></td>";
                    retorno = retorno + "</tr>";
                }
                else
                {
                    retorno = retorno + "<tr id='LineRed'>";
                    retorno = retorno + "<td>" + x1.NumeroPedido + "</td>";
                    retorno = retorno + "<td>" + x1.Data.ToString("dd/MM/yyyy") + " " + x1.Hora + "</td>";
                    retorno = retorno + "<td>";
                    retorno = retorno + "<select name='Tipo' id=" + x1.NumeroPedido + " onchange='AlteraPedido(this)' id=" + x1.NumeroPedido + ">";
                    retorno = retorno + "<option value=" + x1.NumeroPedido + ">Cancelado</option>";
                    retorno = retorno + "<option value=" + x1.NumeroPedido + ">Aguardando Atendimento</option>";
                    retorno = retorno + "</select>";
                    retorno = retorno + "</td>";
                    retorno = retorno + "<td><a href='#' onclick='MostrarDetalhePedido(" + x1.IdPedidoFull + ")'>Detalhes...<a/></td>";
                    retorno = retorno + "</tr>";
                }
            }
            retorno = retorno + "</tbody></table>";


            return retorno;
        }

        public ActionResult AtualizaEstoque()
        {
            return View();
        }

        public string BuscaTipoProdutoPorIngrediente(string Dados)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();
            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                var ret = rp.BuscaTipoProdutoPorIngrediente(int.Parse(IdFornecedor.ToString()), Dados);

                foreach (var r in ret)
                {
                    if (retorno == "")
                    {
                        retorno = r.Tipo.ToString() + "^" + r.Nome;
                    }
                    else
                    {
                        retorno = retorno + "|" + r.Tipo.ToString() + "^" + r.Nome; ;
                    }
                }
            }
            return retorno;
        }

        public string BuscaProdutoPorIngredienteTipo(string Dados, string Tipo, string Tamanho)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                var ret = rp.BuscaProdutoPorIngredienteTipo(int.Parse(IdFornecedor.ToString()), int.Parse(Tipo), Dados, Tamanho);

                foreach (var r in ret)
                {
                    string status = "Inativo";
                    if (r.Status)
                    {
                        status = "Ativo";
                    }

                    string TVSV = r.IdFornecedorProduto + "^" + r.Nome + "^" + r.Valor + "^" + r.Tipo;

                    if (retorno == "")
                    {
                        retorno = r.Nome + "=" + status + "=" + TVSV.ToString();
                    }
                    else
                    {
                        retorno = retorno + "|" + r.Nome + "=" + status + "=" + TVSV.ToString();
                    }
                }
                return retorno + "-" + ret.Count().ToString();
            }
            else
            {

                return retorno;
            }
        }

        public string BuscaProdutoPorIngredienteTipoAll(string Dados, string Tipo)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                var ret = rp.BuscaProdutoPorIngredienteTipoAll(int.Parse(IdFornecedor.ToString()), int.Parse(Tipo), Dados);

                foreach (var r in ret)
                {
                    string status = "Inativo";
                    if (r.Status)
                    {
                        status = "Ativo";
                    }

                    string TVSV = r.IdFornecedorProduto + "^" + r.Nome + "^" + r.Valor + "^" + r.Tipo;

                    if (retorno == "")
                    {
                        retorno = r.Nome + "=" + status + "=" + TVSV.ToString();
                    }
                    else
                    {
                        retorno = retorno + "|" + r.Nome + "=" + status + "=" + TVSV.ToString();
                    }
                }
                return retorno + "-" + ret.Count().ToString();
            }
            else
            {

                return retorno;
            }
        }

        public string AlteraStatusProduto(string Dados, string Ingrediente)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                retorno = rp.AlteraStatusProdutosPorIngrediente(int.Parse(IdFornecedor.ToString()), Dados, Ingrediente);
            }
            return retorno;
        }

        public ActionResult PedidoManual()
        {
            return View();
        }

        public ActionResult PedidoManualProduto(string IdFornecedor, string Cep, string Rua, string Numero)
        {
            Persistence.MPRepository rp = new Persistence.MPRepository();
            List<Domain.ProdutoModel> prd = new List<Domain.ProdutoModel>();

            prd = rp.ListaProdutos(1, 1).ToList();

            //ViewBag.Produto = prd;

            return View(prd);
        }

        public string BuscarClienteTelCep(string Numero, string Cep, string Telefone)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                retorno = rp.BuscaClienteTelCep(Numero, Cep, Telefone);

                if ((retorno == "") && (!string.IsNullOrWhiteSpace(Cep)))
                {
                    retorno = rp.BuscaCEP(Cep, Numero);
                }
            }
            else
            {
                retorno = "Erro de Autenticação.";
            }

            return retorno;
        }

        public string CadastraCliente(string Rua, string Telefone, string Numero, string NomeCliente, string Cep, string Complemento, string CPF, string Referencia, string Email)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                retorno = rp.CadastraCliente(Rua, Telefone, Numero, NomeCliente, Cep, Complemento, CPF, Referencia, Email);
            }
            System.Web.HttpContext.Current.Session["CEP"] = Cep;
            System.Web.HttpContext.Current.Session["Numero"] = Numero;
            System.Web.HttpContext.Current.Session["Telefone"] = Telefone;
            System.Web.HttpContext.Current.Session["Rua"] = Rua;

            return retorno;
        }

        public ActionResult MeioPagamento(string Dados)
        {
            return View();
        }

        public string CarregaDados()
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            var Dados = (string)System.Web.HttpContext.Current.Session["ManualCarrinho"];

            if (IdFornecedor != null)
            {
                string FormaPagamentos = rp.BuscaFormaPagamento(int.Parse(IdFornecedor.ToString()));

                var ArrayFormaPgt = FormaPagamentos.Split('|');

                retorno = retorno + "<div id='Container' style='margin-top:50px;'>";
                retorno = retorno + "<br>";
                retorno = retorno + "<label>Selecione uma Forma de Pagamento:</label><br><br>";

                for (int i = 0; i < ArrayFormaPgt.Length; i++)
                {
                    retorno = retorno + "<label>" + ArrayFormaPgt[i] + "</label>";
                    retorno = retorno + "<input type='radio' name='FormaPgto' value='" + ArrayFormaPgt[i] + "'> &nbsp&nbsp&nbsp&nbsp";
                }

                retorno = retorno + "<br>";
                retorno = retorno + "<br>";
                retorno = retorno + "</div><br><br>";

                retorno = retorno + "<table id='FimPedido'><th>Produto</th><th>Valor Unitário</th><th>Tamanho</th><th>Quantidade</th><th>Observações</th>";
                var tsplit = Dados.Split('_');
                decimal valortotal = 0;
                for (int i = 0; i < tsplit.Length; i++)
                {
                    var DadosTbl = tsplit[i].Split('^');

                    var ValidaMeia = tsplit[i].Split(';');

                    if (ValidaMeia.Length > 1)
                    {
                        decimal MaiorValor = 0;
                        if (decimal.Parse(DadosTbl[3]) > decimal.Parse(DadosTbl[8]))
                        {
                            MaiorValor = decimal.Parse(DadosTbl[3]);
                        }
                        else
                        {
                            MaiorValor = decimal.Parse(DadosTbl[8]);
                        }
                        var Borda = DadosTbl[6].Split('#');

                        retorno = retorno + "<tr><td>1/2" + DadosTbl[2] + "1/2" + DadosTbl[7] + " - " + DadosTbl[11] + "</td><td>" + DadosTbl[3] + "</td><td>" + DadosTbl[5] + "</td><td>" + DadosTbl[12] + "</td><td>" + DadosTbl[13] + "</td>";
                        valortotal = valortotal + (MaiorValor * decimal.Parse(DadosTbl[12]));
                    }
                    else
                    {
                        if (DadosTbl[4] == "3")
                        {
                            retorno = retorno + "<tr><td>" + DadosTbl[2] + "</td><td>" + DadosTbl[3] + "</td><td>" + DadosTbl[5] + "</td><td>" + DadosTbl[6] + "</td><td></td>";
                            valortotal = valortotal + (decimal.Parse(DadosTbl[3]) * decimal.Parse(DadosTbl[6]));
                        }
                        else if (DadosTbl[4] == "11")
                        {
                            retorno = retorno + "<tr><td>" + DadosTbl[2] + "</td><td>" + DadosTbl[3] + "</td><td>" + DadosTbl[5] + "</td><td>" + DadosTbl[7] + "</td><td>" + DadosTbl[8] + "</td>";
                            valortotal = valortotal + (decimal.Parse(DadosTbl[3]) * decimal.Parse(DadosTbl[7]));
                        }
                        else
                        {
                            retorno = retorno + "<tr><td>" + DadosTbl[2] + " - " + DadosTbl[6] + "</td><td>" + DadosTbl[3] + "</td><td>" + DadosTbl[5] + "</td><td>" + DadosTbl[7] + "</td><td>" + DadosTbl[8] + "</td>";
                            valortotal = valortotal + (decimal.Parse(DadosTbl[3]) * decimal.Parse(DadosTbl[7]));
                        }

                    }

                }

                retorno = retorno + "</table><input id='DadosP' type='hidden' value='" + Dados + "'><br><br>";
                retorno = retorno + "<label> Valor Total: </label>" + valortotal + "<br><br>";
            }
            return retorno;
        }

        public string DadoSessionManual(string Dados, string Acao)
        {
            string retorno = "";

            if (Acao == "Adicionar")
            {
                string Carrinho = (string)System.Web.HttpContext.Current.Session["ManualCarrinho"];
                if (string.IsNullOrEmpty(Carrinho))
                {
                    Carrinho = Dados;
                }
                else
                {
                    Carrinho = Carrinho + "_" + Dados;
                }
                System.Web.HttpContext.Current.Session["ManualCarrinho"] = Carrinho;
            }
            else if (Acao == "Excluir")
            {
                string newcarrinho = "";
                string Carrinho = (string)System.Web.HttpContext.Current.Session["ManualCarrinho"];
                if (!string.IsNullOrEmpty(Carrinho))
                {
                    var ListaProdutos = Carrinho.Split('_');
                    var ProdutoRequisicao = Dados.Split('^');

                    for (int x = 0; x < ListaProdutos.Length; x++)
                    {
                        var Produto = ListaProdutos[x].Split('^');
                        if (Produto[1] != ProdutoRequisicao[1])
                        {
                            if (newcarrinho == "")
                            {
                                newcarrinho = ListaProdutos[x];
                            }
                            else
                            {
                                newcarrinho = newcarrinho + "_" + ListaProdutos[x];
                            }
                        }
                    }

                    System.Web.HttpContext.Current.Session["ManualCarrinho"] = newcarrinho;
                }
            }
            else
            {
                string Carrinho = (string)System.Web.HttpContext.Current.Session["ManualCarrinho"];
                if (!string.IsNullOrEmpty(Carrinho))
                {
                    string newcarrinho = "";
                    var ListaProdutos = Carrinho.Split('_');
                    var ProdutoRequisicao = Dados.Split('^');

                    for (int x = 0; x < ListaProdutos.Length; x++)
                    {
                        var splitproduto = ListaProdutos[x];
                        var Produto = splitproduto.Split('^');

                        if (Produto[1] == ProdutoRequisicao[1])
                        {
                            if (newcarrinho == "")
                            {
                                newcarrinho = Dados;
                            }
                            else
                            {
                                newcarrinho = newcarrinho + "_" + Dados;
                            }
                        }
                        else
                        {
                            if (newcarrinho == "")
                            {
                                newcarrinho = splitproduto;
                            }
                            else
                            {
                                newcarrinho = newcarrinho + "_" + splitproduto;
                            }
                        }
                    }

                    System.Web.HttpContext.Current.Session["ManualCarrinho"] = newcarrinho;
                }
            }
            return retorno;
        }

        public string SalvarPedidoManual(string Dados, string MeioPgto, string NotaFiscal, string Desconto, string Troco)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            Domain.PedidoModel Pedido = new PedidoModel();
            List<Domain.ItemPedidoModel> ListaItem = new List<ItemPedidoModel>();

            var tsplit = Dados.Split('_');
            decimal valortotal = 0;
            for (int i = 0; i < tsplit.Length; i++)
            {
                Domain.ItemPedidoModel Item = new ItemPedidoModel();

                var DadosTbl = tsplit[i].Split('^');

                var ValidaMeia = tsplit[i].Split(';');

                if (ValidaMeia.Length > 1)
                {
                    decimal MaiorValor = 0;
                    if (decimal.Parse(DadosTbl[3]) > decimal.Parse(DadosTbl[8]))
                    {
                        MaiorValor = decimal.Parse(DadosTbl[3]);
                    }
                    else
                    {
                        MaiorValor = decimal.Parse(DadosTbl[8]);
                    }
                    var Borda = DadosTbl[6].Split('#');

                    var IdsMeia = DadosTbl[6].Split(';');

                    Item.IdProduto = DadosTbl[1] + "-" + IdsMeia[1];
                    Item.Borda = Borda[0];
                    Item.Observacoes = DadosTbl[13];
                    Item.Quantidade = int.Parse(DadosTbl[12]);
                    ListaItem.Add(Item);

                    valortotal = valortotal + (MaiorValor * decimal.Parse(DadosTbl[12]));
                }
                else
                {
                    if (DadosTbl[4] == "3")
                    {

                        Item.IdProduto = DadosTbl[1];
                        Item.Borda = "";
                        Item.Observacoes = "";
                        Item.Quantidade = int.Parse(DadosTbl[6]);
                        ListaItem.Add(Item);

                        valortotal = valortotal + (decimal.Parse(DadosTbl[3]) * decimal.Parse(DadosTbl[6]));
                    }
                    else
                    {
                        Item.IdProduto = DadosTbl[1];
                        Item.Borda = DadosTbl[6];
                        Item.Observacoes = DadosTbl[8];
                        Item.Quantidade = int.Parse(DadosTbl[7]);
                        ListaItem.Add(Item);

                        valortotal = valortotal + (decimal.Parse(DadosTbl[3]) * decimal.Parse(DadosTbl[7]));
                    }
                }

            }
            var data = DateTime.Now.ToString().Split(' ');


            Pedido.ItensPedido = ListaItem;
            Pedido.Data = DateTime.Now.Date;
            Pedido.Desconto = double.Parse(Desconto);
            Pedido.Hora = TimeSpan.Parse(data[1]);
            Pedido.FormaPagamento = MeioPgto;
            Pedido.Cep = (string)System.Web.HttpContext.Current.Session["CEP"];
            Pedido.Numero = int.Parse(System.Web.HttpContext.Current.Session["Numero"].ToString());
            Pedido.Rua = (string)System.Web.HttpContext.Current.Session["Rua"];
            Pedido.IdFornecedor = (int)System.Web.HttpContext.Current.Session["SessionFornecedor"];
            Pedido.NotaFiscal = bool.Parse(NotaFiscal);
            Pedido.NumeroPedido = 0;
            Pedido.Troco = Troco;
            Pedido.ValorTotal = valortotal.ToString();

            var selectCliente = rp.BuscaIdCliente(Pedido.Numero.ToString(), Pedido.Cep);
            Pedido.IdCliente = selectCliente.IdCliente;


            retorno = rp.SalvarPedidoManual(Pedido);

            System.Web.HttpContext.Current.Session.Remove("CEP");
            System.Web.HttpContext.Current.Session.Remove("Numero");
            System.Web.HttpContext.Current.Session.Remove("ManualCarrinho");
            System.Web.HttpContext.Current.Session.Remove("Rua");

            return retorno;
        }

        public string RetornaCarrinhoManual()
        {
            string Retorno = (string)System.Web.HttpContext.Current.Session["ManualCarrinho"];

            return Retorno;
        }

        public string BuscaValores(string Dados)
        {
            string retorno = "";

            MPRepository rp = new MPRepository();

            var IdFornecedor = System.Web.HttpContext.Current.Session["SessionFornecedor"];

            if (IdFornecedor != null)
            {
                retorno = rp.BuscaIDProdutosCarrinho(Dados, int.Parse(IdFornecedor.ToString()));
            }
            return retorno;
        }

    }
}

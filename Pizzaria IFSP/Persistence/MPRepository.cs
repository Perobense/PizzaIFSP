using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class MPRepository
    {
        public Boolean InserirProduto(Domain.ProdutoModel model)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO Prd = new FORNECEDOR_PRODUTO();
            Boolean ret = new Boolean();

            var x = db.FORNECEDOR_PRODUTOs.Any(n => n.ID_FORNECEDOR == model.IdFonecedor && n.NOME == model.Nome && n.TAMANHO == model.Tamanho);

            if (!x)
            {
                Prd.INGREDIENTES = model.Ingredientes;
                Prd.ID_FORNECEDOR = model.IdFonecedor;
                Prd.VALOR = (decimal)model.Valor;
                Prd.TIPO = model.Tipo;
                Prd.NOME = model.Nome;
                Prd.FOTOINTEIRA = model.FotoInteira;
                Prd.SUBVARIACAO = model.SubVariacao;
                Prd.PROMOCAO = false;
                Prd.QTD_ESTOQUE = model.QtdEstoque;
                Prd.VARIACAO = model.Variacao;
                Prd.TAMANHO = model.Tamanho;
                Prd.STATUS = true;

                db.FORNECEDOR_PRODUTOs.InsertOnSubmit(Prd);
                db.SubmitChanges();

                ret = true;
            }
            else
            {
                ret = false;
            }
            return ret;

        }

        public List<Domain.ProdutoModel> ListaProdutos(int IdFornecedor, int Tipo)
        {
            IFSPDataContext db = new IFSPDataContext();
            Domain.ProdutoModel pm = new Domain.ProdutoModel();
            List<Domain.ProdutoModel> prdl = new List<Domain.ProdutoModel>();

            if (Tipo != 0)
            {
                var x1 = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.TIPO == Tipo && n.STATUS == true).ToList().OrderByDescending(x => x.TIPO);

                foreach (var i in x1)
                {
                    pm = new Domain.ProdutoModel();

                    pm.IdFornecedorProduto = i.ID_FORNECEDOR_PRODUTO;
                    pm.Ingredientes = i.INGREDIENTES;
                    pm.Valor = (decimal)i.VALOR;
                    pm.Tipo = (int)i.TIPO;
                    pm.Nome = i.NOME;
                    pm.Status = (bool)i.STATUS;

                    prdl.Add(pm);

                }
            }
            else
            {
                var x1 = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == IdFornecedor).ToList().OrderByDescending(x => x.TIPO);

                foreach (var i in x1)
                {
                    pm = new Domain.ProdutoModel();

                    pm.IdFornecedorProduto = i.ID_FORNECEDOR_PRODUTO;
                    pm.Ingredientes = i.INGREDIENTES;
                    pm.Valor = (decimal)i.VALOR;
                    pm.Tipo = (int)i.TIPO;
                    pm.Nome = i.NOME;

                    prdl.Add(pm);

                }
            }
            return prdl;
        }

        public Domain.ProdutoModel BuscarProduto(Domain.ProdutoModel model)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO pizza = new FORNECEDOR_PRODUTO();
            Domain.ProdutoModel ret = new Domain.ProdutoModel();

            var x = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == model.IdFonecedor && n.ID_FORNECEDOR_PRODUTO == model.IdProduto && n.TIPO == model.Tipo).FirstOrDefault();

            if (x.NOME != null)
            {
                ret.Ingredientes = x.INGREDIENTES;
                ret.IdFonecedor = x.ID_FORNECEDOR;
                ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                ret.Valor = (decimal)x.VALOR;
                ret.Tipo = (int)x.TIPO;
                ret.Nome = x.NOME;
                ret.Variacao = (int)x.VARIACAO;
                ret.Tamanho = (int)x.TAMANHO;
            }

            return ret;

        }

        public Domain.ProdutoModel BuscarBebida(Domain.ProdutoModel model)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO pizza = new FORNECEDOR_PRODUTO();
            Domain.ProdutoModel ret = new Domain.ProdutoModel();
            // n.ID_VENDEDOR == model.IDVendedor &&
            var x = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR_PRODUTO == model.IdProduto).FirstOrDefault();

            if ((x.NOME != null) && (x.VALOR != null) && (x.TIPO != null))
            {
                ret.Ingredientes = x.INGREDIENTES;
                ret.IdFonecedor = x.ID_FORNECEDOR;
                ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                ret.Valor = (decimal)x.VALOR;
                ret.Tipo = (int)x.TIPO;
                ret.Nome = x.NOME;
                ret.Variacao = (int)x.VARIACAO;
                ret.Tamanho = (int)x.TAMANHO;
            }

            return ret;

        }

        public List<Domain.ProdutoModel> BuscarProdutoPorID(string ID)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO pizza = new FORNECEDOR_PRODUTO();
            List<Domain.ProdutoModel> ret = new List<Domain.ProdutoModel>();

            var Ids = ID.Split(',');

            if (Ids.Count() == 1)
            {
                var x = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR_PRODUTO == int.Parse(ID)).FirstOrDefault();

                if (x != null)
                {
                    Domain.ProdutoModel objt = new Domain.ProdutoModel();

                    objt.Ingredientes = x.INGREDIENTES;
                    objt.IdFonecedor = x.ID_FORNECEDOR;
                    objt.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                    objt.Valor = (decimal)x.VALOR;
                    objt.Tipo = (int)x.TIPO;
                    objt.Nome = x.NOME;
                    objt.Variacao = (int)x.VARIACAO;
                    objt.Tamanho = (int)x.TAMANHO;
                    if (x.FOTOINTEIRA != null)
                    {
                        objt.FotoInteira = x.FOTOINTEIRA.ToArray();
                    }
                    ret.Add(objt);
                }
            }
            else
            {
                var arrID = ID.Split(',');

                var select = (from fp in db.FORNECEDOR_PRODUTOs
                              where arrID.Contains(fp.ID_FORNECEDOR_PRODUTO.ToString())
                              select fp).ToList();

                foreach (var x in select)
                {
                    if ((x.NOME != null) && (x.VALOR != null) && (x.TIPO != null))
                    {
                        Domain.ProdutoModel objt = new Domain.ProdutoModel();

                        objt.Ingredientes = x.INGREDIENTES;
                        objt.IdFonecedor = x.ID_FORNECEDOR;
                        objt.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                        objt.Valor = (decimal)x.VALOR;
                        objt.Tipo = (int)x.TIPO;
                        objt.Nome = x.NOME;
                        objt.Variacao = (int)x.VARIACAO;
                        objt.Tamanho = (int)x.TAMANHO;
                        if (x.FOTOINTEIRA != null)
                        {
                            objt.FotoInteira = x.FOTOINTEIRA.ToArray();
                        }
                        ret.Add(objt);
                    }
                }
            }
            return ret;

        }

        public List<Domain.PedidoModel> MostraPedido(int IdVendedor, DateTime DATA)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            PEDIDO_FULL Busca = new PEDIDO_FULL();
            List<Domain.PedidoModel> ListaRetorno = new List<Domain.PedidoModel>();

            var Select = db.PEDIDO_FULLs.Where(Armazena => Armazena.ID_FORNECEDOR == IdVendedor).ToList();

            foreach (var obj in Select)
            {

                Domain.PedidoModel Retorno = new Domain.PedidoModel();

                Retorno.Data = (DateTime)obj.DATA;
                Retorno.Hora = (TimeSpan)obj.HORA;
                Retorno.Numero = Convert.ToInt32(obj.NUMERO);
                Retorno.Status = obj.STATUS;
                Retorno.NumeroPedido = (int)obj.NUMERO_PEDIDO;
                Retorno.IdPedidoFull = obj.ID_PEDIDO_FULL;

                ListaRetorno.Add(Retorno);
            }

            return ListaRetorno;
        }

        public Domain.ProdutoModel BuscarBebidas(int I)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO pizza = new FORNECEDOR_PRODUTO();
            Domain.ProdutoModel ret = new Domain.ProdutoModel();

            var ListaProduto = db.FORNECEDOR_PRODUTOs.Where(n => n.TIPO == 3).ToList();

            int contador = 0;
            foreach (var x in ListaProduto)
            {
                if (contador == I)
                {
                    if ((x.NOME != null) && (x.VALOR != null) && (x.TIPO != null))
                    {
                        ret.Ingredientes = x.INGREDIENTES;
                        ret.IdFonecedor = x.ID_FORNECEDOR;
                        ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                        ret.Valor = (decimal)x.VALOR;
                        ret.Tipo = (int)x.TIPO;
                        ret.Nome = x.NOME;
                        ret.Variacao = (int)x.VARIACAO;
                        ret.Tamanho = (int)x.TAMANHO;
                        if (x.FOTOINTEIRA != null)
                        {
                            ret.FotoInteira = x.FOTOINTEIRA.ToArray();
                        }
                    }
                }
                contador = contador + 1;
            }
            return ret;

        }

        public List<Domain.ProdutoModel> MostraBebidasFull()
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO pizza = new FORNECEDOR_PRODUTO();
            List<Domain.ProdutoModel> Listret = new List<Domain.ProdutoModel>();

            var ListaProduto = db.FORNECEDOR_PRODUTOs.Where(n => n.TIPO == 3).ToList();

            foreach (var x in ListaProduto)
            {
                Domain.ProdutoModel Produto = new Domain.ProdutoModel();
                if ((x.NOME != null) && (x.VALOR != null) && (x.TIPO != null))
                {
                    Produto.Ingredientes = x.INGREDIENTES;
                    Produto.IdFonecedor = x.ID_FORNECEDOR;
                    Produto.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                    Produto.Valor = (decimal)x.VALOR;
                    Produto.Tipo = (int)x.TIPO;
                    Produto.Nome = x.NOME;
                    Produto.Variacao = (int)x.VARIACAO;
                    Produto.Tamanho = (int)x.TAMANHO;
                    if (x.FOTOINTEIRA != null)
                    {
                        Produto.FotoInteira = x.FOTOINTEIRA.ToArray();
                    }
                    Listret.Add(Produto);
                }

            }
            return Listret;

        }

        public Domain.PedidoModel MostrarDetalhePedido(int Id)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            PEDIDO_FULL Busca = new PEDIDO_FULL();
            Domain.PedidoModel Retorno = new Domain.PedidoModel();
            List<Domain.ItemPedidoModel> ListaItem = new List<Domain.ItemPedidoModel>();

            var obj = db.PEDIDO_FULLs.Where(TabelaPedido => TabelaPedido.ID_PEDIDO_FULL == Id).FirstOrDefault();

            Retorno.Data = (DateTime)obj.DATA;
            Retorno.Hora = (TimeSpan)obj.HORA;
            Retorno.Numero = Convert.ToInt32(obj.NUMERO);
            Retorno.Status = obj.STATUS;
            if (obj.NOTAFISCAL != null)
            {
                Retorno.NotaFiscal = (bool)obj.NOTAFISCAL;
            }
            Retorno.Numero = (int)obj.NUMERO;
            Retorno.Observacoes = obj.OBSERVACOES;
            Retorno.Rua = obj.RUA;
            Retorno.FormaPagamento = obj.FORMA_PAGAMENTO;
            Retorno.Desconto = (double)obj.DESCONTO;
            Retorno.Cep = obj.CEP;
            Retorno.IdCliente = obj.ID_CLIENTE;
            Retorno.ValorTotal = obj.VALOR_TOTAL.ToString();
            Retorno.NumeroPedido = (int)obj.NUMERO_PEDIDO;
            Retorno.IdPedidoFull = obj.ID_PEDIDO_FULL;

            var selectItens = db.ITENS_PEDIDOs.Where(i => i.PEDIDO_ID == Retorno.IdPedidoFull).ToList();

            foreach (var item in selectItens)
            {
                Domain.ItemPedidoModel pedidoitem = new Domain.ItemPedidoModel();

                pedidoitem.IdItemPedido = item.ITEM_PEDIDO_ID;
                pedidoitem.IdProduto = item.ID_PRODUTO;
                pedidoitem.Quantidade = item.QUANTIDADE;
                pedidoitem.IdPedido = Retorno.IdPedidoFull;

                ListaItem.Add(pedidoitem);
            }

            Retorno.ItensPedido = ListaItem;

            return Retorno;
        }

        public Domain.FornecedorModel Login(string login, string senha)
        {
            IFSPDataContext Banco = new IFSPDataContext();
            MPRepository RP = new MPRepository();
            FORNECEDOR Busca = new FORNECEDOR();
            Domain.FornecedorModel Retorno = new Domain.FornecedorModel();

            try
            {
                var Select = Banco.FORNECEDORs.Where(Fornecedor => Fornecedor.USUARIO == login && Fornecedor.SENHA == senha).FirstOrDefault();

                if (Select != null)
                {
                    if (Select.USUARIO != null && Select.SENHA != null)
                    {

                        Retorno.IdUtilizador = Select.ID_UTILIZADOR;
                        Retorno.Usuario = Select.USUARIO;
                        Retorno.Senha = Select.NOME;
                        Retorno.TipoUtilizador = (bool)Select.TIPO_UTILIZADOR;

                        Select.DATA_ULTIMO_LOGIN = DateTime.Now;

                        Banco.SubmitChanges();

                    }
                }
                else 
                {
                    throw new Exception();
                }
            }
            catch(Exception e)
            {
                                
            }
            return Retorno;
        }

        public bool UpdateFornecedor(int IdFornecedor, Domain.FornecedorModel Fornecedor, string TipoAlteracao, string TempoPreparo, string TempoEntrega)
        {
            IFSPDataContext Banco = new IFSPDataContext();
            MPRepository RP = new MPRepository();
            FORNECEDOR Busca = new FORNECEDOR();
            bool Retorno = false;

            var Select = Banco.FORNECEDORs.Where(tabela => tabela.ID_UTILIZADOR == IdFornecedor).FirstOrDefault();

            if (Select != null)
            {
                if (TipoAlteracao == "TempoPreparoEntrega")
                {
                    // TEMPO_ENTREGA (banco) = TempoEntrega (tela)
                    Select.TEMPO_ENTREGA = TempoEntrega;
                    Select.TEMPO_PREPARO = TempoPreparo;

                }
                else
                {
                    Select.CNPJ = Fornecedor.Cnpj;
                    Select.NOME = Fornecedor.Nome;
                    Select.CEP = Fornecedor.Cep.ToString();
                    Select.TELEFONE = Fornecedor.Telefone;
                    Select.RUA = Fornecedor.Rua;
                    Select.NUMERO = Fornecedor.Numero;
                    Select.REFERENCIA = Fornecedor.Referencia;
                    Select.STATUS = Fornecedor.Status;
                    Select.EMAIL = Fornecedor.Email;
                    Select.BAIRRO = Fornecedor.Bairro;
                    Select.CIDADE = Fornecedor.Cidade;
                    Select.VOUCHER = Fornecedor.Voucher;
                    Select.NOME_CONTATO = Fornecedor.NomeContato;
                    Select.VENDA_ONLINE = Fornecedor.VendaOnline;
                    Select.USUARIO = Fornecedor.Usuario;
                    Select.SENHA = Fornecedor.Senha;
                    Select.DATA_ULTIMO_LOGIN = Fornecedor.DataUltimoLogin;
                    Select.USER_LOGADO = Fornecedor.UserLogado;
                    Select.TEMPO_ENTREGA = Fornecedor.TempoEntrega;
                    Select.TEMPO_PREPARO = Fornecedor.TempoPreparo;
                    Select.TAXA_ENTREGA = Fornecedor.TaxaEntrega;
                }
                //update
                Banco.SubmitChanges();

                Retorno = true;
            }

            return Retorno;
        }

        public bool ValidaStatus(int Id)
        {
            bool Retorno = false;
            IFSPDataContext Banco = new IFSPDataContext();

            var Dados = DateTime.Now.ToString().Split(' ');

            var Dia = DeParaDiaSemana(DateTime.Now.DayOfWeek.ToString());

            string data = Dados[0];
            string hora = Dados[1];

            Retorno = Banco.FORNECEDOR_HORARIOs.Any(FornecedorHorario => FornecedorHorario.FORNECEDOR.ID_UTILIZADOR == Id && FornecedorHorario.DIA == Dia && FornecedorHorario.HORA_INICIO <= TimeSpan.Parse(hora) && FornecedorHorario.HORA_FIM >= TimeSpan.Parse(hora));

            Retorno = true;
            return Retorno;
        }

        public string DeParaDiaSemana(string DiaIngles)
        {
            string DiaPortugues = "";

            switch (DiaIngles)
            {
                case "Monday":
                    DiaPortugues = "SEGUNDA";
                    break;

                case "Tuesday":
                    DiaPortugues = "TERÇA";
                    break;

                case "Wednesday":
                    DiaPortugues = "QUARTA";
                    break;

                case "Thursday":
                    DiaPortugues = "QUINTA";
                    break;

                case "Friday":
                    DiaPortugues = "SEXTA";
                    break;

                case "Saturday":
                    DiaPortugues = "SABADO";
                    break;

                case "Sunday":
                    DiaPortugues = "DOMINGO";
                    break;

            }

            return DiaPortugues;
        }

        public Domain.ProdutoModel BuscaDadosCardapio(int Id)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO Busca = new FORNECEDOR_PRODUTO();
            Domain.ProdutoModel Retorno = new Domain.ProdutoModel();

            var Select = db.FORNECEDOR_PRODUTOs.Where(TabelaProduto => TabelaProduto.ID_FORNECEDOR_PRODUTO == Id).ToList();

            foreach (var obj in Select)
            {
                Retorno.Nome = obj.NOME;
                Retorno.Valor = Convert.ToDecimal(obj.VALOR);
                Retorno.Ingredientes = obj.INGREDIENTES;
                Retorno.Tipo = Convert.ToInt32(obj.TIPO);
                Retorno.Tamanho = Convert.ToInt32(obj.TAMANHO);
                Retorno.Variacao = Convert.ToInt32(obj.VARIACAO);
                Retorno.IdFornecedorProduto = Convert.ToInt32(obj.ID_FORNECEDOR_PRODUTO);
                Retorno.Promocao = (bool)obj.PROMOCAO;
                Retorno.Status = (bool)obj.STATUS;
                Retorno.SubVariacao = (int)obj.SUBVARIACAO;
                Retorno.ValorPromocao = (decimal)obj.VALOR_PROMOCAO;
            }

            return Retorno;
        }

        public List<Domain.FornecedorModel> BuscarCepFornecedor(string CEP)
        {
            List<Domain.FornecedorModel> ListaProduto = new List<Domain.FornecedorModel>();
            IFSPDataContext Banco = new IFSPDataContext();
            FORNECEDOR_CEP TabelaFornecedorCep = new FORNECEDOR_CEP();

            var Select = Banco.FORNECEDOR_CEPs.Where(Tabela => Tabela.CEP == CEP).ToList();

            foreach (var Fornecedor in Select)
            {
                Domain.FornecedorModel Produto = new Domain.FornecedorModel();

                Produto.IdUtilizador = Fornecedor.FORECEDOR_ID;

                ListaProduto.Add(Produto);
            }

            return ListaProduto;
        }

        public List<Domain.FornecedorCepModel> BuscarCep(int IdFornecedor)
        {
            List<Domain.FornecedorCepModel> ListaCep = new List<Domain.FornecedorCepModel>();
            IFSPDataContext Banco = new IFSPDataContext();
            FORNECEDOR_CEP TabelaFornecedorCep = new FORNECEDOR_CEP();

            var Select = Banco.FORNECEDOR_CEPs.Where(Tabela => Tabela.FORECEDOR_ID == IdFornecedor).ToList();

            foreach (var Fornecedor in Select)
            {
                Domain.FornecedorCepModel Cep = new Domain.FornecedorCepModel();

                Cep.Cep = Fornecedor.CEP.ToString();
                Cep.Rua = Fornecedor.RUA.ToString();
                Cep.Taxa = Fornecedor.TAXA.ToString();
                Cep.IdCep = Fornecedor.ID_CEP.ToString();
                Cep.Bairro = Fornecedor.BAIRRO.ToString();

                ListaCep.Add(Cep);
            }

            return ListaCep;
        }

        public List<Domain.FornecedorCepModel> AtualizaBuscaCEP(string Tipo, string Dados, int IdFornecedor)
        {
            List<Domain.FornecedorCepModel> ListaCep = new List<Domain.FornecedorCepModel>();
            IFSPDataContext Banco = new IFSPDataContext();
            FORNECEDOR_CEP TabelaFornecedorCep = new FORNECEDOR_CEP();

            string CepFornecedor = Banco.FORNECEDORs.Where(n => n.ID_UTILIZADOR == IdFornecedor).FirstOrDefault().CEP;

            var retornoForn = Banco.CEP_FULLs.Where(n => n.CEP == CepFornecedor.ToString()).FirstOrDefault();

            if (retornoForn != null)
            {
                string EstadoFornecedor = retornoForn.Estado;

                if (Tipo == "CEP")
                {
                    var Select = (from fc in Banco.FORNECEDOR_CEPs
                                  where fc.FORECEDOR_ID == IdFornecedor
                                  select fc.CEP).ToList();
                    //select pelo CEP
                    var FornecedorCep = Banco.CEP_FULLs.Where(Tabela => Tabela.CEP == Dados).FirstOrDefault();

                    if (!Select.Contains(FornecedorCep.CEP))
                    {
                        Domain.FornecedorCepModel Cep = new Domain.FornecedorCepModel();

                        Cep.Cep = FornecedorCep.CEP.ToString();
                        Cep.Rua = FornecedorCep.Rua.ToString();
                        Cep.Bairro = FornecedorCep.Bairro.ToString();
                        Cep.Municipio = FornecedorCep.Cidade.ToString();
                        Cep.Estado = FornecedorCep.Estado.ToString();

                        ListaCep.Add(Cep);
                    }
                    else
                    {
                        Domain.FornecedorCepModel Cep = new Domain.FornecedorCepModel();

                        Cep.Cep = "-1";
                        Cep.Rua = FornecedorCep.Rua.ToString();
                        Cep.Bairro = FornecedorCep.Bairro.ToString();
                        Cep.Municipio = FornecedorCep.Cidade.ToString();
                        Cep.Estado = FornecedorCep.Estado.ToString();

                        ListaCep.Add(Cep);
                    }
                }
                else
                {

                    /*var Select = (from cf in Banco.CEP_FULLs
                                  join fc in Banco.FORNECEDOR_CEPs on cf.CEP equals fc.CEP
                                  where fc.FORECEDOR_ID == IdFornecedor
                                  select fc.CEP).ToList();
                    */
                    var Select = (from fc in Banco.FORNECEDOR_CEPs
                                  where fc.FORECEDOR_ID == IdFornecedor
                                  select fc.CEP).ToList();
                    //select pela rua
                    var NewCepForn = Banco.CEP_FULLs.Where(Tabela => Tabela.Rua.Contains(Dados) && Tabela.Estado == EstadoFornecedor).ToList();

                    foreach (var FornecedorCep in NewCepForn)
                    {
                        if (!Select.Contains(FornecedorCep.CEP))
                        {
                            Domain.FornecedorCepModel Cep = new Domain.FornecedorCepModel();

                            Cep.Cep = FornecedorCep.CEP.ToString();
                            Cep.Rua = FornecedorCep.Rua.ToString();
                            Cep.Bairro = FornecedorCep.Bairro.ToString();
                            Cep.Municipio = FornecedorCep.Cidade.ToString();
                            Cep.Estado = FornecedorCep.Estado.ToString();

                            ListaCep.Add(Cep);
                        }
                    }
                }
            }

            return ListaCep;
        }

        public List<Domain.FornecedorModel> ListarFornecedor(string IDs)
        {
            List<Domain.FornecedorModel> ListaFornecedor = new List<Domain.FornecedorModel>();
            IFSPDataContext Banco = new IFSPDataContext();
            FORNECEDOR TabelaFornecedorCep = new FORNECEDOR();

            if (IDs != "")
            {
                var ParamIDs = IDs.Split('^');
                // int?[] productModelIds = { ParamIDs };
                var Select = from Fornecedor in Banco.FORNECEDORs
                             where ParamIDs.Contains(Fornecedor.ID_UTILIZADOR.ToString())
                             select Fornecedor;

                foreach (var Fornecedor in Select)
                {
                    Domain.FornecedorModel Forn = new Domain.FornecedorModel();

                    Forn.IdUtilizador = Fornecedor.ID_UTILIZADOR;
                    Forn.Bairro = Fornecedor.BAIRRO;
                    Forn.Nome = Fornecedor.NOME;
                    Forn.Cnpj = Fornecedor.CNPJ;
                    Forn.Cep = Fornecedor.CEP;
                    Forn.NomeContato = Fornecedor.NOME_CONTATO;
                    Forn.Numero = (int)Fornecedor.NUMERO;
                    Forn.Referencia = Fornecedor.REFERENCIA;
                    Forn.Rua = Fornecedor.RUA;
                    Forn.Telefone = Fornecedor.TELEFONE;
                    Forn.Cidade = Fornecedor.CIDADE;
                    Forn.Email = Fornecedor.EMAIL;
                    Forn.Status = (bool)Fornecedor.STATUS;
                    Forn.DataUltimoLogin = (DateTime)Fornecedor.DATA_ULTIMO_LOGIN;
                    Forn.Diretorio = Fornecedor.DIRETORIO;
                    Forn.Rua = Fornecedor.RUA;
                    Forn.Senha = Fornecedor.SENHA;
                    Forn.TaxaEntrega = Fornecedor.TAXA_ENTREGA;
                    Forn.TempoEntrega = Fornecedor.TEMPO_ENTREGA;
                    Forn.TempoPreparo = Fornecedor.TEMPO_PREPARO;
                    Forn.Usuario = Fornecedor.USUARIO;
                    Forn.VendaOnline = (bool)Fornecedor.VENDA_ONLINE;
                    Forn.Voucher = Fornecedor.VOUCHER;

                    ListaFornecedor.Add(Forn);
                }
            }
            else
            {
                var Select = from Fornecedor in Banco.FORNECEDORs
                             select Fornecedor;

                foreach (var Fornecedor in Select)
                {
                    Domain.FornecedorModel Forn = new Domain.FornecedorModel();


                    Forn.IdUtilizador = Fornecedor.ID_UTILIZADOR;
                    Forn.Bairro = Fornecedor.BAIRRO;
                    Forn.Nome = Fornecedor.NOME;
                    Forn.Cep = Fornecedor.CEP;
                    Forn.Cnpj = Fornecedor.CNPJ;
                    Forn.NomeContato = Fornecedor.NOME_CONTATO;
                    Forn.Numero = (int)Fornecedor.NUMERO;
                    Forn.Referencia = Fornecedor.REFERENCIA;
                    Forn.Rua = Fornecedor.RUA;
                    Forn.Telefone = Fornecedor.TELEFONE;
                    Forn.Cidade = Fornecedor.CIDADE;
                    Forn.Email = Fornecedor.EMAIL;
                    Forn.Status = (bool)Fornecedor.STATUS;
                    Forn.DataUltimoLogin = (DateTime)Fornecedor.DATA_ULTIMO_LOGIN;
                    Forn.Diretorio = Fornecedor.DIRETORIO;
                    Forn.Rua = Fornecedor.RUA;
                    Forn.Senha = Fornecedor.SENHA;
                    Forn.TaxaEntrega = Fornecedor.TAXA_ENTREGA;
                    Forn.TempoEntrega = Fornecedor.TEMPO_ENTREGA;
                    Forn.TempoPreparo = Fornecedor.TEMPO_PREPARO;
                    Forn.Usuario = Fornecedor.USUARIO;
                    Forn.VendaOnline = (bool)Fornecedor.VENDA_ONLINE;
                    Forn.Voucher = Fornecedor.VOUCHER;

                    ListaFornecedor.Add(Forn);
                }
            }

            return ListaFornecedor;
        }

        public Domain.FornecedorModel BuscaFornecedor(string ID)
        {
            Domain.FornecedorModel RetornoFornecedor = new Domain.FornecedorModel();
            IFSPDataContext Banco = new IFSPDataContext();
            FORNECEDOR TabelaFornecedo = new FORNECEDOR();

            var Fornecedor = Banco.FORNECEDORs.Where(Tabela => Tabela.ID_UTILIZADOR == int.Parse(ID)).FirstOrDefault();

            if (Fornecedor != null)
            {
                RetornoFornecedor.IdUtilizador = Fornecedor.ID_UTILIZADOR;
                RetornoFornecedor.Bairro = Fornecedor.BAIRRO;
                RetornoFornecedor.Nome = Fornecedor.NOME;
                RetornoFornecedor.Cnpj = Fornecedor.CNPJ;
                RetornoFornecedor.Cep = Fornecedor.CEP;
                RetornoFornecedor.NomeContato = Fornecedor.NOME_CONTATO;
                RetornoFornecedor.Numero = (int)Fornecedor.NUMERO;
                RetornoFornecedor.Referencia = Fornecedor.REFERENCIA;
                RetornoFornecedor.Rua = Fornecedor.RUA;
                RetornoFornecedor.Telefone = Fornecedor.TELEFONE;
                RetornoFornecedor.Cidade = Fornecedor.CIDADE;
                RetornoFornecedor.Email = Fornecedor.EMAIL;
                RetornoFornecedor.Status = (bool)Fornecedor.STATUS;
                RetornoFornecedor.DataUltimoLogin = (DateTime)Fornecedor.DATA_ULTIMO_LOGIN;
                RetornoFornecedor.Diretorio = Fornecedor.DIRETORIO;
                RetornoFornecedor.Rua = Fornecedor.RUA;
                RetornoFornecedor.Senha = Fornecedor.SENHA;
                RetornoFornecedor.TaxaEntrega = Fornecedor.TAXA_ENTREGA;
                RetornoFornecedor.TempoEntrega = Fornecedor.TEMPO_ENTREGA;
                RetornoFornecedor.TempoPreparo = Fornecedor.TEMPO_PREPARO;
                RetornoFornecedor.Usuario = Fornecedor.USUARIO;
                RetornoFornecedor.VendaOnline = (bool)Fornecedor.VENDA_ONLINE;
                RetornoFornecedor.Voucher = Fornecedor.VOUCHER;

            }

            return RetornoFornecedor;
        }

        public string CadastraAlteraFornecedor(Domain.FornecedorModel Forn)
        {
            string retorno = "";
            bool inserir = false;
            IFSPDataContext Banco = new IFSPDataContext();
            FORNECEDOR TabelaFornecedo = new FORNECEDOR();

            TabelaFornecedo = Banco.FORNECEDORs.Where(Tabela => Tabela.ID_UTILIZADOR == Forn.IdUtilizador).FirstOrDefault();

            if (TabelaFornecedo == null)
            {
                TabelaFornecedo = new FORNECEDOR();

                var Select = (from f in Banco.FORNECEDORs
                              select f.USUARIO).ToList();

                if (Select.Contains(Forn.Usuario))
                {
                    retorno = "Este Usuário já existe no sistema, escolha outro usuario";
                    return retorno;
                }

                inserir = true;
            }

            TabelaFornecedo.BAIRRO = Forn.Bairro;
            TabelaFornecedo.NOME = Forn.Nome;
            TabelaFornecedo.CNPJ = Forn.Cnpj;
            TabelaFornecedo.CEP = Forn.Cep;
            TabelaFornecedo.NOME_CONTATO = Forn.NomeContato;
            TabelaFornecedo.NUMERO = (int)Forn.Numero;
            TabelaFornecedo.REFERENCIA = Forn.Referencia;
            TabelaFornecedo.RUA = Forn.Rua;
            TabelaFornecedo.TELEFONE = Forn.Telefone;
            TabelaFornecedo.CIDADE = Forn.Cidade;
            TabelaFornecedo.EMAIL = Forn.Email;
            TabelaFornecedo.STATUS = (bool)Forn.Status;
            TabelaFornecedo.DIRETORIO = Forn.Diretorio;
            TabelaFornecedo.RUA = Forn.Rua;
            TabelaFornecedo.SENHA = Forn.Senha;
            TabelaFornecedo.TAXA_ENTREGA = Forn.TaxaEntrega;
            TabelaFornecedo.TEMPO_ENTREGA = Forn.TempoEntrega;
            TabelaFornecedo.TEMPO_PREPARO = Forn.TempoEntrega;
            TabelaFornecedo.USUARIO = Forn.Usuario;
            TabelaFornecedo.VENDA_ONLINE = (bool)Forn.VendaOnline;
            TabelaFornecedo.VOUCHER = Forn.Voucher;
            TabelaFornecedo.INGREDIENTES_PADRAO = Forn.IngredientePadrao;
            TabelaFornecedo.DATA_ULTIMO_LOGIN = DateTime.Now;

            retorno = "Registro alterado com sucesso.";

            if (inserir)
            {
                Banco.FORNECEDORs.InsertOnSubmit(TabelaFornecedo);
                retorno = "Registro inserido com sucesso.";
            }

            Banco.SubmitChanges();

            return retorno;
        }

        public List<Domain.ProdutoModel> AtualizaBusca(string Dados, int Tipo, string Status, int IdFornecedor, string Variacao, string Tamanhos, string SubVariacao)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO pizza = new FORNECEDOR_PRODUTO();

            List<Domain.ProdutoModel> ListaRetorno = new List<Domain.ProdutoModel>();

            if (!string.IsNullOrWhiteSpace(Dados))
            {
                if ((Tipo == 1) || (Tipo == 2))
                {
                    if (Variacao == "")
                    {
                        var selectVariacao = db.VARIACAO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectVariacao)
                        {
                            if (Variacao == "")
                            {
                                Variacao = x.ID_VARIACAO.ToString();
                            }
                            else
                            {
                                Variacao = Variacao + "," + x.ID_VARIACAO.ToString();
                            }
                        }
                    }

                    var arrayVariacao = Variacao.Split(',');

                    if (Tamanhos == "")
                    {
                        var selectTamanhos = db.TAMANHO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectTamanhos)
                        {
                            if (Tamanhos == "")
                            {
                                Tamanhos = x.ID_TAMANHO.ToString();
                            }
                            else
                            {
                                Tamanhos = Tamanhos + "," + x.ID_TAMANHO.ToString();
                            }
                        }
                    }

                    var arrayTamanhos = Tamanhos.Split(',');

                    if (SubVariacao == "")
                    {
                        var selectSubVariacao = db.SUBVARIACAO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectSubVariacao)
                        {
                            if (SubVariacao == "")
                            {
                                SubVariacao = x.ID_SUBVARIACAO.ToString();
                            }
                            else
                            {
                                SubVariacao = SubVariacao + "," + x.ID_SUBVARIACAO.ToString();
                            }
                        }
                    }

                    var arraySubVariacao = SubVariacao.Split(',');

                    var retorno = db.FORNECEDOR_PRODUTOs.Where(n =>
                        n.NOME.Contains(Dados) && n.TIPO == Tipo && n.ID_FORNECEDOR == IdFornecedor
                        && arrayVariacao.Contains(n.VARIACAO.ToString())
                        && arrayTamanhos.Contains(n.TAMANHO.ToString())
                        && arraySubVariacao.Contains(n.SUBVARIACAO.ToString())
                        ).ToList();

                    if (retorno != null)
                    {
                        foreach (var x in retorno)
                        {
                            Domain.ProdutoModel ret = new Domain.ProdutoModel();
                            ret.Ingredientes = x.INGREDIENTES;
                            ret.IdFonecedor = x.ID_FORNECEDOR;
                            ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                            ret.Valor = (decimal)x.VALOR;
                            ret.Tipo = (int)x.TIPO;
                            ret.Nome = x.NOME;
                            ret.Variacao = (int)x.VARIACAO;
                            ret.Tamanho = (int)x.TAMANHO;

                            if ((Status != ""))
                            {
                                if ((x.STATUS == bool.Parse(Status)))
                                {
                                    ListaRetorno.Add(ret);
                                }
                            }
                            else
                            {
                                ListaRetorno.Add(ret);
                            }
                        }
                    }
                }
                else if (Tipo == 3)
                {
                    if (Tamanhos == "")
                    {
                        var selectTamanhos = db.TAMANHO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectTamanhos)
                        {
                            if (Tamanhos == "")
                            {
                                Tamanhos = x.ID_TAMANHO.ToString();
                            }
                            else
                            {
                                Tamanhos = Tamanhos + "," + x.ID_TAMANHO.ToString();
                            }
                        }
                    }

                    var arrayTamanhos = Tamanhos.Split(',');

                    var retorno = db.FORNECEDOR_PRODUTOs.Where(n =>
                        n.TIPO == Tipo && n.ID_FORNECEDOR == IdFornecedor && n.NOME.Contains(Dados)
                        && arrayTamanhos.Contains(n.TAMANHO.ToString())
                        ).ToList();

                    if (retorno != null)
                    {
                        foreach (var x in retorno)
                        {
                            Domain.ProdutoModel ret = new Domain.ProdutoModel();
                            ret.Ingredientes = x.INGREDIENTES;
                            ret.IdFonecedor = x.ID_FORNECEDOR;
                            ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                            ret.Valor = (decimal)x.VALOR;
                            ret.Tipo = (int)x.TIPO;
                            ret.Nome = x.NOME;
                            ret.Variacao = (int)x.VARIACAO;
                            ret.Tamanho = (int)x.TAMANHO;

                            if ((Status != ""))
                            {
                                if ((x.STATUS == bool.Parse(Status)))
                                {
                                    ListaRetorno.Add(ret);
                                }
                            }
                            else
                            {
                                ListaRetorno.Add(ret);
                            }
                        }
                    }
                }
                else
                {
                    var retorno = db.FORNECEDOR_PRODUTOs.Where(n =>
                        n.TIPO == Tipo && n.ID_FORNECEDOR == IdFornecedor && n.NOME.Contains(Dados)
                        ).ToList();

                    if (retorno != null)
                    {
                        foreach (var x in retorno)
                        {
                            Domain.ProdutoModel ret = new Domain.ProdutoModel();
                            ret.Ingredientes = x.INGREDIENTES;
                            ret.IdFonecedor = x.ID_FORNECEDOR;
                            ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                            ret.Valor = (decimal)x.VALOR;
                            ret.Tipo = (int)x.TIPO;
                            ret.Nome = x.NOME;
                            ret.Variacao = (int)x.VARIACAO;
                            ret.Tamanho = (int)x.TAMANHO;

                            if ((Status != ""))
                            {
                                if ((x.STATUS == bool.Parse(Status)))
                                {
                                    ListaRetorno.Add(ret);
                                }
                            }
                            else
                            {
                                ListaRetorno.Add(ret);
                            }
                        }

                    }
                }
            }
            else
            {
                if (Tipo == 1 || (Tipo == 2))
                {
                    if (Variacao == "")
                    {
                        var selectVariacao = db.VARIACAO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectVariacao)
                        {
                            if (Variacao == "")
                            {
                                Variacao = x.ID_VARIACAO.ToString();
                            }
                            else
                            {
                                Variacao = Variacao + "," + x.ID_VARIACAO.ToString();
                            }
                        }
                    }

                    var arrayVariacao = Variacao.Split(',');

                    if (Tamanhos == "")
                    {
                        var selectTamanhos = db.TAMANHO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectTamanhos)
                        {
                            if (Tamanhos == "")
                            {
                                Tamanhos = x.ID_TAMANHO.ToString();
                            }
                            else
                            {
                                Tamanhos = Tamanhos + "," + x.ID_TAMANHO.ToString();
                            }
                        }
                    }

                    var arrayTamanhos = Tamanhos.Split(',');

                    if (SubVariacao == "")
                    {
                        var selectSubVariacao = db.SUBVARIACAO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectSubVariacao)
                        {
                            if (SubVariacao == "")
                            {
                                SubVariacao = x.ID_SUBVARIACAO.ToString();
                            }
                            else
                            {
                                SubVariacao = SubVariacao + "," + x.ID_SUBVARIACAO.ToString();
                            }
                        }
                    }

                    var arraySubVariacao = SubVariacao.Split(',');

                    var retorno = db.FORNECEDOR_PRODUTOs.Where(n =>
                        n.TIPO == Tipo && n.ID_FORNECEDOR == IdFornecedor
                        && arrayVariacao.Contains(n.VARIACAO.ToString())
                        && arrayTamanhos.Contains(n.TAMANHO.ToString())
                        && arraySubVariacao.Contains(n.SUBVARIACAO.ToString())
                        ).ToList();

                    if (retorno != null)
                    {
                        foreach (var x in retorno)
                        {
                            Domain.ProdutoModel ret = new Domain.ProdutoModel();
                            ret.Ingredientes = x.INGREDIENTES;
                            ret.IdFonecedor = x.ID_FORNECEDOR;
                            ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                            ret.Valor = (decimal)x.VALOR;
                            ret.Tipo = (int)x.TIPO;
                            ret.Nome = x.NOME;
                            ret.Variacao = (int)x.VARIACAO;
                            ret.Tamanho = (int)x.TAMANHO;

                            if ((Status != ""))
                            {
                                if ((x.STATUS == bool.Parse(Status)))
                                {
                                    ListaRetorno.Add(ret);
                                }
                            }
                            else
                            {
                                ListaRetorno.Add(ret);
                            }
                        }
                    }

                }
                else if (Tipo == 3)
                {
                    if (Tamanhos == "")
                    {
                        var selectTamanhos = db.TAMANHO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == Tipo).ToList();

                        foreach (var x in selectTamanhos)
                        {
                            if (Tamanhos == "")
                            {
                                Tamanhos = x.ID_TAMANHO.ToString();
                            }
                            else
                            {
                                Tamanhos = Tamanhos + "," + x.ID_TAMANHO.ToString();
                            }
                        }
                    }

                    var arrayTamanhos = Tamanhos.Split(',');

                    var retorno = db.FORNECEDOR_PRODUTOs.Where(n =>
                        n.TIPO == Tipo && n.ID_FORNECEDOR == IdFornecedor
                        && arrayTamanhos.Contains(n.TAMANHO.ToString())
                        ).ToList();

                    if (retorno != null)
                    {
                        foreach (var x in retorno)
                        {
                            Domain.ProdutoModel ret = new Domain.ProdutoModel();
                            ret.Ingredientes = x.INGREDIENTES;
                            ret.IdFonecedor = x.ID_FORNECEDOR;
                            ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                            ret.Valor = (decimal)x.VALOR;
                            ret.Tipo = (int)x.TIPO;
                            ret.Nome = x.NOME;
                            ret.Variacao = (int)x.VARIACAO;
                            ret.Tamanho = (int)x.TAMANHO;

                            if ((Status != ""))
                            {
                                if ((x.STATUS == bool.Parse(Status)))
                                {
                                    ListaRetorno.Add(ret);
                                }
                            }
                            else
                            {
                                ListaRetorno.Add(ret);
                            }
                        }
                    }
                }
                else
                {
                    var retorno = db.FORNECEDOR_PRODUTOs.Where(n =>
                        n.TIPO == Tipo && n.ID_FORNECEDOR == IdFornecedor
                        ).ToList();

                    if (retorno != null)
                    {
                        foreach (var x in retorno)
                        {
                            Domain.ProdutoModel ret = new Domain.ProdutoModel();
                            ret.Ingredientes = x.INGREDIENTES;
                            ret.IdFonecedor = x.ID_FORNECEDOR;
                            ret.IdProduto = x.ID_FORNECEDOR_PRODUTO;
                            ret.Valor = (decimal)x.VALOR;
                            ret.Tipo = (int)x.TIPO;
                            ret.Nome = x.NOME;
                            ret.Variacao = (int)x.VARIACAO;
                            ret.Tamanho = (int)x.TAMANHO;

                            if ((Status != ""))
                            {
                                if ((x.STATUS == bool.Parse(Status)))
                                {
                                    ListaRetorno.Add(ret);
                                }
                            }
                            else
                            {
                                ListaRetorno.Add(ret);
                            }
                        }

                    }
                }
            }
            return ListaRetorno;
        }

        public string BuscaTipoProdutos(int IdFornecedor)
        {
            string Retorno = "";

            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO pizza = new FORNECEDOR_PRODUTO();
            if (IdFornecedor != 0)
            {
                var select = db.FORNECEDOR_PRODUTOs.Where(n =>
                    n.ID_FORNECEDOR == IdFornecedor).ToList();


                if (select != null)
                {
                    foreach (var x in select)
                    {
                        bool adiciona = false;
                        if (Retorno != "")
                        {
                            var splitretorno = Retorno.Split('^');

                            for (int i = 0; i < splitretorno.Length; i++)
                            {
                                var newsplit = splitretorno[i].Split('|');
                                if (x.TIPO == int.Parse(newsplit[0]))
                                {
                                    adiciona = false;
                                    break;
                                }
                                else
                                {
                                    adiciona = true;
                                }
                            }
                        }
                        else
                        {
                            adiciona = true;
                        }

                        if (adiciona)
                        {
                            var sltTipo = db.TIPO_PRODUTOs.Where(n =>
                                         n.ID_TIPO_PRODUTO == x.TIPO).FirstOrDefault();
                            if (sltTipo != null)
                            {
                                if (Retorno == "")
                                {
                                    Retorno = x.TIPO.ToString() + "|" + sltTipo.NOME_TIPO_PRODUTO;
                                }
                                else
                                {
                                    Retorno = Retorno + "^" + x.TIPO + "|" + sltTipo.NOME_TIPO_PRODUTO;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var x = db.TIPO_PRODUTOs.ToList();

                foreach (var sltTipo in x)
                {
                    if (Retorno == "")
                    {
                        Retorno = sltTipo.NOME_TIPO_PRODUTO;
                    }
                    else
                    {
                        Retorno = Retorno + "|" + sltTipo.NOME_TIPO_PRODUTO;
                    }
                }
            }

            return Retorno;

        }

        public bool AtualizarCadastroProduto(Domain.ProdutoModel Produto)
        {
            bool retorno = false;

            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_PRODUTO Prd = new FORNECEDOR_PRODUTO();
            if (Produto.IdFornecedorProduto != 0)
            {
                var select = db.FORNECEDOR_PRODUTOs.Where(n =>
                                n.ID_FORNECEDOR_PRODUTO == Produto.IdFornecedorProduto).FirstOrDefault();

                select.NOME = Produto.Nome;
                select.INGREDIENTES = Produto.Ingredientes;
                select.VALOR = Produto.Valor;
                select.TIPO = Produto.Tipo;
                select.STATUS = Produto.Status;
                select.PROMOCAO = Produto.Promocao;
                select.VALOR_PROMOCAO = Produto.ValorPromocao;
            }
            else
            {
                Prd.ID_FORNECEDOR = Produto.IdFonecedor;
                Prd.NOME = Produto.Nome;
                Prd.INGREDIENTES = Produto.Ingredientes;
                Prd.VALOR = Produto.Valor;
                Prd.TIPO = Produto.Tipo;
                Prd.STATUS = Produto.Status;
                Prd.PROMOCAO = Produto.Promocao;
                Prd.VALOR_PROMOCAO = Produto.ValorPromocao;

                db.FORNECEDOR_PRODUTOs.InsertOnSubmit(Prd);

            }

            db.SubmitChanges();

            return retorno;
        }

        public string InserirCEP(string CEP, int IdFornecedor, string Taxa)
        {
            string Retorno = "";

            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_CEP FornCep = new FORNECEDOR_CEP();

            var select2 = db.FORNECEDOR_CEPs.Where(n =>
                n.CEP == CEP && n.FORECEDOR_ID == IdFornecedor).FirstOrDefault();

            if (select2 == null)
            {
                var select = db.CEP_FULLs.Where(n =>
                                n.CEP == CEP).FirstOrDefault();

                FornCep.FORECEDOR_ID = IdFornecedor;
                FornCep.CEP = CEP;
                FornCep.BAIRRO = select.Bairro;
                FornCep.ESTADO = select.Estado;
                FornCep.MUNICIPIO = select.Cidade;
                FornCep.RUA = select.Rua;
                FornCep.TAXA = Taxa;

                db.FORNECEDOR_CEPs.InsertOnSubmit(FornCep);
                db.SubmitChanges();
                Retorno = "Sucesso";
            }
            else
            {
                Retorno = "Cep ja cadastrado";
            }

            return Retorno;

        }

        public string RemoverCepFornecedor(int IdFornecedor, string IdCep)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_CEP FornCep = new FORNECEDOR_CEP();

            string Retorno = "";

            var Select = (from fc in db.FORNECEDOR_CEPs
                          where fc.ID_CEP == int.Parse(IdCep) && fc.FORECEDOR_ID == IdFornecedor
                          select fc).FirstOrDefault();

            db.FORNECEDOR_CEPs.DeleteOnSubmit(Select);
            db.SubmitChanges();

            Retorno = "Registro Excluido";

            return Retorno;
        }

        public List<Domain.ProdutoModel> BuscarProdutosPorFornecedor(int IdFornecedor, string Dados)
        {
            IFSPDataContext db = new IFSPDataContext();
            List<Domain.ProdutoModel> ListaRet = new List<Domain.ProdutoModel>();

            var SplitDados = Dados.Split('^');

            var TipoProduto = db.TIPO_PRODUTOs.Where(n => n.NOME_TIPO_PRODUTO == Dados).FirstOrDefault();

            var x = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.TIPO == TipoProduto.ID_TIPO_PRODUTO && n.STATUS == true).ToList();

            foreach (var Pdr in x)
            {
                if (Pdr.STATUS != false)
                {
                    Domain.ProdutoModel Produto = new Domain.ProdutoModel();

                    Produto.IdFornecedorProduto = Pdr.ID_FORNECEDOR_PRODUTO;
                    Produto.Nome = Pdr.NOME;
                    Produto.Ingredientes = Pdr.INGREDIENTES;
                    Produto.Valor = (decimal)Pdr.VALOR;
                    Produto.Tamanho = (int)Pdr.TAMANHO;
                    Produto.SubVariacao = (int)Pdr.SUBVARIACAO;
                    Produto.Variacao = (int)Pdr.VARIACAO;

                    ListaRet.Add(Produto);
                }
            }

            return ListaRet;
        }

        public string SalvarCombo(string Ids, int IdFornecedor, string NomeCombo, string ValorCombo)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.FORNECEDOR_COMBOs.Where(n => n.ID_FORNECEDOR == IdFornecedor).ToList();

            if (select.Count < 5)
            {
                FORNECEDOR_COMBO fc = new FORNECEDOR_COMBO();

                fc.ID_FORNECEDOR = IdFornecedor;
                fc.ID_PRODUTOS = Ids;
                fc.NOME_COMBO = NomeCombo;
                fc.VALOR_COMBO = ValorCombo;
                fc.STATUS = true;

                db.FORNECEDOR_COMBOs.InsertOnSubmit(fc);

                db.SubmitChanges();
            }
            else
            {
                retorno = "Maximo de combos 5 já cadastrado.";
            }
            return retorno;
        }

        public List<Domain.FornecedorComboModel> MostraCombos(int IdFornecedor)
        {
            List<Domain.FornecedorComboModel> ListaRetorno = new List<Domain.FornecedorComboModel>();
            IFSPDataContext db = new IFSPDataContext();

            var x = db.FORNECEDOR_COMBOs.Where(n => n.ID_FORNECEDOR == IdFornecedor).ToList();

            foreach (var i in x)
            {
                Domain.FornecedorComboModel fcm = new Domain.FornecedorComboModel();
                List<Domain.ProdutoModel> ListaPrd = new List<Domain.ProdutoModel>();

                fcm.IdCombo = i.ID_COMBO;
                fcm.NomeCombo = i.NOME_COMBO;
                fcm.ValorCombo = i.VALOR_COMBO;
                fcm.Status = (bool)i.STATUS;

                var idPrd = i.ID_PRODUTOS.Split(';');
                for (int c = 0; c < idPrd.Length; c++)
                {
                    Domain.ProdutoModel Prd = new Domain.ProdutoModel();

                    var p = db.FORNECEDOR_PRODUTOs.Where(k => k.ID_FORNECEDOR_PRODUTO == int.Parse(idPrd[c]) && k.ID_FORNECEDOR == IdFornecedor).FirstOrDefault();

                    if (p != null)
                    {
                        Prd.IdProduto = p.ID_FORNECEDOR_PRODUTO;
                        Prd.Nome = p.NOME;
                        Prd.Ingredientes = p.INGREDIENTES;
                        Prd.Valor = (decimal)p.VALOR;
                        Prd.Status = (bool)p.STATUS;
                        ListaPrd.Add(Prd);
                    }
                }
                fcm.Produtos = ListaPrd;

                ListaRetorno.Add(fcm);
            }


            return ListaRetorno;
        }

        public string DeletarCombo(int Id)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            FORNECEDOR_CEP FornCep = new FORNECEDOR_CEP();

            string Retorno = "";

            var Select = (from fc in db.FORNECEDOR_COMBOs
                          where fc.ID_COMBO == Id
                          select fc).FirstOrDefault();
            //deletar combo
            db.FORNECEDOR_COMBOs.DeleteOnSubmit(Select);
            db.SubmitChanges();

            Retorno = "Registro Excluido";

            return Retorno;
        }

        public string AlterarSenha(string Senha, int IdFornecedor, string OldSenha)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            FORNECEDOR forn = new FORNECEDOR();

            forn = db.FORNECEDORs.Where(n => n.ID_UTILIZADOR == IdFornecedor).FirstOrDefault();

            if (forn != null)
            {
                if (forn.SENHA == OldSenha)
                {
                    forn.SENHA = Senha;

                    db.SubmitChanges();
                }
                else
                {
                    retorno = "Senha atual incorreta.";
                }
            }

            return retorno;
        }

        public List<Domain.PedidoModel> GeraRelatorio(int IdFornecedor, string DataInicio, string DataFim, string Status, string Produto, string Valor, int IdFornecedorDelivery, int TipoProduto)
        {
            List<Domain.PedidoModel> Retorno = new List<Domain.PedidoModel>();
            IFSPDataContext db = new IFSPDataContext();
            string IdStatus = "";
            bool TipoProdutoIgual = false;
            bool inseriu = false;
            Valor = Valor.Replace("R$", "");

            Valor.TrimEnd();
            Valor.TrimStart();

            var arrayValores = Valor.Split('-');

            if (Status == "Finalizado")
            {
                IdStatus = "1";
            }
            else if (Status == "Cancelado")
            {
                IdStatus = "2";
            }
            else
            {
                IdStatus = "";
            }

            if ((IdStatus == "") && (IdFornecedorDelivery == 0))
            {

                var select = db.PEDIDO_FULLs.Where(n => n.ID_FORNECEDOR == IdFornecedor
                    && n.DATA >= DateTime.Parse(DataInicio) && n.DATA <= DateTime.Parse(DataFim)
                    ).ToList().OrderByDescending(m => m.DATA);

                foreach (var x in select)
                {
                    TipoProdutoIgual = false;
                    inseriu = false;
                    Domain.PedidoModel Pd = new Domain.PedidoModel();
                    List<Domain.ItemPedidoModel> ListaItensPedido = new List<Domain.ItemPedidoModel>();

                    Pd.Cep = x.CEP;
                    Pd.IdPedidoFull = x.ID_PEDIDO_FULL;
                    Pd.Data = (DateTime)x.DATA;
                    Pd.Desconto = (double)x.DESCONTO;
                    Pd.FormaPagamento = x.FORMA_PAGAMENTO;
                    Pd.Hora = (TimeSpan)x.HORA;
                    Pd.NotaFiscal = (Boolean)x.NOTAFISCAL;
                    Pd.Numero = (int)x.NUMERO;
                    Pd.Observacoes = x.OBSERVACOES;
                    Pd.Rua = x.RUA;
                    Pd.IdCliente = x.ID_CLIENTE;
                    Pd.IdDelivery = x.ID_DELIVERY;
                    Pd.Status = x.STATUS;
                    Pd.Troco = x.TROCO.ToString();
                    Pd.ValorTotal = x.VALOR_TOTAL.ToString();
                    Pd.NumeroPedido = (int)x.NUMERO_PEDIDO;

                    var selectItem = db.ITENS_PEDIDOs.Where(n => n.PEDIDO_ID == Pd.IdPedidoFull).ToList();

                    foreach (var item in selectItem)
                    {
                        Domain.ItemPedidoModel ItemPedido = new Domain.ItemPedidoModel();

                        ItemPedido.IdPedido = Pd.IdPedidoFull;
                        ItemPedido.Quantidade = item.QUANTIDADE;
                        ItemPedido.IdItemPedido = item.ITEM_PEDIDO_ID;
                        ItemPedido.IdProduto = item.ID_PRODUTO;

                        if (TipoProduto != 0)
                        {
                            if (ItemPedido.IdProduto.Contains('-'))
                            {
                                var Ids = ItemPedido.IdProduto.Split('-');
                                if ((TipoProdutoIgual == false) &&
                                    ((TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    || (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    ))
                                {
                                    TipoProdutoIgual = true;
                                }

                            }
                            else
                            {
                                if ((TipoProdutoIgual == false) && (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(item.ID_PRODUTO)).FirstOrDefault().TIPO))
                                {
                                    TipoProdutoIgual = true;
                                }
                            }
                        }
                        else
                        {
                            TipoProdutoIgual = true;
                        }
                        ListaItensPedido.Add(ItemPedido);
                    }
                    Pd.ItensPedido = ListaItensPedido;

                    if ((Produto != "") && (Valor != ""))
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Produto != "")
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Valor != "")
                    {
                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                        {
                            if (TipoProdutoIgual == true)
                            {
                                Retorno.Add(Pd);
                            }
                        }
                    }
                    else
                    {
                        if (TipoProdutoIgual == true)
                        {
                            Retorno.Add(Pd);
                        }
                    }

                }
            }
            else if ((IdFornecedorDelivery != 0) && (IdStatus == ""))
            {
                var select = db.PEDIDO_FULLs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.DATA >= DateTime.Parse(DataInicio) && n.DATA <= DateTime.Parse(DataFim)
                    && n.ID_DELIVERY == IdFornecedorDelivery).ToList().OrderByDescending(m => m.DATA);
                foreach (var x in select)
                {
                    TipoProdutoIgual = false;
                    inseriu = false;
                    Domain.PedidoModel Pd = new Domain.PedidoModel();
                    List<Domain.ItemPedidoModel> ListaItensPedido = new List<Domain.ItemPedidoModel>();

                    Pd.Cep = x.CEP;
                    Pd.Data = (DateTime)x.DATA;
                    Pd.IdPedidoFull = x.ID_PEDIDO_FULL;
                    Pd.Desconto = (double)x.DESCONTO;
                    Pd.FormaPagamento = x.FORMA_PAGAMENTO;
                    Pd.Hora = (TimeSpan)x.HORA;
                    Pd.NotaFiscal = (Boolean)x.NOTAFISCAL;
                    Pd.Numero = (int)x.NUMERO;
                    Pd.Observacoes = x.OBSERVACOES;
                    Pd.Rua = x.RUA;
                    Pd.IdCliente = x.ID_CLIENTE;
                    Pd.IdDelivery = x.ID_DELIVERY;
                    Pd.Status = x.STATUS;
                    Pd.Troco = x.TROCO.ToString();
                    Pd.ValorTotal = x.VALOR_TOTAL.ToString();
                    Pd.NumeroPedido = (int)x.NUMERO_PEDIDO;

                    var selectItem = db.ITENS_PEDIDOs.Where(n => n.PEDIDO_ID == Pd.IdPedidoFull).ToList();
                    foreach (var item in selectItem)
                    {
                        Domain.ItemPedidoModel ItemPedido = new Domain.ItemPedidoModel();

                        ItemPedido.IdPedido = Pd.IdPedidoFull;
                        ItemPedido.Quantidade = item.QUANTIDADE;
                        ItemPedido.IdItemPedido = item.ITEM_PEDIDO_ID;
                        ItemPedido.IdProduto = item.ID_PRODUTO;

                        if (TipoProduto != 0)
                        {
                            if (ItemPedido.IdProduto.Contains('-'))
                            {
                                var Ids = ItemPedido.IdProduto.Split('-');
                                if ((TipoProdutoIgual == false) &&
                                    ((TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    || (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    ))
                                {
                                    TipoProdutoIgual = true;
                                }

                            }
                            else
                            {
                                if ((TipoProdutoIgual == false) && (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(item.ID_PRODUTO)).FirstOrDefault().TIPO))
                                {
                                    TipoProdutoIgual = true;
                                }
                            }
                        }
                        else
                        {
                            TipoProdutoIgual = true;
                        }
                        ListaItensPedido.Add(ItemPedido);
                    }
                    Pd.ItensPedido = ListaItensPedido;

                    if ((Produto != "") && (Valor != ""))
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Produto != "")
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Valor != "")
                    {
                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                        {
                            if (TipoProdutoIgual == true)
                            {
                                Retorno.Add(Pd);
                            }
                        }
                    }
                    else
                    {
                        if (TipoProdutoIgual == true)
                        {
                            Retorno.Add(Pd);
                        }
                    }
                }
            }
            else if ((IdFornecedorDelivery != 0) && (IdStatus != ""))
            {
                var select = db.PEDIDO_FULLs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.DATA >= DateTime.Parse(DataInicio) && n.DATA <= DateTime.Parse(DataFim)
                    && n.ID_DELIVERY == IdFornecedorDelivery && n.STATUS == IdStatus).ToList().OrderByDescending(m => m.DATA);
                foreach (var x in select)
                {
                    TipoProdutoIgual = false;
                    inseriu = false;
                    Domain.PedidoModel Pd = new Domain.PedidoModel();
                    List<Domain.ItemPedidoModel> ListaItensPedido = new List<Domain.ItemPedidoModel>();

                    Pd.Cep = x.CEP;
                    Pd.Data = (DateTime)x.DATA;
                    Pd.IdPedidoFull = x.ID_PEDIDO_FULL;
                    Pd.Desconto = (double)x.DESCONTO;
                    Pd.FormaPagamento = x.FORMA_PAGAMENTO;
                    Pd.Hora = (TimeSpan)x.HORA;
                    Pd.NotaFiscal = (Boolean)x.NOTAFISCAL;
                    Pd.Numero = (int)x.NUMERO;
                    Pd.Observacoes = x.OBSERVACOES;
                    Pd.Rua = x.RUA;
                    Pd.IdCliente = x.ID_CLIENTE;
                    Pd.IdDelivery = x.ID_DELIVERY;
                    Pd.Status = x.STATUS;
                    Pd.Troco = x.TROCO.ToString();
                    Pd.ValorTotal = x.VALOR_TOTAL.ToString();
                    Pd.NumeroPedido = (int)x.NUMERO_PEDIDO;

                    var selectItem = db.ITENS_PEDIDOs.Where(n => n.PEDIDO_ID == Pd.IdPedidoFull).ToList();
                    foreach (var item in selectItem)
                    {
                        Domain.ItemPedidoModel ItemPedido = new Domain.ItemPedidoModel();

                        ItemPedido.IdPedido = Pd.IdPedidoFull;
                        ItemPedido.Quantidade = item.QUANTIDADE;
                        ItemPedido.IdItemPedido = item.ITEM_PEDIDO_ID;
                        ItemPedido.IdProduto = item.ID_PRODUTO;

                        if (TipoProduto != 0)
                        {
                            if (ItemPedido.IdProduto.Contains('-'))
                            {
                                var Ids = ItemPedido.IdProduto.Split('-');
                                if ((TipoProdutoIgual == false) &&
                                    ((TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    || (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    ))
                                {
                                    TipoProdutoIgual = true;
                                }

                            }
                            else
                            {
                                if ((TipoProdutoIgual == false) && (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(item.ID_PRODUTO)).FirstOrDefault().TIPO))
                                {
                                    TipoProdutoIgual = true;
                                }
                            }
                        }
                        else
                        {
                            TipoProdutoIgual = true;
                        }
                        ListaItensPedido.Add(ItemPedido);
                    }
                    Pd.ItensPedido = ListaItensPedido;

                    if ((Produto != "") && (Valor != ""))
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Produto != "")
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Valor != "")
                    {
                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                        {
                            if (TipoProdutoIgual == true)
                            {
                                Retorno.Add(Pd);
                            }
                        }
                    }
                    else
                    {
                        if (TipoProdutoIgual == true)
                        {
                            Retorno.Add(Pd);
                        }
                    }
                }
            }
            else
            {
                var select = db.PEDIDO_FULLs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.STATUS == IdStatus && n.DATA >= DateTime.Parse(DataInicio)
                    && n.DATA <= DateTime.Parse(DataFim)).ToList().OrderByDescending(m => m.DATA);
                foreach (var x in select)
                {
                    TipoProdutoIgual = false;
                    inseriu = false;
                    Domain.PedidoModel Pd = new Domain.PedidoModel();
                    List<Domain.ItemPedidoModel> ListaItensPedido = new List<Domain.ItemPedidoModel>();

                    Pd.Cep = x.CEP;
                    Pd.IdPedidoFull = x.ID_PEDIDO_FULL;
                    Pd.Data = (DateTime)x.DATA;
                    Pd.Desconto = (double)x.DESCONTO;
                    Pd.FormaPagamento = x.FORMA_PAGAMENTO;
                    Pd.Hora = (TimeSpan)x.HORA;
                    Pd.NotaFiscal = (Boolean)x.NOTAFISCAL;
                    Pd.Numero = (int)x.NUMERO;
                    Pd.Observacoes = x.OBSERVACOES;
                    Pd.Rua = x.RUA;
                    Pd.IdCliente = x.ID_CLIENTE;
                    Pd.IdDelivery = x.ID_DELIVERY;
                    Pd.Status = x.STATUS;
                    Pd.Troco = x.TROCO.ToString();
                    Pd.ValorTotal = x.VALOR_TOTAL.ToString();
                    Pd.NumeroPedido = (int)x.NUMERO_PEDIDO;

                    var selectItem = db.ITENS_PEDIDOs.Where(n => n.PEDIDO_ID == Pd.IdPedidoFull).ToList();

                    foreach (var item in selectItem)
                    {
                        Domain.ItemPedidoModel ItemPedido = new Domain.ItemPedidoModel();

                        ItemPedido.IdPedido = Pd.IdPedidoFull;
                        ItemPedido.Quantidade = item.QUANTIDADE;
                        ItemPedido.IdItemPedido = item.ITEM_PEDIDO_ID;
                        ItemPedido.IdProduto = item.ID_PRODUTO;

                        if (TipoProduto != 0)
                        {
                            if (ItemPedido.IdProduto.Contains('-'))
                            {
                                var Ids = ItemPedido.IdProduto.Split('-');
                                if ((TipoProdutoIgual == false) &&
                                    ((TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    || (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Ids[0])).FirstOrDefault().TIPO)
                                    ))
                                {
                                    TipoProdutoIgual = true;
                                }

                            }
                            else
                            {
                                if ((TipoProdutoIgual == false) && (TipoProduto == db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(item.ID_PRODUTO)).FirstOrDefault().TIPO))
                                {
                                    TipoProdutoIgual = true;
                                }
                            }
                        }
                        else
                        {
                            TipoProdutoIgual = true;
                        }

                        ListaItensPedido.Add(ItemPedido);
                    }
                    Pd.ItensPedido = ListaItensPedido;

                    if ((Produto != "") && (Valor != ""))
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                                        {
                                            if (TipoProdutoIgual == true)
                                            {
                                                Retorno.Add(Pd);
                                                inseriu = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Produto != "")
                    {
                        var SelectIdProduto = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR == IdFornecedor && fp.NOME == Produto).ToList();
                        foreach (var i in SelectIdProduto)
                        {
                            foreach (var item in Pd.ItensPedido)
                            {
                                if (item.IdProduto.Contains('-'))
                                {
                                    var idsMeia = item.IdProduto.Split('-');
                                    if ((int.Parse(idsMeia[0]) == i.ID_FORNECEDOR_PRODUTO) || (int.Parse(idsMeia[1]) == i.ID_FORNECEDOR_PRODUTO))
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (int.Parse(item.IdProduto) == i.ID_FORNECEDOR_PRODUTO)
                                    {
                                        if (TipoProdutoIgual == true)
                                        {
                                            Retorno.Add(Pd);
                                            inseriu = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (inseriu == true)
                            {
                                break;
                            }
                        }
                    }
                    else if (Valor != "")
                    {
                        if (double.Parse(Pd.ValorTotal) >= double.Parse(arrayValores[0]) && double.Parse(Pd.ValorTotal) <= double.Parse(arrayValores[1]))
                        {
                            if (TipoProdutoIgual == true)
                            {
                                Retorno.Add(Pd);
                            }
                        }
                    }
                    else
                    {
                        if (TipoProdutoIgual == true)
                        {
                            Retorno.Add(Pd);
                        }
                    }
                }
            }
            return Retorno;
        }

        //public List<Domain.PedidoGraficoModel> GeraGrafico(int IdFornecedor, string DataInicio, string DataFim, string Status, string Produto, string Valor)
        //{
        //    List<Domain.PedidoGraficoModel> Retorno = new List<Domain.PedidoGraficoModel>();

        //    IFSPDataContext db = new IFSPDataContext();
        //    string IdStatus = "";

        //    Valor = Valor.Replace("R$", "");

        //    Valor.TrimEnd();
        //    Valor.TrimStart();

        //    var arrayValores = Valor.Split('-');

        //    if (Status == "Finalizado")
        //    {
        //        IdStatus = "1";
        //    }
        //    else if (Status == "Cancelado")
        //    {
        //        IdStatus = "2";
        //    }
        //    else
        //    {
        //        IdStatus = "";
        //    }

        //    if (IdStatus == "")
        //    {

        //        var select = db.PEDIDO_FULLs.Where(n => n.ID_FORNECEDOR == IdFornecedor
        //            && n.DATA >= DateTime.Parse(DataInicio) && n.DATA <= DateTime.Parse(DataFim)
        //            ).GroupBy(n => n.DATA).Select(p => new
        //        {
        //            Data = p.Key,
        //            Total = p.GroupBy(pedidos => pedidos.ID_PEDIDO_FULL).Count()
        //        });

        //        //var select2 = db.PEDIDO_FULLs.Where(n => n.ID_FORNECEDOR == IdFornecedor
        //        //    && n.DATA >= DateTime.Parse(DataInicio) && n.DATA <= DateTime.Parse(DataFim)
        //        //    ).ToList().OrderByDescending(m => m.DATA);

        //        //var todosProdutosPorHora = vendas.GroupBy(v => v.DataHora.Hour)
        //        //                    .Select(g => new
        //        //                    {
        //        //                        Hora = g.Key,
        //        //                        Total = g.Sum(venda =>
        //        //                                    venda.ProdutosVendidos.Sum(pv => pv.Quantidade))
        //        //                    });
        //        foreach (var x in select)
        //        {
        //            Domain.PedidoGraficoModel Pd = new Domain.PedidoGraficoModel();

        //            Pd.Data = (DateTime)x.Data;
        //            Pd.Qnt = x.Total;

        //            Retorno.Add(Pd);

        //        }
        //    }
        //    else
        //    {
        //        var select = db.PEDIDO_FULLs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.STATUS == IdStatus && n.DATA >= DateTime.Parse(DataInicio) && n.DATA <= DateTime.Parse(DataFim)).ToList().OrderByDescending(m => m.DATA);
        //        foreach (var x in select)
        //        {
        //            Domain.PedidoGraficoModel Pd = new Domain.PedidoGraficoModel();

        //            Pd.Data = (DateTime)x.DATA;


        //            Retorno.Add(Pd);

        //        }
        //    }

        //    return Retorno;
        //}

        public Domain.ClienteModel BuscarCliente(int IdCliente)
        {
            IFSPDataContext db = new IFSPDataContext();
            Domain.ClienteModel Retorno = new Domain.ClienteModel();

            var select = db.CLIENTEs.Where(n => n.ID_CLIENTE == IdCliente).FirstOrDefault();

            Retorno.Cep = select.CEP;
            Retorno.Cpf = select.CPF;
            Retorno.Email = select.EMAIL;
            Retorno.NomeCliente = select.NOME_CLIENTE;
            Retorno.Telefones = select.TELEFONES;

            return Retorno;
        }

        public string BuscaDadosProduto(string TipoProduto, string Variacao, string SubVariacao, string Tamanho)
        {
            string retorno = "";
            IFSPDataContext db = new IFSPDataContext();

            var select = db.TIPO_PRODUTOs.Where(n => n.NOME_TIPO_PRODUTO == TipoProduto).FirstOrDefault();
            if (TipoProduto == "PIZZA")
            {
                if (select != null)
                {
                    var selectVariacao = db.VARIACAO_PRODUTOs.Where(v => v.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO && v.NOME_VARIACAO == Variacao).FirstOrDefault();

                    if (selectVariacao != null)
                    {
                        var selectTamanho = db.TAMANHO_PRODUTOs.Where(t => t.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO && t.NOME_TAMANHO == Tamanho).FirstOrDefault();

                        if (selectTamanho != null)
                        {
                            var selectSubVariacao = db.SUBVARIACAO_PRODUTOs.Where(sv => sv.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO && sv.NOME_SUBVARIACAO == SubVariacao).FirstOrDefault();

                            if (selectSubVariacao != null)
                            {
                                retorno = "" + select.ID_TIPO_PRODUTO + "|" + selectVariacao.ID_VARIACAO + "|" +
                                          "" + selectTamanho.ID_TAMANHO + "|" + selectSubVariacao.ID_SUBVARIACAO + "";
                            }
                            else
                            {
                                retorno = "SubVariação de Produto" + SubVariacao + " não encontrado";
                            }
                        }
                        else
                        {
                            retorno = "Tamanho de Produto" + Tamanho + " não encontrado";
                        }
                    }
                    else
                    {
                        retorno = "Variação de Produto" + Variacao + " não encontrado";
                    }
                }
                else
                {
                    retorno = "Tipo de Produto" + TipoProduto + " não encontrado";
                }
            }
            else
            {
                if (select != null)
                {
                    var selectTamanho = db.TAMANHO_PRODUTOs.Where(t => t.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO && t.NOME_TAMANHO == Tamanho).FirstOrDefault();

                    if (selectTamanho != null)
                    {
                        retorno = "" + select.ID_TIPO_PRODUTO + "|0|" +
                                 "" + selectTamanho.ID_TAMANHO + "|0";

                    }
                    else
                    {
                        retorno = "Tamanho de Produto" + Tamanho + " não encontrado";
                    }

                }
                else
                {
                    retorno = "Tipo de Produto" + TipoProduto + " não encontrado";
                }
            }

            return retorno;
        }

        public string CadastraFuncionario(int IdFornecedor, string Nome, string Cpf, string TpTransporte, string cnh, string Placa)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();
            FORNECEDOR_DELIVERY fd = new FORNECEDOR_DELIVERY();

            fd = db.FORNECEDOR_DELIVERies.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.CPF == Cpf).FirstOrDefault();

            if (fd == null)
            {
                fd = new FORNECEDOR_DELIVERY();

                fd.NOME = Nome;
                fd.ID_FORNECEDOR = IdFornecedor;
                fd.TIPO_TRANSPORTE = TpTransporte;
                fd.IDENTIFICACAO = cnh;
                fd.PLACA = Placa;
                fd.CPF = Cpf;

                db.FORNECEDOR_DELIVERies.InsertOnSubmit(fd);

                retorno = "registro salvo com sucesso";
            }
            else
            {
                fd.NOME = Nome;
                fd.ID_FORNECEDOR = IdFornecedor;
                fd.TIPO_TRANSPORTE = TpTransporte;
                fd.IDENTIFICACAO = cnh;
                fd.PLACA = Placa;

                retorno = "registro alterado com sucesso.";
            }

            db.SubmitChanges();

            return retorno;
        }

        public List<Domain.FornecedorDeliveryModel> ListaFuncionario(int IdFornecedor)
        {
            IFSPDataContext db = new IFSPDataContext();
            List<Domain.FornecedorDeliveryModel> retorno = new List<Domain.FornecedorDeliveryModel>();

            var select = db.FORNECEDOR_DELIVERies.Where(n => n.ID_FORNECEDOR == IdFornecedor).ToList();

            foreach (var x in select)
            {
                Domain.FornecedorDeliveryModel fd = new Domain.FornecedorDeliveryModel();

                fd.Nome = x.NOME;
                fd.IdFornecedorTransporte = x.ID_FORNECEDOR_TRANSPORTE.ToString();
                fd.TipoTransporte = x.TIPO_TRANSPORTE;
                fd.Identificacao = x.IDENTIFICACAO;
                fd.Cpf = x.CPF;
                fd.Placa = x.PLACA;

                retorno.Add(fd);
            }

            return retorno;
        }

        public string ListaFornecedores()
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.FORNECEDORs.ToList();

            foreach (var x in select)
            {
                if (retorno == "")
                {
                    retorno = x.NOME;
                }
                else
                {
                    retorno = retorno + "|" + x.NOME;
                }
            }

            return retorno;
        }

        public string GetTamanhoTipo(string Tipo, int IdFornecedor)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.TIPO_PRODUTOs.Where(n => n.NOME_TIPO_PRODUTO == Tipo).FirstOrDefault();

            var selectTamanho = db.TAMANHO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO).ToList();

            foreach (var x in selectTamanho)
            {
                if (retorno == "")
                {
                    retorno = x.ID_TAMANHO + "^" + x.NOME_TAMANHO;
                }
                else
                {
                    retorno = retorno + "|" + x.ID_TAMANHO + "^" + x.NOME_TAMANHO;
                }
            }

            return retorno;
        }

        public string GetTamanhoTipoId(string Tipo, int IdFornecedor)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.TIPO_PRODUTOs.Where(n => n.ID_TIPO_PRODUTO == int.Parse(Tipo)).FirstOrDefault();

            var selectTamanho = db.TAMANHO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO).ToList();

            foreach (var x in selectTamanho)
            {
                if (retorno == "")
                {
                    retorno = x.ID_TAMANHO + "^" + x.NOME_TAMANHO;
                }
                else
                {
                    retorno = retorno + "|" + x.ID_TAMANHO + "^" + x.NOME_TAMANHO;
                }
            }

            return retorno;
        }

        public string GetBordas(int IdFornecedor)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var selectBorda = db.BORDAs.Where(tp => tp.ID_FORNECEDOR == 1).ToList();

            foreach (var x in selectBorda)
            {
                if (retorno == "")
                {
                    retorno = x.ID_BORDA + "^" + x.NOME_BORDA;
                }
                else
                {
                    retorno = retorno + "|" + x.ID_BORDA + "^" + x.NOME_BORDA;
                }
            }

            return retorno;
        }

        public string GetCarregaSubVariacao(string Tipo)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.TIPO_PRODUTOs.Where(n => n.NOME_TIPO_PRODUTO == Tipo).FirstOrDefault();

            var selectTamanho = db.SUBVARIACAO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO).ToList();

            foreach (var x in selectTamanho)
            {
                if (retorno == "")
                {
                    retorno = x.ID_SUBVARIACAO + "^" + x.NOME_SUBVARIACAO;
                }
                else
                {
                    retorno = retorno + "|" + x.ID_SUBVARIACAO + "^" + x.NOME_SUBVARIACAO;
                }
            }

            return retorno;
        }

        public string GetCarregaVariacao(string Tipo)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.TIPO_PRODUTOs.Where(n => n.NOME_TIPO_PRODUTO == Tipo).FirstOrDefault();

            var selectVariacao = db.VARIACAO_PRODUTOs.Where(tp => tp.ID_TIPO_PRODUTO == select.ID_TIPO_PRODUTO).ToList();

            foreach (var x in selectVariacao)
            {
                if (retorno == "")
                {
                    retorno = x.ID_VARIACAO + "^" + x.NOME_VARIACAO;
                }
                else
                {
                    retorno = retorno + "|" + x.ID_VARIACAO + "^" + x.NOME_VARIACAO;
                }
            }


            return retorno;
        }

        public string SalvarProdutosFornecedor(string Tamanhos, string Fornecedor, string Produto, string SubVariacao)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var selecfornecedor = db.FORNECEDORs.Where(f => f.NOME == Fornecedor).FirstOrDefault();

            if (Produto == "BEBIDAS")
            {
                List<int> ArrayId = new List<int>();
                var arrID = Tamanhos.Split(',');

                var selectTamanhos = (from tp in db.TAMANHO_PRODUTOs
                                      where arrID.Contains(tp.ID_TAMANHO.ToString())
                                      select tp).ToList();

                var selectBebidas = (from b in db.BEBIDAs
                                     select b).ToList();

                foreach (var t in selectTamanhos)
                {
                    foreach (var b in selectBebidas)
                    {

                        var existe = db.FORNECEDOR_PRODUTOs.Any(sp => sp.ID_FORNECEDOR == selecfornecedor.ID_UTILIZADOR && sp.TIPO == b.ID_TIPO_PRODUTO
                            && sp.TAMANHO == t.ID_TAMANHO && sp.NOME == b.NOME_BEBIDA);

                        if (existe == false)
                        {
                            ArrayId.Add(ArrayId.Count() + 1);

                            FORNECEDOR_PRODUTO fp = new FORNECEDOR_PRODUTO();

                            fp.ID_FORNECEDOR = selecfornecedor.ID_UTILIZADOR;
                            fp.TAMANHO = t.ID_TAMANHO;
                            fp.VALOR = 0;
                            fp.STATUS = false;
                            fp.VALOR_PROMOCAO = 0;
                            fp.TIPO = t.ID_TIPO_PRODUTO;
                            fp.QTD_ESTOQUE = 0;
                            fp.INGREDIENTES = null;
                            fp.PROMOCAO = false;
                            fp.SUBVARIACAO = 0;
                            fp.VARIACAO = 0;
                            fp.NOME = b.NOME_BEBIDA;
                            fp.INGREDIENTES = null;

                            db.FORNECEDOR_PRODUTOs.InsertOnSubmit(fp);
                        }

                    }
                }
                if (ArrayId.Count() == 0)
                {
                    retorno = "Já foram inseridos Produtos do tipo " + Produto + " com os tamanhos selecionados.";
                }
                else
                {
                    db.SubmitChanges();
                    retorno = "Sucesso";
                }
            }
            else if (Produto == "PIZZA")
            {
                List<int> ArrayId = new List<int>();
                var arrID = Tamanhos.Split(',');
                var arrIDs = SubVariacao.Split(',');

                var selectTamanhos = (from tp in db.TAMANHO_PRODUTOs
                                      where arrID.Contains(tp.ID_TAMANHO.ToString())
                                      select tp).ToList();

                var selectSubVariacoes = (from sp in db.SUBVARIACAO_PRODUTOs
                                          where arrIDs.Contains(sp.ID_SUBVARIACAO.ToString())
                                          select sp).ToList();

                var selectPizza = (from p in db.PIZZAs
                                   select p).ToList();

                foreach (var t in selectTamanhos)
                {
                    foreach (var s in selectSubVariacoes)
                    {
                        foreach (var p in selectPizza)
                        {

                            var existe = db.FORNECEDOR_PRODUTOs.Any(sp => sp.ID_FORNECEDOR == selecfornecedor.ID_UTILIZADOR && sp.TIPO == p.ID_TIPO_PRODUTO
                                && sp.SUBVARIACAO == s.ID_SUBVARIACAO && sp.TAMANHO == t.ID_TAMANHO && sp.NOME == p.NOME_PIZZA);

                            if (existe == false)
                            {
                                ArrayId.Add(ArrayId.Count() + 1);

                                FORNECEDOR_PRODUTO fp = new FORNECEDOR_PRODUTO();

                                fp.ID_FORNECEDOR = selecfornecedor.ID_UTILIZADOR;
                                fp.TAMANHO = t.ID_TAMANHO;
                                fp.VALOR = 0;
                                fp.STATUS = false;
                                fp.VALOR_PROMOCAO = 0;
                                fp.TIPO = t.ID_TIPO_PRODUTO;
                                fp.QTD_ESTOQUE = 0;
                                fp.INGREDIENTES = null;
                                fp.PROMOCAO = false;
                                fp.SUBVARIACAO = s.ID_SUBVARIACAO;
                                fp.VARIACAO = p.ID_VARIACAO;
                                fp.NOME = p.NOME_PIZZA;
                                fp.INGREDIENTES = p.INGREDIENTES;

                                db.FORNECEDOR_PRODUTOs.InsertOnSubmit(fp);
                            }
                        }
                    }
                }
                if (ArrayId.Count() == 0)
                {
                    retorno = "Já foram inseridos produtos do tipo " + Produto + " com os tamanhos selecionados e SubVariações selecionadas.";
                }
                else
                {
                    db.SubmitChanges();
                    retorno = "Sucesso";
                }
            }
            else if (Produto == "PIZZA DOCE")
            {
                List<int> ArrayId = new List<int>();
                var arrID = Tamanhos.Split(',');
                var arrIDs = SubVariacao.Split(',');

                var selectTamanhos = (from tp in db.TAMANHO_PRODUTOs
                                      where arrID.Contains(tp.ID_TAMANHO.ToString())
                                      select tp).ToList();

                var selectSubVariacoes = (from sp in db.SUBVARIACAO_PRODUTOs
                                          where arrIDs.Contains(sp.ID_SUBVARIACAO.ToString())
                                          select sp).ToList();

                var selectPizza = (from p in db.PIZZAs
                                   select p).ToList();

                foreach (var t in selectTamanhos)
                {
                    foreach (var s in selectSubVariacoes)
                    {
                        foreach (var p in selectPizza)
                        {

                            var existe = db.FORNECEDOR_PRODUTOs.Any(sp => sp.ID_FORNECEDOR == selecfornecedor.ID_UTILIZADOR && sp.TIPO == p.ID_TIPO_PRODUTO
                                && sp.SUBVARIACAO == s.ID_SUBVARIACAO && sp.TAMANHO == t.ID_TAMANHO && sp.NOME == p.NOME_PIZZA);

                            if (existe == false)
                            {
                                ArrayId.Add(ArrayId.Count() + 1);

                                FORNECEDOR_PRODUTO fp = new FORNECEDOR_PRODUTO();

                                fp.ID_FORNECEDOR = selecfornecedor.ID_UTILIZADOR;
                                fp.TAMANHO = t.ID_TAMANHO;
                                fp.VALOR = 0;
                                fp.STATUS = false;
                                fp.VALOR_PROMOCAO = 0;
                                fp.TIPO = t.ID_TIPO_PRODUTO;
                                fp.QTD_ESTOQUE = 0;
                                fp.INGREDIENTES = null;
                                fp.PROMOCAO = false;
                                fp.SUBVARIACAO = s.ID_SUBVARIACAO;
                                fp.VARIACAO = p.ID_VARIACAO;
                                fp.NOME = p.NOME_PIZZA;
                                fp.INGREDIENTES = p.INGREDIENTES;

                                db.FORNECEDOR_PRODUTOs.InsertOnSubmit(fp);
                            }
                        }
                    }
                }
                if (ArrayId.Count() == 0)
                {
                    retorno = "Já foram inseridos produtos do tipo " + Produto + " com os tamanhos selecionados e SubVariações selecionadas.";
                }
                else
                {
                    db.SubmitChanges();
                    retorno = "Sucesso";
                }
            }
            else if (Produto == "ESFIHA")
            {
                List<int> ArrayId = new List<int>();
                var arrID = Tamanhos.Split(',');
                var arrIDs = SubVariacao.Split(',');

                var selectTamanhos = (from tp in db.TAMANHO_PRODUTOs
                                      where arrID.Contains(tp.ID_TAMANHO.ToString())
                                      select tp).ToList();

                var selectSubVariacoes = (from sp in db.SUBVARIACAO_PRODUTOs
                                          where arrIDs.Contains(sp.ID_SUBVARIACAO.ToString())
                                          select sp).ToList();

                var selectEsfiha = (from e in db.ESFIHAs
                                    select e).ToList();

                foreach (var t in selectTamanhos)
                {
                    foreach (var s in selectSubVariacoes)
                    {
                        foreach (var e in selectEsfiha)
                        {

                            var existe = db.FORNECEDOR_PRODUTOs.Any(sp => sp.ID_FORNECEDOR == selecfornecedor.ID_UTILIZADOR && sp.TIPO == e.ID_TIPO_PRODUTO
                                && sp.VARIACAO == e.ID_VARIACAO && sp.SUBVARIACAO == s.ID_SUBVARIACAO && sp.TAMANHO == t.ID_TAMANHO && sp.NOME == e.NOME_ESFIHA);

                            if (existe == false)
                            {
                                ArrayId.Add(ArrayId.Count() + 1);

                                FORNECEDOR_PRODUTO fp = new FORNECEDOR_PRODUTO();

                                fp.ID_FORNECEDOR = selecfornecedor.ID_UTILIZADOR;
                                fp.TAMANHO = t.ID_TAMANHO;
                                fp.VALOR = 0;
                                fp.STATUS = false;
                                fp.VALOR_PROMOCAO = 0;
                                fp.TIPO = t.ID_TIPO_PRODUTO;
                                fp.QTD_ESTOQUE = 0;
                                fp.INGREDIENTES = null;
                                fp.PROMOCAO = false;
                                fp.SUBVARIACAO = s.ID_SUBVARIACAO;
                                fp.VARIACAO = e.ID_VARIACAO;
                                fp.NOME = e.NOME_ESFIHA;
                                fp.INGREDIENTES = e.INGREDIENTES;

                                db.FORNECEDOR_PRODUTOs.InsertOnSubmit(fp);
                            }
                        }
                    }
                }
                if (ArrayId.Count() == 0)
                {
                    retorno = "Já foram inseridos produtos do tipo " + Produto + " com os tamanhos selecionados e SubVariações selecionadas.";
                }
                else
                {
                    db.SubmitChanges();
                    retorno = "Sucesso";
                }
            }
            return retorno;
        }

        public System.Linq.IQueryable<string> GetProdutosFornecedor(int IdFornecedor, string searchstring, int TipoProduto)
        {
            IFSPDataContext db = new IFSPDataContext();

            if (TipoProduto != 0)
            {
                var suggestions = (from pf in db.FORNECEDOR_PRODUTOs
                                   where pf.ID_FORNECEDOR == int.Parse(IdFornecedor.ToString()) && pf.TIPO == TipoProduto
                                   select pf.NOME).Distinct();

                var namelist = suggestions.Where(n => n.ToLower().Contains(searchstring.ToLower()));


                return namelist;
            }
            else
            {
                var suggestions = (from pf in db.FORNECEDOR_PRODUTOs
                                   where pf.ID_FORNECEDOR == int.Parse(IdFornecedor.ToString())
                                   select pf.NOME).Distinct();

                var namelist = suggestions.Where(n => n.ToLower().Contains(searchstring.ToLower()));


                return namelist;
            }
        }

        public string BuscaNomeDelivery(int IdDelivery)
        {
            string retorno = "";
            IFSPDataContext db = new IFSPDataContext();

            retorno = db.FORNECEDOR_DELIVERies.Where(n => n.ID_FORNECEDOR_TRANSPORTE == IdDelivery).FirstOrDefault().NOME;

            return retorno;
        }

        public string BuscaNomeProdutos(string IdsProdutos, int Qtd)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            if (IdsProdutos.Contains('-'))
            {
                var Meia = IdsProdutos.Split('-');
                if (retorno == "")
                {
                    retorno = Qtd + "x&nbsp&nbsp1/2" + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Meia[0])).FirstOrDefault().NOME + " 1/2 " + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Meia[1])).FirstOrDefault().NOME;
                }
                else
                {
                    retorno = retorno + "|" + Qtd + "x&nbsp&nbsp1/2" + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Meia[0])).FirstOrDefault().NOME + " 1/2 " + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Meia[1])).FirstOrDefault().NOME;
                }
            }
            else
            {
                if (retorno == "")
                {
                    retorno = Qtd + "x&nbsp&nbsp" + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(IdsProdutos)).FirstOrDefault().NOME;
                }
                else
                {
                    retorno = retorno + "|" + Qtd + "x&nbsp&nbsp" + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(IdsProdutos)).FirstOrDefault().NOME;
                }
            }

            return retorno;
        }

        public string BuscaNomeProduto(string IdProduto)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            if (IdProduto.Contains('-'))
            {
                var Meia = IdProduto.Split('-');
                retorno = "1/2" + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Meia[0])).FirstOrDefault().NOME + " 1/2 " + db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(Meia[1])).FirstOrDefault().NOME;
            }
            else
            {

                retorno = db.FORNECEDOR_PRODUTOs.Where(fp => fp.ID_FORNECEDOR_PRODUTO == int.Parse(IdProduto)).FirstOrDefault().NOME;

            }

            return retorno;
        }

        public Domain.ClienteModel BuscaDadosCliente(int IdCliente)
        {
            Domain.ClienteModel Retorno = new Domain.ClienteModel();
            IFSPDataContext db = new IFSPDataContext();

            var cli = db.CLIENTEs.Where(c => c.ID_CLIENTE == IdCliente).FirstOrDefault();

            Retorno.Cep = cli.CEP;
            Retorno.Complemento = cli.COMPLEMENTO;
            Retorno.Cpf = cli.CPF;
            Retorno.Email = cli.EMAIL;
            Retorno.NomeCliente = cli.NOME_CLIENTE;
            Retorno.Numero = cli.NUMERO;
            Retorno.Referencia = cli.REFERENCIA;
            Retorno.Rua = cli.RUA;
            Retorno.Telefones = cli.TELEFONES;

            return Retorno;
        }

        public string AlterarStatusPedido(int Pedido, int IdStatus, int IdFornecedor)
        {
            string retorno = "";
            IFSPDataContext db = new IFSPDataContext();

            PEDIDO_FULL pf = new PEDIDO_FULL();

            pf = db.PEDIDO_FULLs.Where(p => p.NUMERO_PEDIDO == Pedido && p.ID_FORNECEDOR == IdFornecedor && p.STATUS != "1").FirstOrDefault();
            if (pf != null)
            {
                HISTORICO_PEDIDO hp = new HISTORICO_PEDIDO();
                hp.DATA_HORA = DateTime.Now;
                hp.STATUS = IdStatus.ToString();
                hp.NUMERO_PEDIDO = Pedido;

                pf.STATUS = IdStatus.ToString();

                db.HISTORICO_PEDIDOs.InsertOnSubmit(hp);

                db.SubmitChanges();

                retorno = "Alterado.";
            }
            else
            {
                retorno = "Pedido não encontrado.";
            }

            return retorno;
        }

        public Domain.PedidoModel BuscarPedido(string NumeroPedido)
        {
            IFSPDataContext db = new IFSPDataContext();
            MPRepository rp = new MPRepository();
            PEDIDO_FULL Busca = new PEDIDO_FULL();
            Domain.PedidoModel Retorno = new Domain.PedidoModel();

            Busca = db.PEDIDO_FULLs.Where(Armazena => Armazena.NUMERO_PEDIDO == int.Parse(NumeroPedido)).FirstOrDefault();

            Retorno.Data = (DateTime)Busca.DATA;
            Retorno.Hora = (TimeSpan)Busca.HORA;
            Retorno.Numero = Convert.ToInt32(Busca.NUMERO);
            Retorno.Status = Busca.STATUS;
            Retorno.NumeroPedido = (int)Busca.NUMERO_PEDIDO;
            Retorno.IdPedidoFull = Busca.ID_PEDIDO_FULL;

            return Retorno;

        }

        public List<Domain.ProdutoModel> BuscaTipoProdutoPorIngrediente(int IdFornecedor, string Ingrediente)
        {
            List<Domain.ProdutoModel> retorno = new List<Domain.ProdutoModel>();

            IFSPDataContext db = new IFSPDataContext();

            var Select = (from o in db.FORNECEDOR_PRODUTOs
                          where o.ID_FORNECEDOR == 1 && o.INGREDIENTES.Contains(Ingrediente)
                          select new
                          {
                              Tipo = o.TIPO,
                              NomeTipo = db.TIPO_PRODUTOs.Where(x => x.ID_TIPO_PRODUTO == o.TIPO).FirstOrDefault().NOME_TIPO_PRODUTO
                          }).Distinct().OrderBy(x => x.Tipo);

            //db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.INGREDIENTES.Contains(Ingrediente)).ToList();

            foreach (var i in Select)
            {
                Domain.ProdutoModel produto = new Domain.ProdutoModel();

                produto.Tipo = (int)i.Tipo;
                //utilizando o campo nome como nome Tipo Produto
                produto.Nome = i.NomeTipo;
                retorno.Add(produto);
            }

            return retorno;
        }

        public List<Domain.ProdutoModel> BuscaProdutoPorIngredienteTipo(int IdFornecedor, int Tipo, string Ingrediente, string Tamanho)
        {
            List<Domain.ProdutoModel> retorno = new List<Domain.ProdutoModel>();
            IFSPDataContext db = new IFSPDataContext();

            int Tm = 0;

            if (Tipo == 1)
            {
                var retTm = db.TAMANHO_PRODUTOs.Where(t => t.NOME_TAMANHO == Tamanho && t.ID_FORNECEDOR == 1 && t.ID_TIPO_PRODUTO == Tipo).FirstOrDefault();

                Tm = retTm.ID_TAMANHO;
            }


            if (string.IsNullOrWhiteSpace(Ingrediente))
            {
                if (Tm == 0)
                {
                    var Select = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == 1 && n.TIPO == Tipo && n.STATUS == true).ToList();

                    foreach (var i in Select)
                    {
                        Domain.ProdutoModel produto = new Domain.ProdutoModel();
                        produto.IdFornecedorProduto = i.ID_FORNECEDOR_PRODUTO;
                        produto.Tipo = (int)i.TIPO;
                        produto.Nome = i.NOME;
                        produto.Status = (bool)i.STATUS;
                        produto.Valor = (decimal)i.VALOR;
                        retorno.Add(produto);
                    }
                }
                else
                {
                    var Select = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == 1 && n.TIPO == Tipo && n.TAMANHO == Tm && n.STATUS == true).ToList();

                    foreach (var i in Select)
                    {
                        Domain.ProdutoModel produto = new Domain.ProdutoModel();
                        produto.IdFornecedorProduto = i.ID_FORNECEDOR_PRODUTO;
                        produto.Tipo = (int)i.TIPO;
                        produto.Nome = i.NOME;
                        produto.Status = (bool)i.STATUS;
                        produto.Valor = (decimal)i.VALOR;
                        retorno.Add(produto);
                    }
                }
            }
            else
            {
                if (Tm == 0)
                {
                    var Select = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == 1 && n.INGREDIENTES.Contains(Ingrediente) && n.TIPO == Tipo && n.STATUS == true).ToList();

                    foreach (var i in Select)
                    {
                        Domain.ProdutoModel produto = new Domain.ProdutoModel();
                        produto.IdFornecedorProduto = i.ID_FORNECEDOR_PRODUTO;
                        produto.Tipo = (int)i.TIPO;
                        produto.Nome = i.NOME;
                        produto.Status = (bool)i.STATUS;
                        produto.Valor = (decimal)i.VALOR;
                        retorno.Add(produto);
                    }
                }
                else
                {
                    var Select = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == 1 && n.INGREDIENTES.Contains(Ingrediente) && n.TIPO == Tipo && n.TAMANHO == Tm && n.STATUS == true).ToList();

                    foreach (var i in Select)
                    {
                        Domain.ProdutoModel produto = new Domain.ProdutoModel();
                        produto.IdFornecedorProduto = i.ID_FORNECEDOR_PRODUTO;
                        produto.Tipo = (int)i.TIPO;
                        produto.Nome = i.NOME;
                        produto.Status = (bool)i.STATUS;
                        produto.Valor = (decimal)i.VALOR;
                        retorno.Add(produto);
                    }
                }
            }
            return retorno;
        }

        public List<Domain.ProdutoModel> BuscaProdutoPorIngredienteTipoAll(int IdFornecedor, int Tipo, string Ingrediente)
        {
            List<Domain.ProdutoModel> retorno = new List<Domain.ProdutoModel>();

            IFSPDataContext db = new IFSPDataContext();
            if (string.IsNullOrWhiteSpace(Ingrediente))
            {
                var Select = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.TIPO == Tipo).ToList();

                foreach (var i in Select)
                {
                    Domain.ProdutoModel produto = new Domain.ProdutoModel();
                    produto.IdFornecedorProduto = i.ID_FORNECEDOR_PRODUTO;
                    produto.Tipo = (int)i.TIPO;
                    produto.Nome = i.NOME;
                    produto.Status = (bool)i.STATUS;
                    produto.Valor = (decimal)i.VALOR;
                    retorno.Add(produto);
                }
            }
            else
            {
                var Select = db.FORNECEDOR_PRODUTOs.Where(n => n.ID_FORNECEDOR == IdFornecedor && n.INGREDIENTES.Contains(Ingrediente) && n.TIPO == Tipo).ToList();

                foreach (var i in Select)
                {
                    Domain.ProdutoModel produto = new Domain.ProdutoModel();

                    produto.Tipo = (int)i.TIPO;
                    produto.Nome = i.NOME;
                    produto.Status = (bool)i.STATUS;
                    retorno.Add(produto);
                }
            }
            return retorno;
        }

        public string AlteraStatusProdutosPorIngrediente(int IdFornecedor, string Status, string Ingrediente)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            bool tStatus = false;

            if (Status == "Ativar")
            {
                tStatus = true;
            }

            var Select = db.FORNECEDOR_PRODUTOs.Where(n => n.INGREDIENTES.Contains(Ingrediente) && n.ID_FORNECEDOR == IdFornecedor).ToList();

            foreach (var i in Select)
            {
                i.STATUS = tStatus;

                db.SubmitChanges();
            }

            retorno = Select.Count + " Produtos foram alterados para o status:" + Status;


            return retorno;
        }

        public string BuscaClienteTelCep(string Numero, string Cep, string Telefone)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            if (!string.IsNullOrWhiteSpace(Cep))
            {
                var select = db.CLIENTEs.Where(c => c.CEP == Cep && c.NUMERO == Numero).FirstOrDefault();

                if (select != null)
                {
                    retorno = select.CEP + "|" + select.RUA + "|" + select.NUMERO + "|" + select.CPF + "|" + select.EMAIL + "|" + select.TELEFONES + "|" + select.NOME_CLIENTE + "|" + select.COMPLEMENTO + "|" + select.REFERENCIA;
                }
            }

            if (!string.IsNullOrWhiteSpace(Telefone))
            {
                var select = db.CLIENTEs.Where(t => t.TELEFONES.Contains(Telefone)).FirstOrDefault();

                if (select != null)
                {
                    retorno = select.CEP + "|" + select.RUA + "|" + select.NUMERO + "|" + select.CPF + "|" + select.EMAIL + "|" + select.TELEFONES + "|" + select.NOME_CLIENTE + "|" + select.COMPLEMENTO + "|" + select.REFERENCIA;
                }
            }

            return retorno;
        }

        public Domain.ClienteModel BuscaIdCliente(string Numero, string Cep)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            Domain.ClienteModel Cliente = new Domain.ClienteModel();

            if (!string.IsNullOrWhiteSpace(Cep))
            {
                var select = db.CLIENTEs.Where(c => c.CEP == Cep && c.NUMERO == Numero).FirstOrDefault();

                if (select != null)
                {
                    Cliente.Cep = select.CEP;
                    Cliente.Rua = select.RUA;
                    Cliente.Numero = select.NUMERO;
                    Cliente.Cpf = select.CPF;
                    Cliente.IdCliente = select.ID_CLIENTE;
                    Cliente.Email = select.EMAIL;
                    Cliente.Telefones = select.TELEFONES;
                    Cliente.NomeCliente = select.NOME_CLIENTE;
                    Cliente.Complemento = select.COMPLEMENTO;
                    Cliente.Referencia = select.REFERENCIA;
                }
            }

            return Cliente;
        }

        public string BuscaCEP(string Cep, string Numero)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.CEP_FULLs.Where(c => c.CEP == Cep).FirstOrDefault();

            if (select != null)
            {
                retorno = select.CEP + "|" + select.Rua + "|" + Numero + "||||";
            }

            return retorno;
        }

        public string CadastraCliente(string Rua, string Telefone, string Numero, string NomeCliente, string Cep, string Complemento, string CPF, string Referencia, string Email)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.CLIENTEs.Where(c => c.NUMERO == Numero && c.RUA == Rua && c.TELEFONES.Contains(Telefone)).FirstOrDefault();

            if (select == null)
            {
                CLIENTE c = new CLIENTE();

                c.NUMERO = Numero;
                c.NOME_CLIENTE = NomeCliente;
                c.REFERENCIA = Referencia;
                c.RUA = Rua;
                c.TELEFONES = Telefone;
                c.CEP = Cep;
                c.COMPLEMENTO = Complemento;
                c.CPF = CPF;
                c.EMAIL = Email;

                db.CLIENTEs.InsertOnSubmit(c);
                db.SubmitChanges();
            }

            retorno = "Cliente Cadastrado com sucesso.";

            return retorno;
        }

        public string SalvarPedidoManual(Domain.PedidoModel Pedido)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.PEDIDO_FULLs.Where(p => p.ID_FORNECEDOR == 1).OrderByDescending(pd => pd.ID_PEDIDO_FULL).First();

            PEDIDO_FULL pf = new PEDIDO_FULL();

            pf.NUMERO_PEDIDO = select.NUMERO_PEDIDO + 1;

            pf.ID_CLIENTE = Pedido.IdCliente;
            pf.CEP = Pedido.Cep;
            pf.DATA = Pedido.Data;
            pf.HORA = Pedido.Hora;
            pf.ID_DELIVERY = 1;
            pf.DESCONTO = Pedido.Desconto;
            pf.FORMA_PAGAMENTO = Pedido.FormaPagamento;
            pf.NUMERO = Pedido.Numero;
            pf.ID_FORNECEDOR = 1;
            pf.NOTAFISCAL = Pedido.NotaFiscal;
            pf.OBSERVACOES = Pedido.Observacoes;
            pf.RUA = Pedido.Rua;
            pf.STATUS = "0";
            pf.TROCO = double.Parse(Pedido.Troco);
            pf.VALOR_TOTAL = Decimal.Parse(Pedido.ValorTotal);

            db.PEDIDO_FULLs.InsertOnSubmit(pf);
            db.SubmitChanges();

            foreach (var PedidoItem in Pedido.ItensPedido)
            {
                ITENS_PEDIDO Item = new ITENS_PEDIDO();

                Item.ID_PRODUTO = PedidoItem.IdProduto;
                Item.PEDIDO_ID = pf.ID_PEDIDO_FULL;
                Item.QUANTIDADE = PedidoItem.Quantidade;
                Item.BORDA = PedidoItem.Borda;
                Item.OBSERVACOES = PedidoItem.Observacoes;

                db.ITENS_PEDIDOs.InsertOnSubmit(Item);
                db.SubmitChanges();
            }
            retorno = pf.NUMERO_PEDIDO.ToString();
            return retorno;
        }

        public string BuscaIDProdutosCarrinho(string Dados, int IdFornecedor)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var ArrayProdutos = Dados.Split('#');

            if (ArrayProdutos.Length > 1)
            {
                var select = db.FORNECEDOR_PRODUTOs.Where(p => p.ID_FORNECEDOR == IdFornecedor && p.NOME == "").FirstOrDefault();
            }
            else
            {
                var Data = ArrayProdutos[0].Split('^');

                var selectTamanho = db.TAMANHO_PRODUTOs.Where(t => t.ID_FORNECEDOR == IdFornecedor && t.NOME_TAMANHO == Data[4] && t.ID_TIPO_PRODUTO == int.Parse(Data[3])).FirstOrDefault();

                var select = db.FORNECEDOR_PRODUTOs.Where(p => p.ID_FORNECEDOR == IdFornecedor && p.NOME == Data[0] && p.TAMANHO == selectTamanho.ID_TAMANHO).FirstOrDefault();
            }

            return retorno;
        }

        public string BuscaFormaPagamento(int IdFornecedor)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.FORMA_PAGAMENTOs.Where(f => f.STATUS == true && f.ID_FORNECEDOR == 1).ToList();

            foreach (var fm in select)
            {
                if (retorno == "")
                {
                    retorno = fm.TIPO_PAGAMENTO;
                }
                else
                {
                    retorno = retorno + "|" + fm.TIPO_PAGAMENTO;
                }
            }

            return retorno;
        }

        public Domain.ClienteModel BuscaSenhaCliente(string Email)
        {
            Domain.ClienteModel Cliente = new Domain.ClienteModel();

            IFSPDataContext db = new IFSPDataContext();

            var select = db.CLIENTEs.Where(c => c.EMAIL == Email).FirstOrDefault();

            if (select != null)
            {
                Cliente.Senha = select.SENHA;
            }
            else
            {
                Cliente.Senha = "";
            }

            return Cliente;
        }

        public string PesquisaCEP(string Cep)
        {
            string retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.CEP_FULLs.Where(c => c.CEP == Cep).FirstOrDefault();

            if (select != null)
            {
                retorno = select.Cidade+", "+select.Rua;
            }

            return retorno;
        }

        public System.Linq.IQueryable<string> AutoCompleteCidade(string Cidade, string UF)
        {
            IFSPDataContext db = new IFSPDataContext();


            var suggestions = (from C in db.CEP_FULLs
                                where C.Estado == UF
                                select C.Cidade).Distinct();

            var namelist = suggestions.Where(n => n.ToLower().Contains(Cidade.ToLower()));

            return namelist;
            
        }

        public string PesquisaEndereco(string UF, string Cidade, string Bairro, string Rua)
        {
            string Retorno = "";

            IFSPDataContext db = new IFSPDataContext();

            var select = db.CEP_FULLs.Where(c => c.Estado == UF && c.Cidade == Cidade && c.Bairro == Bairro && c.Rua == Rua).FirstOrDefault();

            if (select != null)
            {
                Retorno = select.CEP;
            }
            else
            {
                Retorno = "Endereço não encontrado";
            }

            return Retorno;
        }

        public Domain.ClienteModel LoginCliente(string login, string senha)
        {
            Domain.ClienteModel cliente = new Domain.ClienteModel();
            IFSPDataContext db = new IFSPDataContext();

            var select = db.CLIENTEs.Where(cli => cli.EMAIL == login && cli.SENHA == senha).FirstOrDefault();

            if (select != null)
            {
                cliente.NomeCliente = select.NOME_CLIENTE;
                cliente.Numero = select.NUMERO;
                cliente.Rua = select.RUA;
                cliente.Email = select.EMAIL;
                cliente.Cep = select.CEP;
                cliente.Complemento = select.COMPLEMENTO;
                cliente.Cpf = select.CPF;
                cliente.Referencia= select.REFERENCIA;
                cliente.Pontos = (int)select.PONTOS;
                
            }
            else
            {
                cliente.NomeCliente = "";
            }

            return cliente;

        }
    }

}

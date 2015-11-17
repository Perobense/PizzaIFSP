using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PedidoModel
    {
        public int IdPedidoFull { get; set; }
        public int NumeroPedido { get; set; }
        public int IdFornecedor { get; set; }
        public int IdCliente { get; set; }
        public string FormaPagamento { get; set; } // 1 - Cartao de Credito 2- Cartao de Debito 3 - Dinheiro 4 - Vale Refeicao
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Status { get; set; } // 1 - Finalizado 2 - Cancelado  3 - Em Preparo 0 - Aguardando Atendimento
        public string Cep { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Troco { get; set; }
        public int IdDelivery { get; set; }
        public string ValorTotal { get; set; }
        public string Observacoes { get; set; }
        public bool NotaFiscal { get; set; }
        public double Desconto { get; set; }
        public List<Domain.ItemPedidoModel> ItensPedido { get; set; }
    }
}

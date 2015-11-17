using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PedidoExcelModel
    {
        public int NumeroPedido { get; set; }
        public string ValorTotal { get; set; }
        public string Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Status { get; set; }
        public string FormaPagamento { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Troco { get; set; }
        public string Observacoes { get; set; }
        public string NotaFiscal { get; set; }
        public double Desconto { get; set; }
        public string ItensPedido { get; set; }
       
    }
}

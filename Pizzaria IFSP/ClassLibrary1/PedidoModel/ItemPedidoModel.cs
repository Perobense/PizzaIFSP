using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ItemPedidoModel
    {
        public int IdItemPedido { get; set; }
        public int IdPedido { get; set; }
        public string IdProduto{ get; set; }
        public int Quantidade { get; set; }
        public string Observacoes{ get; set; }
        public string Borda { get; set; }
    }
}

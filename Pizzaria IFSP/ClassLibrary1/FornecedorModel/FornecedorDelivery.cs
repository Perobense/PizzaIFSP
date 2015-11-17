using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FornecedorDeliveryModel
    {
        public string IdFornecedorTransporte { get; set; }
        public string IdFornecedor { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TipoTransporte { get; set; }
        public string Identificacao { get; set; }
        public string Placa { get; set; }
    }
}

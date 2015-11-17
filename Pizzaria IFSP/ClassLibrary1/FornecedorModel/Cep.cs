using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FornecedorCepModel
    {
        public string IdCep { get; set; }
        public string Cep { get; set; }
        public string IdFornecedor { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Taxa { get; set; }
    }
}

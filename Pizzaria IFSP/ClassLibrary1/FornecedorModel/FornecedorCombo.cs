using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FornecedorComboModel
    {
        public int IdCombo { get; set; }
        public int IdFornecedor { get; set; }
        public string NomeCombo { get; set; }
        public string ValorCombo { get; set; }
        public bool Status { get; set; }
        public List<Domain.ProdutoModel> Produtos { get; set; }

    }
}

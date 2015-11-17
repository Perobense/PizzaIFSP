using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FornecedorModel
    {
        public int IdUtilizador { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Referencia { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Voucher { get; set; }
        public string NomeContato { get; set; }
        public bool VendaOnline { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DateTime DataUltimoLogin { get; set; }
        public bool UserLogado { get; set; }
        public string TempoEntrega { get; set; }
        public string TempoPreparo { get; set; }
        public string TaxaEntrega { get; set; }
        public string Diretorio { get; set; }
        public byte[] Foto { get; set; }
        public bool IngredientePadrao { get; set; }
        public bool TipoUtilizador { get; set; }
    }
}

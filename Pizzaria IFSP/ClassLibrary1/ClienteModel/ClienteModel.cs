using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Cpf { get; set; }
        public string Telefones { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Complemento { get; set; }
        public string Referencia { get; set; }
        public string Email { get; set; }
        public string Numero { get; set; }
        public string Senha { get; set; }
        public int Pontos { get; set; }
    }
}

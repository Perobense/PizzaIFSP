using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FornecedorHorarioModel
    {
        public string IdFornecedorHorario { get; set; }
        public string IdFornecedor { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
        public string Dia { get; set; }
    }
}

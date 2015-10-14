using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace PizzaIFSP.Models
{
    public class CadastoPagamento
    {
        [Key]
        public int pkPagamento { get; set; }

        [Required]
        [Display(Name = "Código do Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Código do Pedido")]
        public int IdPedido { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Forma de Pagamento")]
        public string FormaPgto { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Pagamento")]
        public DateTime DataPgto { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public int Valor { get; set; }
    }

    public class CadastroPagamentoContext : DbContext
    {
        public CadastroPagamentoContext()
            : base("name=CadatroPagamentoContext")
        {

            Database.Connection.ConnectionString =

                @"Data Source=LAB03-08;Initial Catalog=Pagamentos;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        }

        public DbSet<CadastoPagamento> Pagamentos { get; set; }
    }
}

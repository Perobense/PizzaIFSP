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
    public class CadastoPedido
    {
        [Key]
        public int pkPedido { get; set; }

        [Required]
        [Display(Name = "CodPedido")]
        public string CodPedido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de criação")]
        public DateTime Data { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "IdCliente")]
        public string IdCliente { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Total")]
        public float ValorTotal { get; set; }

    }


    public class CadastroPedidoContext : DbContext
    {
        public CadastroPedidoContext()
            : base("name=CadatroPedidoContext")
        {

            Database.Connection.ConnectionString =

                @"Data Source=LAB03-08;Initial Catalog=Pedidos;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        }

        public DbSet<CadastoPedido> Pedidos { get; set; }
    }
}

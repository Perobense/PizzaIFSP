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
    public class RelacaoProdutoPedido
    {
        [Key]
        public int pkProdutoPedido { get; set; }

        [Required]
        [Display(Name = "Codigo do Pedido")]
        public string codPedido { get; set; }

        [Required]
        [Display(Name = "Codigo do Produto")]
        public string codProduto { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public string Quantidade { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public float Valor { get; set; }
    }


    public class RelacaoProdutoPedidoContext : DbContext
    {
        public RelacaoProdutoPedidoContext()
            : base("name=RelacaoProdutoPedidoContext")
        {

            Database.Connection.ConnectionString =

                @"Data Source=LAB03-08;Initial Catalog=ProdutoPedido;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        }

        public DbSet<RelacaoProdutoPedido> ProdutoPedido { get; set; }
    }
}

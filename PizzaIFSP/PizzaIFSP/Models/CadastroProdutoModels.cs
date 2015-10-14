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
    public class CadastroProduto
    {
        [Key]
        public int pkProduto { get; set; }

        [Required]
        [Display(Name = "Código do Produto")]
        public int IdProduto { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }
    }

    public class CadastroProdutoContext : DbContext
    {
        public CadastroProdutoContext()
            : base("name=CadatroProdutoContext")
        {

            Database.Connection.ConnectionString =

                @"Data Source=LAB03-08;Initial Catalog=Produtos;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        }

        public DbSet<CadastroProduto> Produtos { get; set; }
    }
}

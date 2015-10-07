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
    public class CadastrarProduto
    {
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
}

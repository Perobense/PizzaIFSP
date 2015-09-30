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
    public class Pessoa
    {
        [Required]
        [Display(Name = "Código do Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nome completo")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public int Tel { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Complemento")]
        public string Comp { get; set; }

        [Required]
        [Display(Name = "CEP")]
        public int CEP { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha tem que ter mais de (6) caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }

    public class CadastroPessoaContext : DbContext
    {
        public CadastroPessoaContext()
            : base("name=CadatroPessoaContext")
        {

            Database.Connection.ConnectionString =

                @"Data Source=LAB03-08;Initial Catalog=Pessoas;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}

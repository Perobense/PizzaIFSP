using System;
using PizzaIFSP.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzaIFSP.Repository
{
    public class CadastroPessoaRepository
    {
        private CadastroPessoaContext context = new CadastroPessoaContext();

        public void Cadastra(CadastoPessoa pessoa)
        {
            context.Pessoas.Add(pessoa);
        }

        public void Salva()
        {
            context.SaveChanges();
        }

        public CadastoPessoa Consulta(int IdCliente)
        {
            return context.Pessoas.Find(IdCliente);
        }

        public void Exclui(int IdCliente)
        {
            CadastoPessoa pessoa = Consulta(IdCliente);
            context.Pessoas.Remove(pessoa);
        }
    }
}
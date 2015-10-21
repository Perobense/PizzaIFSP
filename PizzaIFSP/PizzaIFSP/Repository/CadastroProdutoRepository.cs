using System;
using PizzaIFSP.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzaIFSP.Repository
{
    public class CadastroProdutoRepository
    {
        private CadastroProdutoContext context = new CadastroProdutoContext();

        public void Cadastra(CadastroProduto produto)
        {
            context.Produtos.Add(produto);
        }

        public void Salva()
        {
            context.SaveChanges();
        }

        public CadastroProduto Consulta(int IdProduto)
        {
            return context.Produtos.Find(IdProduto);
        }

        public void Exclui(int IdProduto)
        {
            CadastroProduto produto = Consulta(IdProduto);
            context.Produtos.Remove(produto);
        }
    }
}
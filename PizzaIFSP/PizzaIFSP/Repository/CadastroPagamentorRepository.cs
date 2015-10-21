using System;
using PizzaIFSP.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzaIFSP.Repository
{
    public class CadastroPagamentoRepository
    {
        private CadastroPagamentoContext context = new CadastroPagamentoContext();

        public void Cadastra(CadastroPagamento Pagamento)
        {
            context.Pagamentos.Add(Pagamento);
        }

        public void Salva()
        {
            context.SaveChanges();
        }

        public CadastroPagamento Consulta(int IdPagamento)
        {
            return context.Pagamentos.Find(IdPagamento);
        }

        public void Exclui(int IdPagamento)
        {
            CadastroPagamento Pagamento = Consulta(IdPagamento);
            context.Pagamentos.Remove(Pagamento);
        }
    }
}
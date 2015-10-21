using System;
using PizzaIFSP.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzaIFSP.Repository
{
    public class CadastroPedidoRepository
    {
        private CadastroPedidoContext context = new CadastroPedidoContext();

        public void Cadastra(CadastoPedido pedido)
        {
            context.Pedidos.Add(pedido);
        }

        public void Salva()
        {
            context.SaveChanges();
        }

        public CadastoPedido Consulta(int IdPedido)
        {
            return context.Pedidos.Find(IdPedido);
        }

        public void Exclui(int IdPedido)
        {
            CadastoPedido Pedido = Consulta(IdPedido);
            context.Pedidos.Remove(Pedido);
        }
    }
}
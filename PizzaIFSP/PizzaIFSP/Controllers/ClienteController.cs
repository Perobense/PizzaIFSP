using PizzariaSys.Dominio;
using PizzariaSys.Dominio.Interfaces;
using PizzariaSys.Dominio.Negocios;
using PizzariaSys.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzariaSys.Web.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteNegocios _clienteNegocios = new ClienteNegocios();
        // GET: /Cliente/
        public ActionResult Index()
        {
            var clientes = _clienteNegocios.ListarTodos();
            return View(clientes);
        }

        [HttpGet]
        public ActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Inserir(ClientViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var cliente = new Cliente
            {
                Nome = model.Nome,
                Logradouro = model.Logradouro,
                Numero = model.Numero,
                Bairro = model.Bairro,
                Telefone = model.Telefone
            };

            _clienteNegocios.Salvar(cliente);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ConsultarTelefone()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConsultarTelefone(string telefone)
        {
            var cliente = _clienteNegocios.ListarClienteTelefone(telefone);

            if(cliente == null)
            {

                return View();
            }

            return View("Detalhes", new ClientViewModel(cliente));
        }

        [HttpGet]
        public ActionResult Detalhes(int id)
        {
            var cliente = _clienteNegocios.BuscarId(id);
            return View(new ClientViewModel(cliente));
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var cliente = _clienteNegocios.BuscarId(id);
            return View(new ClientViewModel(cliente));
        }

        [HttpPost]
        public ActionResult Editar(ClientViewModel model)
        {
            if(ModelState.IsValid)
            {
                return View(model);
            }

            var cliente = new Cliente
            {
                Id = model.Id,
                Nome = model.Nome,
                Logradouro = model.Logradouro,
                Numero = model.Numero,
                Bairro = model.Bairro,
                Telefone = model.Telefone

            };

            _clienteNegocios.Salvar(cliente);

            return RedirectToAction("Index");
        }
	}
}
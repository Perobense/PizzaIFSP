using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PizzaIFSP.Models;

namespace PizzaIFSP.Controllers
{
    public class CadastroPedidoController : ApiController
    {
        private PizzaIFSPContext db = new PizzaIFSPContext();

        // GET api/CadastroPedido
        public IEnumerable<CadastoPedido> GetCadastoPedidoes()
        {
            return db.CadastoPedidoes.AsEnumerable();
        }

        // GET api/CadastroPedido/5
        public CadastoPedido GetCadastoPedido(int id)
        {
            CadastoPedido cadastopedido = db.CadastoPedidoes.Find(id);
            if (cadastopedido == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return cadastopedido;
        }

        // PUT api/CadastroPedido/5
        public HttpResponseMessage PutCadastoPedido(int id, CadastoPedido cadastopedido)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != cadastopedido.pkPedido)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(cadastopedido).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/CadastroPedido
        public HttpResponseMessage PostCadastoPedido(CadastoPedido cadastopedido)
        {
            if (ModelState.IsValid)
            {
                db.CadastoPedidoes.Add(cadastopedido);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cadastopedido);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cadastopedido.pkPedido }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CadastroPedido/5
        public HttpResponseMessage DeleteCadastoPedido(int id)
        {
            CadastoPedido cadastopedido = db.CadastoPedidoes.Find(id);
            if (cadastopedido == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.CadastoPedidoes.Remove(cadastopedido);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cadastopedido);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
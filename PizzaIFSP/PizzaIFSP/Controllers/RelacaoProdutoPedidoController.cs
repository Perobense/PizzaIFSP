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
    public class RelacaoProdutoPedidoController : ApiController
    {
        private PizzaIFSPContext db = new PizzaIFSPContext();

        // GET api/RelacaoProdutoPedido
        public IEnumerable<RelacaoProdutoPedido> GetRelacaoProdutoPedidoes()
        {
            return db.RelacaoProdutoPedidoes.AsEnumerable();
        }

        // GET api/RelacaoProdutoPedido/5
        public RelacaoProdutoPedido GetRelacaoProdutoPedido(int id)
        {
            RelacaoProdutoPedido relacaoprodutopedido = db.RelacaoProdutoPedidoes.Find(id);
            if (relacaoprodutopedido == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return relacaoprodutopedido;
        }

        // PUT api/RelacaoProdutoPedido/5
        public HttpResponseMessage PutRelacaoProdutoPedido(int id, RelacaoProdutoPedido relacaoprodutopedido)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != relacaoprodutopedido.pkProdutoPedido)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(relacaoprodutopedido).State = EntityState.Modified;

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

        // POST api/RelacaoProdutoPedido
        public HttpResponseMessage PostRelacaoProdutoPedido(RelacaoProdutoPedido relacaoprodutopedido)
        {
            if (ModelState.IsValid)
            {
                db.RelacaoProdutoPedidoes.Add(relacaoprodutopedido);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, relacaoprodutopedido);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = relacaoprodutopedido.pkProdutoPedido }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/RelacaoProdutoPedido/5
        public HttpResponseMessage DeleteRelacaoProdutoPedido(int id)
        {
            RelacaoProdutoPedido relacaoprodutopedido = db.RelacaoProdutoPedidoes.Find(id);
            if (relacaoprodutopedido == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.RelacaoProdutoPedidoes.Remove(relacaoprodutopedido);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, relacaoprodutopedido);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
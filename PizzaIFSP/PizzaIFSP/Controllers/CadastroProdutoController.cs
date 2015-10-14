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
    public class CadastroProdutoController : ApiController
    {
        private PizzaIFSPContext db = new PizzaIFSPContext();

        // GET api/CadastroProduto
        public IEnumerable<CadastroProduto> GetCadastroProdutoes()
        {
            return db.CadastroProdutoes.AsEnumerable();
        }

        // GET api/CadastroProduto/5
        public CadastroProduto GetCadastroProduto(int id)
        {
            CadastroProduto cadastroproduto = db.CadastroProdutoes.Find(id);
            if (cadastroproduto == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return cadastroproduto;
        }

        // PUT api/CadastroProduto/5
        public HttpResponseMessage PutCadastroProduto(int id, CadastroProduto cadastroproduto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != cadastroproduto.pkProduto)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(cadastroproduto).State = EntityState.Modified;

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

        // POST api/CadastroProduto
        public HttpResponseMessage PostCadastroProduto(CadastroProduto cadastroproduto)
        {
            if (ModelState.IsValid)
            {
                db.CadastroProdutoes.Add(cadastroproduto);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cadastroproduto);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cadastroproduto.pkProduto }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CadastroProduto/5
        public HttpResponseMessage DeleteCadastroProduto(int id)
        {
            CadastroProduto cadastroproduto = db.CadastroProdutoes.Find(id);
            if (cadastroproduto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.CadastroProdutoes.Remove(cadastroproduto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cadastroproduto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
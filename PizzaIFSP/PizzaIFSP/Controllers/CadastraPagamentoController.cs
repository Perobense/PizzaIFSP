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
    public class CadastraPagamentoController : ApiController
    {
        private PizzaIFSPContext db = new PizzaIFSPContext();

        // GET api/CadastraPagamento
        public IEnumerable<CadastoPagamento> GetCadastoPagamentoes()
        {
            return db.CadastoPagamentoes.AsEnumerable();
        }

        // GET api/CadastraPagamento/5
        public CadastoPagamento GetCadastoPagamento(int id)
        {
            CadastoPagamento cadastopagamento = db.CadastoPagamentoes.Find(id);
            if (cadastopagamento == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return cadastopagamento;
        }

        // PUT api/CadastraPagamento/5
        public HttpResponseMessage PutCadastoPagamento(int id, CadastoPagamento cadastopagamento)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != cadastopagamento.pkPagamento)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(cadastopagamento).State = EntityState.Modified;

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

        // POST api/CadastraPagamento
        public HttpResponseMessage PostCadastoPagamento(CadastoPagamento cadastopagamento)
        {
            if (ModelState.IsValid)
            {
                db.CadastoPagamentoes.Add(cadastopagamento);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cadastopagamento);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cadastopagamento.pkPagamento }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CadastraPagamento/5
        public HttpResponseMessage DeleteCadastoPagamento(int id)
        {
            CadastoPagamento cadastopagamento = db.CadastoPagamentoes.Find(id);
            if (cadastopagamento == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.CadastoPagamentoes.Remove(cadastopagamento);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cadastopagamento);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
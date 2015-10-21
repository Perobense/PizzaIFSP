using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaIFSP.Models;

namespace PizzaIFSP.Controllers
{
    public class CadastroPessoaController : Controller
    {
        private PizzaIFSPContext db = new PizzaIFSPContext();

        //
        // GET: /CadastroPessoa/

        public ActionResult Index()
        {
            return View(db.CadastoPessoas.ToList());
        }


        //
        // GET: /CadastroPessoa/Details/5

        public ActionResult Details(int id = 0)
        {
            CadastoPessoa cadastopessoa = db.CadastoPessoas.Find(id);
            if (cadastopessoa == null)
            {
                return HttpNotFound();
            }
            return View(cadastopessoa);
        }

        //
        // GET: /CadastroPessoa/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CadastroPessoa/Create

        [HttpPost]
        public ActionResult Create(CadastoPessoa cadastopessoa)
        {
            if (ModelState.IsValid)
            {
                db.CadastoPessoas.Add(cadastopessoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cadastopessoa);
        }

        //
        // GET: /CadastroPessoa/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CadastoPessoa cadastopessoa = db.CadastoPessoas.Find(id);
            if (cadastopessoa == null)
            {
                return HttpNotFound();
            }
            return View(cadastopessoa);
        }

        //
        // POST: /CadastroPessoa/Edit/5

        [HttpPost]
        public ActionResult Edit(CadastoPessoa cadastopessoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cadastopessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cadastopessoa);
        }

        //
        // GET: /CadastroPessoa/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CadastoPessoa cadastopessoa = db.CadastoPessoas.Find(id);
            if (cadastopessoa == null)
            {
                return HttpNotFound();
            }
            return View(cadastopessoa);
        }

        //
        // POST: /CadastroPessoa/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CadastoPessoa cadastopessoa = db.CadastoPessoas.Find(id);
            db.CadastoPessoas.Remove(cadastopessoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
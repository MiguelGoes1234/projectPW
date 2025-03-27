using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CelularController : Controller
    {
        // GET: Celular
        public ActionResult Index()
        {
            Celular.GerarLista(Session);
            return View(Session["ListaCelular"] as List<Celular>);
        }
        public ActionResult Exibir(int id)
        {
            return View((Session["ListaCelular"] as List<Celular>).ElementAt(id));
            ////return View(Carro.GerarLista().ElementAt(id));
        }
        public ActionResult Edit(int id)
        {
            return View((Session["ListaCelular"] as List<Celular>).ElementAt(id));
            ////return View(Carro.GerarLista().ElementAt(id));
        }

        // POST: Caminhao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string numero = Convert.ToString(collection["Numero"]);
                string marca = Convert.ToString(collection["Marca"]);
                string novo = Convert.ToString(collection["Novo"]);

                var celular = (Session["ListaCelular"] as List<Celular>).ElementAt(id);
                celular.Numero = int.Parse(numero);
                celular.Marca = marca;
                celular.Novo = bool.Parse(novo);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View((Session["ListaCelular"] as List<Celular>).ElementAt(id));
        }
        // POST: Caminhao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var celulares = (Session["ListaCelular"] as List<Celular>);
                var celular = (Session["ListaCelular"] as List<Celular>).ElementAt(id);

                celulares.Remove(celular);
                Session.Remove("ListaCelular");
                Session.Add("ListaCelular", celulares);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View(new Celular());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Celular celular)
        {
            celular.Adicionar(Session);
            return RedirectToAction("Index");
        }

    }
}
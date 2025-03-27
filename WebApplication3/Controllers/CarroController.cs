using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using static System.Collections.Specialized.BitVector32;

namespace WebApplication3.Controllers
{
    public class CarroController : Controller
    {
        // GET: Carro
        public ActionResult Index()
        {
            Carro.GerarLista(Session);
            return View(Session["ListaCarro"] as List<Carro>);
        }
        public ActionResult Listar()
        {
            Carro.GerarLista(Session);
            return View(Session["ListaCarro"] as List<Carro>);
        }
        public ActionResult Exibir(int id)
        {
            return View((Session["ListaCarro"] as List<Carro>).ElementAt(id));
            ////return View(Carro.GerarLista().ElementAt(id));
        }
        public ActionResult Edit(int id)
        {
            return View((Session["ListaCarro"] as List<Carro>).ElementAt(id));
            ////return View(Carro.GerarLista().ElementAt(id));
        }

        // POST: Caminhao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string placa = Convert.ToString(collection["Placa"]);
                string ano = Convert.ToString(collection["Ano"]);
                string cor = Convert.ToString(collection["Cor"]);

                var carro = (Session["ListaCarro"] as List<Carro>).ElementAt(id);
                carro.Placa = placa;
                carro.Ano = int.Parse(ano);
                carro.Cor = cor;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View((Session["ListaCarro"] as List<Carro>).ElementAt(id));
        }
        // POST: Caminhao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var carros = (Session["ListaCarro"] as List<Carro>);
                var carro = (Session["ListaCarro"] as List<Carro>).ElementAt(id);

                carros.Remove(carro);
                Session.Remove("ListaCarro");
                Session.Add("ListaCarro", carros);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View(new Carro());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carro carro)
        {
            carro.Adicionar(Session);
            return RedirectToAction("Index");
        }

    }
}
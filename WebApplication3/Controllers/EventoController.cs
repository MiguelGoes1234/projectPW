using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class EventoController : Controller
    {
        public ActionResult Index()
        {
            Evento.GerarLista(Session);
            return View(Session["ListaEvento"] as List<Evento>);
        }
        public ActionResult Exibir(int id)
        {
            return View((Session["ListaEvento"] as List<Evento>).ElementAt(id));
            ////return View(Carro.GerarLista().ElementAt(id));
        }
        public ActionResult Edit(int id)
        {
            return View((Session["ListaEvento"] as List<Evento>).ElementAt(id));
            ////return View(Carro.GerarLista().ElementAt(id));
        }

        // POST: Caminhao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string local = Convert.ToString(collection["Local"]);
                string data = Convert.ToString(collection["Data"]);

                var evento = (Session["ListaEvento"] as List<Evento>).ElementAt(id);
                evento.Local = local;
                evento.Data = DateTime.Parse(data);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View((Session["ListaEvento"] as List<Evento>).ElementAt(id));
        }
        // POST: Caminhao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var eventos = (Session["ListaEvento"] as List<Evento>);
                var evento = (Session["ListaEvento"] as List<Evento>).ElementAt(id);

                eventos.Remove(evento);
                Session.Remove("ListaEvento");
                Session.Add("ListaEvento", eventos);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View(new Evento());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evento evento)
        {
            evento.Adicionar(Session);
            return RedirectToAction("Index");
        }
    }
}
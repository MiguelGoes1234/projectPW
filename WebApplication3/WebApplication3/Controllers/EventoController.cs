﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Listar()
        {
            return View(Evento.GerarLista());
        }
        public ActionResult Exibir(int id)
        {
            return View(Evento.GerarLista().ElementAt(id));
        }
    }
}
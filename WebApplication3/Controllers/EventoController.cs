using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public ActionResult CreatePdf()
        {
            // 1. Recuperar a lista de eventos da sessão
            var listaEventos = Session["ListaEvento"] as List<Evento>;
            if (listaEventos == null || listaEventos.Count == 0)
            {
                return Content("Nenhum eventos disponível para gerar o PDF.");
            }

            // 2. Gerar o conteúdo HTML com base na lista
            StringBuilder html = new StringBuilder();
            html.Append("<html><body>");
            html.Append("<h1>Lista de Eventos</h1>");
            html.Append("<table border='1' cellpadding='5' cellspacing='0'>");
            html.Append("<tr><th>Local</th><th>Data</th></tr>");

            foreach (var evento in listaEventos)
            {
                html.Append("<tr>");
                html.Append($"<td>{evento.Local}</td>");
                html.Append($"<td>{evento.Data.ToShortDateString()}</td>");
                html.Append("</tr>");
            }

            html.Append("</table>");
            html.Append("</body></html>");

            // 3. Converter o HTML para PDF e retornar como arquivo
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                using (var sr = new StringReader(html.ToString()))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                }

                document.Close();

                byte[] pdfBytes = ms.ToArray();
                return File(pdfBytes, "application/pdf", "ListaEventos.pdf");
            }
        }
    }
}
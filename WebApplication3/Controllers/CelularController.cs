using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
                string data = Convert.ToString(collection["Data"]);

                var celular = (Session["ListaCelular"] as List<Celular>).ElementAt(id);
                celular.Numero = int.Parse(numero);
                celular.Marca = marca;
                celular.Novo = bool.Parse(novo);
                celular.Data = DateTime.Parse(data);

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

        public ActionResult CreatePdf()
        {
            // 1. Recuperar a lista de celulares da sessão
            var listaCelulares = Session["ListaCelular"] as List<Celular>;
            if (listaCelulares == null || listaCelulares.Count == 0)
            {
                return Content("Nenhum celular disponível para gerar o PDF.");
            }

            // 2. Gerar o conteúdo HTML com base na lista
            StringBuilder html = new StringBuilder();
            html.Append("<html><body>");
            html.Append("<h1>Lista de celulares</h1>");
            html.Append("<table border='1' cellpadding='5' cellspacing='0'>");
            html.Append("<tr><th>Id</th><th>Marca</th><th>Novo</th><th>Numero</th><th>Data</th></tr>");

            foreach (var celular in listaCelulares)
            {
                html.Append("<tr>");
                html.Append($"<td>{celular.Id}</td>");
                html.Append($"<td>{celular.Marca}</td>");
                html.Append($"<td>{celular.Novo.ToString()}</td>");
                html.Append($"<td>{celular.Numero}</td>");
                html.Append($"<td>{celular.Data.ToShortDateString()}</td>");
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
                return File(pdfBytes, "application/pdf", "ListaCelulares.pdf");
            }
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using System.Xml.Linq;
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
                string data = Convert.ToString(collection["Data"]);

                var carro = (Session["ListaCarro"] as List<Carro>).ElementAt(id);
                carro.Placa = placa;
                carro.Ano = int.Parse(ano);
                carro.Cor = cor;
                carro.Data = DateTime.Parse(data);

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

        public ActionResult CreatePdf()
        {
            // 1. Recuperar a lista de carros da sessão
            var listaCarros = Session["ListaCarro"] as List<Carro>;
            if (listaCarros == null || listaCarros.Count == 0)
            {
                return Content("Nenhum carro disponível para gerar o PDF.");
            }

            // 2. Gerar o conteúdo HTML com base na lista
            StringBuilder html = new StringBuilder();
            html.Append("<html><body>");
            html.Append("<h1>Lista de Carros</h1>");
            html.Append("<table border='1' cellpadding='5' cellspacing='0'>");
            html.Append("<tr><th>Placa</th><th>Ano</th><th>Cor</th><th>Data</th></tr>");

            foreach (var carro in listaCarros)
            {
                html.Append("<tr>");
                html.Append($"<td>{carro.Placa}</td>");
                html.Append($"<td>{carro.Ano}</td>");
                html.Append($"<td>{carro.Cor}</td>");
                html.Append($"<td>{carro.Data.ToShortDateString()}</td>");
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
                return File(pdfBytes, "application/pdf", "ListaCarros.pdf");
            }
        }
    }
}
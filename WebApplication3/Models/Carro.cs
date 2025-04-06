using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int Ano {  get; set; }

        public string Cor { get; set; }

        public string Car {  get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        public static void GerarLista(HttpSessionStateBase session)
        {

            if (session["ListaCarro"] != null)
            {
                if (((List<Carro>)session["ListaCarro"]).Count > 0)
                {
                    return;
                }
            }

            var lista = new List<Carro>();
            lista.Add(new Carro {Id = 1, Placa = "32145124", Ano = 2020, Cor = "Rosa", Data = DateTime.Now });
            lista.Add(new Carro {Id = 2, Placa = "23fs65", Ano = 2019, Cor = "Preto", Data = DateTime.Now });
            lista.Add(new Carro {Id = 3, Placa = "3dfv21", Ano = 2022, Cor = "Branco", Data = DateTime.Now });

            session.Remove("ListaCarro");
            session.Add("ListaCarro", lista);
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                (session["ListaCarro"] as List<Carro>).Add(this);
            }
        }
    }
}
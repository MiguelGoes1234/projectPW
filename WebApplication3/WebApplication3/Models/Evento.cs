using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Evento
    {
        public string Local { get; set; }
        public DateTime Data { get; set; }

        public static List<Evento> GerarLista()
        {
            var lista = new List<Evento>();
            lista.Add(new Evento { Local = "Casa do Miguel", Data = DateTime.Now });
            lista.Add(new Evento { Local = "Parque das aguas", Data = DateTime.Now });
            lista.Add(new Evento { Local = "Aniversario do Miguel", Data = DateTime.Now });

            return lista;

        }
    }
}
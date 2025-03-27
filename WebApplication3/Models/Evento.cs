using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }

        public static void GerarLista(HttpSessionStateBase session)
        {

            if (session["ListaEvento"] != null)
            {
                if (((List<Evento>)session["ListaEvento"]).Count > 0)
                {
                    return;
                }
            }

            var lista = new List<Evento>();
            lista.Add(new Evento {Id = 1, Local = "Casa do Miguel", Data = DateTime.Now });
            lista.Add(new Evento { Id = 2, Local = "Parque das aguas", Data = DateTime.Now });
            lista.Add(new Evento { Id = 3, Local = "Aniversario do Miguel", Data = DateTime.Now });

            session.Remove("ListaEvento");
            session.Add("ListaEvento", lista);
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaEvento"] != null)
            {
                (session["ListaEvento"] as List<Evento>).Add(this);
            }
        }
    }
}
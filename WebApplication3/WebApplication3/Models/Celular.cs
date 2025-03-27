using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Celular
    {
        public int Numero { get; set; }
        public string Marca { get; set; }

        public bool Novo { get; set; }


        public static List<Celular> GerarLista()
        {
            var lista = new List<Celular>();
            lista.Add(new Celular { Numero = 12, Marca = "Samsung", Novo = true });
            lista.Add(new Celular { Numero = 13, Marca = "Lg", Novo = false });
            lista.Add(new Celular { Numero = 10, Marca = "Apple", Novo = true });

            return lista;

        }

    }
}
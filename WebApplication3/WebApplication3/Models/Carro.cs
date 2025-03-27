using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Carro
    {
        public string Placa { get; set; }
        public int Ano {  get; set; }

        public string Cor { get; set; }

        public string Car {  get; set; }

        public static List<Carro> GerarLista()
        {
            var lista = new List<Carro>();
            lista.Add(new Carro { Placa = "32145124", Ano = 2020, Cor = "Rosa" });
            lista.Add(new Carro { Placa = "23fs65", Ano = 2019, Cor = "Preto" });
            lista.Add(new Carro { Placa = "3dfv21", Ano = 2022, Cor = "Branco" });

            return lista;

        }
    }
}
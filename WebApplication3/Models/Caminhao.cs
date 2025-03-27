using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Caminhao
    {
        public string Placa { get; set; }
        public int Ano {  get; set; }

        public string Cor { get; set; }

        public string Car {  get; set; }

        public static List<Caminhao> GerarLista()
        {
            var lista = new List<Caminhao>();
            lista.Add(new Caminhao { Placa = "32145124", Ano = 2020, Cor = "Rosa" });
            lista.Add(new Caminhao { Placa = "23fs65", Ano = 2019, Cor = "Preto" });
            lista.Add(new Caminhao { Placa = "3dfv21", Ano = 2022, Cor = "Branco" });

            return lista;

        }
    }
}
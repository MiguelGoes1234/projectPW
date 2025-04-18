﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Celular
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Marca { get; set; }
        public bool Novo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [Display(Name = "Data de lançamento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        public static void GerarLista(HttpSessionStateBase session)
        {

            if (session["ListaCelular"] != null)
            {
                if (((List<Celular>)session["ListaCelular"]).Count > 0)
                {
                    return;
                }
            }

            var lista = new List<Celular>();
            lista.Add(new Celular {Id = 1, Numero = 12, Marca = "Samsung", Novo = true, Data = DateTime.Now});
            lista.Add(new Celular {Id = 2, Numero = 13, Marca = "Lg", Novo = false, Data = DateTime.Now});
            lista.Add(new Celular {Id = 3, Numero = 10, Marca = "Apple", Novo = true, Data = DateTime.Now});

            session.Remove("ListaCelular");
            session.Add("ListaCelular", lista);
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaCelular"] != null)
            {
                (session["ListaCelular"] as List<Celular>).Add(this);
            }
        }

    }
}
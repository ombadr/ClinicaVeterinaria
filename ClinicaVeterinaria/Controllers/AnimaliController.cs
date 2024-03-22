using ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Veterinario")]
    public class AnimaliController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index([Bind(Include = "Nome,Tipologia,ColoreMantello,DataNascita,Microchip,NumeroMicrochip,NomeProprietario,CognomeProprietario")] Animali animali)
        {

            var animale = new Animali()
            {
                Nome = animali.Nome,
                Tipologia = animali.Tipologia,
                ColoreMantello = animali.ColoreMantello,
                DataNascita = animali.DataNascita,
                Microchip = animali.Microchip,
                NumeroMicrochip = animali.NumeroMicrochip,
                NomeProprietario = animali.NomeProprietario,
                CognomeProprietario = animali.CognomeProprietario
            };
            db.Animali.Add(animale);
            db.SaveChanges();
            TempData["ConfermaAnimale"] = true;
            return RedirectToAction("Lista");
        }
       
        public ActionResult Dettagli(int ID)
        {
            Animali animale = db.Animali.Where(x => x.ID == ID).FirstOrDefault();
            ViewBag.Animale = animale;
            return View();
        }
        public ActionResult Lista()
        {
            List<Animali> lista = db.Animali.ToList();
            ViewBag.Animali = lista;
            return View();
        }
        [HttpPost]
        public ActionResult Modifica([Bind(Include = "ID,Nome,Tipologia,ColoreMantello,DataNascita,Microchip,NumeroMicrochip,NomeProprietario,CognomeProprietario")] Animali animali)
        {
            var animale = db.Animali.Where(x => x.ID == animali.ID).FirstOrDefault();
            if (animale != null)
            {
                animale.Nome = animali.Nome;
                animale.Tipologia = animali.Tipologia;
                animale.ColoreMantello = animali.ColoreMantello;
                if (animali.DataNascita != null)
                {
                    animale.DataNascita = animali.DataNascita;
                }
                if (animali.Microchip != null)
                {
                    animale.Microchip = animali.Microchip;
                }
                animale.NumeroMicrochip = animali.NumeroMicrochip;
                animale.NomeProprietario = animali.NomeProprietario;
                animale.CognomeProprietario = animali.CognomeProprietario;
                db.SaveChanges();
            }


            return RedirectToAction("Lista");
        }
        [HttpPost]
        public ActionResult Elimina(int ID)
        {
            var animali = db.Animali.Where(x => x.ID == ID).FirstOrDefault();
            db.Animali.Remove(animali);
            db.SaveChanges();
            return RedirectToAction("Lista");

        }

    }
}
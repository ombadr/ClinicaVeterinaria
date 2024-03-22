using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Veterinario")]
    public class VisiteController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Visite
        public ActionResult Index()
        {
            var visiteList = db.Visite.ToList();

            return View(visiteList);
        }

        public ActionResult Registra()

        {
            ViewBag.AnimaliDropDown = db.Animali.Select(a => new SelectListItem
            {
                Text = a.Nome,
                Value = a.ID.ToString(),
            }).ToList();
            List<Animali> animaliList = db.Animali.ToList();
            ViewBag.Animali = animaliList;
            return View();
        }

        public ActionResult GetVisiteByAnimaleId(int animaleId)
        {
            var visite = db.Visite
                .Where(v => v.IDAnimale == animaleId)
                .OrderByDescending(v => v.DataVisita)
                .ToList();

            return Json(visite, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registra([Bind(Include = "IDAnimale,DataVisita,EsameObiettivo,CuraPrescritta")]  Visite visita)
        {

            if (ModelState.IsValid)
            {
                var animale = db.Animali.Find(visita.IDAnimale);
                if (animale != null)
                {
                    try
                    {
                        db.Visite.Add(visita);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError(string.Empty, "Si è verificato un errore durante l'aggiunta della visita.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "L'animale specificato non esiste.");
                }
            }

            ViewBag.Animali = new SelectList(db.Animali.ToList(), "ID", "Nome", visita.IDAnimale);
            return View(visita);
        }

    }
}
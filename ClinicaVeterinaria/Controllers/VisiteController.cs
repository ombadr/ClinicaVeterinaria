using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Farmacista")]
    public class VisiteController : Controller
    {
        // Dichiarazione del contesto del database all'esterno del metodo
        private readonly DBContext db = new DBContext();

        // GET: Visite
        public ActionResult Index()
        {
            // Recupera la lista delle visite veterinarie dal database
            var visiteList = db.Visite.ToList();

            // Assicurati che la vista sia correttamente configurata per mostrare le visite
            return View(visiteList);
        }

        public ActionResult Aggiungi()
        {
            ViewBag.AnimaliList = new SelectList(db.Animali.ToList(), "ID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Aggiungi(Visite visite)
        {
            if (ModelState.IsValid)
            {
                db.Visite.Add(visite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimaliList = new SelectList(db.Animali.ToList(), "ID", "Nome");
            return View(visite);
        }


    }
}
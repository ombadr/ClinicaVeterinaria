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

        public ActionResult Registra()
        
        {
            List<Animali> animaliList = db.Animali.ToList();
            ViewBag.Animali = animaliList;
            return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AggiungiVisita(Visite visita)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //visita.IDAnimale = 1;
                    // Verifica se esiste un animale con l'ID specificato
                    //var animale = db.Animali.Find(visita.IDAnimale);
                    //if (animale == null)
                    //{
                    //    // Se l'animale non esiste, aggiungi un errore al modello e torna alla vista con il modello
                    //    ModelState.AddModelError(string.Empty, "L'animale specificato non esiste.");
                    //    ViewBag.Animali = new SelectList(db.Animali, "ID", "Nome");
                    //    return View(visita);
                    //}

                    // Aggiungi la visita al database
                    db.Visite.Add(visita);
                    db.SaveChanges();

                    // Reindirizza all'azione Index per visualizzare l'elenco aggiornato delle visite
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Gestisci l'eccezione qui
                    // Puoi loggare l'errore, mostrare un messaggio all'utente, ecc.
                    ModelState.AddModelError(string.Empty, "Si è verificato un errore durante l'aggiunta della visita.");
                    ViewBag.Animali = new SelectList(db.Animali, "ID", "Nome");
                    return View(visita);
                }
            }

            // Se il modello non è valido, torna alla vista con il modello e gli errori di validazione
            ViewBag.Animali = new SelectList(db.Animali, "ID", "Nome");
            return View(visita);
        }


    }
}
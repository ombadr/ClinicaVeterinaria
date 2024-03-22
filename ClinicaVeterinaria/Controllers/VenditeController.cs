using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class VenditeController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            List<Vendite> vendite = db.Vendite.ToList();
            return View(vendite);
        }

        public ActionResult Create()
        {
            ViewBag.Prodotti = db.Prodotti.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodiceFiscaleCliente,IDProdotto,NumeroRicettaMedica")] Vendite vendita)
        {
            if (ModelState.IsValid)
            {
                vendita.DataVendita = DateTime.Now;

                var prodotto = db.Prodotti.Find(vendita.IDProdotto);
                if (prodotto == null)
                {
                    ModelState.AddModelError("IDProdotto", "Prodotto non valido");
                    ViewBag.Prodotti = db.Prodotti.ToList();
                    return View(vendita);
                }

                db.Vendite.Add(vendita);
                db.SaveChanges();
                return RedirectToAction("Index", "Farmacia");
            }
            ViewBag.Prodotti = db.Prodotti.ToList();
            return View(vendita);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

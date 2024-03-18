using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class RicoveriController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            var ricoveri = db.Ricoveri.ToList();
            return View(ricoveri);
        }

        public ActionResult Details(int id)
        {
            var ricovero = db.Ricoveri.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            return View(ricovero);
        }

        public ActionResult Aggiungi()
        {
            ViewBag.AnimaliList = new SelectList(db.Animali.ToList(), "ID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Aggiungi(Ricoveri ricovero)
        {
            if (ModelState.IsValid)
            {
                db.Ricoveri.Add(ricovero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimaliList = new SelectList(db.Animali.ToList(), "ID", "Nome");
            return View(ricovero);
        }


        public ActionResult Modifica(int id)
        {
            var ricovero = db.Ricoveri.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }

            ViewBag.AnimaliList = new SelectList(db.Animali.ToList(), "ID", "Nome", ricovero.IDAnimale);
            return View(ricovero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modifica(Ricoveri ricovero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ricovero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimaliList = new SelectList(db.Animali.ToList(), "ID", "Nome", ricovero.IDAnimale);
            return View(ricovero);
        }


        public ActionResult Elimina(int id)
        {
            var ricovero = db.Ricoveri.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            return View(ricovero);
        }

        [HttpPost, ActionName("Elimina")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminaConfermato(int id)
        {
            var ricovero = db.Ricoveri.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            db.Ricoveri.Remove(ricovero);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

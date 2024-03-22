using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;
using System.IO;
using System.Web;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Veterinario")]
    public class RicoveriController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            var ricoveri = db.Ricoveri.Include(r => r.Animali).ToList();
            return View(ricoveri);
        }

        public ActionResult Details(int id)
        {
            var ricovero = db.Ricoveri.Include(r => r.Animali).SingleOrDefault(r => r.ID == id);
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
        public ActionResult Aggiungi([Bind(Include = "ID,IDAnimale,DataInizio,DataFine,Descrizione,Foto")] Ricoveri ricovero, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if(Foto != null && Foto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Foto.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/uploads"), fileName);
                    Foto.SaveAs(path);

                    ricovero.Foto = Path.Combine("~/Assets/uploads", fileName);
                }
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
        public ActionResult Modifica([Bind(Include = "ID,IDAnimale,DataInizio,DataFine,Descrizione,Foto")] Ricoveri ricovero, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if(Foto != null && Foto.ContentLength > 0)
                {
                    if(!string.IsNullOrEmpty(ricovero.Foto))
                    {
                        var oldFilePath = Server.MapPath(ricovero.Foto);
                        if(System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    var fileName = Path.GetFileName(Foto.FileName);
                    var path = Path.Combine(Server.MapPath("~/Assets/uploads"), fileName);
                    Foto.SaveAs(path);

                    ricovero.Foto = $"~/Assets/uploads/{fileName}";
                }
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

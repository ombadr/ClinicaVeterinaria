using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class ProdottiController : Controller
    {
        private readonly DBContext _context;

        public ProdottiController()
        {
            _context = new DBContext(); 
        }

        // GET: Prodotti
        public ActionResult Index()
        {
            var prodotti = _context.Prodotti.ToList();
            return View(prodotti);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Prodotti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotto = _context.Prodotti.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        // POST: Prodotti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,DittaFornitrice,UsoPossibile")] Prodotti prodotto)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(prodotto).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prodotto);
        }
        // GET: Prodotti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotto = _context.Prodotti.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }
        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotto = _context.Prodotti.Find(id);
            _context.Prodotti.Remove(prodotto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Prodotti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prodotti/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,DittaFornitrice,UsoPossibile")] Prodotti prodotto)
        {
            if (ModelState.IsValid)
            {
                _context.Prodotti.Add(prodotto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prodotto);
        }




    }



}

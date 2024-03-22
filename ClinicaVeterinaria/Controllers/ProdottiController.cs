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
            var ProdottiConDisposizioni = _context.Prodotti.Include(p => p.DisposizioneMedicinali).ToList();
            return View(ProdottiConDisposizioni);
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
            Prodotti prodotto = _context.Prodotti.Include(p => p.DisposizioneMedicinali).SingleOrDefault(p => p.ID == id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }

            if (prodotto.DisposizioneMedicinali.Any())
            {
                ViewBag.CodiceArmadietto = prodotto.DisposizioneMedicinali.First().CodiceArmadietto;
                ViewBag.CodiceCassetto = prodotto.DisposizioneMedicinali.First().CodiceCassetto;
            }

            return View(prodotto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,DittaFornitrice,UsoPossibile")] Prodotti prodotto, int? CodiceArmadietto, int? CodiceCassetto)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(prodotto).State = EntityState.Modified;

                
                var disposizione = _context.DisposizioneMedicinali.FirstOrDefault(d => d.IDProdotto == prodotto.ID);
                if (disposizione != null)
                {
                    
                    if (CodiceArmadietto.HasValue && CodiceCassetto.HasValue)
                    {
                        disposizione.CodiceArmadietto = CodiceArmadietto.Value;
                        disposizione.CodiceCassetto = CodiceCassetto.Value;
                    }
                }
                else if (CodiceArmadietto.HasValue && CodiceCassetto.HasValue)
                {
                    
                    _context.DisposizioneMedicinali.Add(new DisposizioneMedicinali
                    {
                        IDProdotto = prodotto.ID,
                        CodiceArmadietto = CodiceArmadietto.Value,
                        CodiceCassetto = CodiceCassetto.Value
                    });
                }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotto = _context.Prodotti.Find(id);
            if (prodotto != null)
            {
                var disposizioni = _context.DisposizioneMedicinali.Where(d => d.IDProdotto == id).ToList();
                foreach (var disposizione in disposizioni)
                {
                    _context.DisposizioneMedicinali.Remove(disposizione);
                }

                _context.Prodotti.Remove(prodotto);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Prodotto rimosso con successo!";
                return RedirectToAction("Index");

            }

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
        public ActionResult Create([Bind(Include = "Nome,DittaFornitrice,UsoPossibile")] Prodotti prodotto, int? codiceArmadietto, int? codiceCassetto)
        {
            if (ModelState.IsValid)
            {
                
                _context.Prodotti.Add(prodotto);
                _context.SaveChanges(); 

                
                if (codiceArmadietto.HasValue && codiceCassetto.HasValue)
                {
                    
                    var disposizione = new DisposizioneMedicinali
                    {
                        IDProdotto = prodotto.ID,
                        CodiceArmadietto = codiceArmadietto.Value,
                        CodiceCassetto = codiceCassetto.Value
                    };
                    _context.DisposizioneMedicinali.Add(disposizione);
                    _context.SaveChanges(); 
                }

                TempData["SuccessMessage"] = "Prodotto creato con successo!"; 

                return RedirectToAction("Index"); 
            }

            return View(prodotto);
        }
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var risultatiRicerca = _context.Prodotti
                                            .Include(p => p.DisposizioneMedicinali)
                                            .Where(p => p.Nome.Contains(searchString) ||
                                                        p.DittaFornitrice.Contains(searchString) ||
                                                        p.DisposizioneMedicinali.Any(d => d.CodiceArmadietto.ToString().Contains(searchString)))
                                            .ToList();

            return View("Index", risultatiRicerca);
        }

    }
}

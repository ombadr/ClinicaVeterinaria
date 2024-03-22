using ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Farmacista")]
    public class MedicinaliVendutiController : Controller
    {
        DBContext db = new DBContext();
        // GET: MedicinaliVenduti
        public ActionResult VenditePerData()
        {
            return View();
        }
     
      
        public JsonResult Sellfordata(DateTime searchdata)
        {
            List<Vendite> vendite = db.Vendite.Where(x => DbFunctions.TruncateTime(x.DataVendita) == DbFunctions.TruncateTime(searchdata)).ToList();
            List<Prodotti> prodotti = new List<Prodotti>();

            foreach (Vendite v in vendite)
            {
                var a = db.Prodotti.FirstOrDefault(x => x.ID == v.IDProdotto);
                if (a != null)
                {
                    Prodotti prodotto = new Prodotti()
                    {
                        Nome = a.Nome,
                        DittaFornitrice = a.DittaFornitrice,
                        UsoPossibile = a.UsoPossibile
                    };
                    prodotti.Add(prodotto);
                }
            }
            return Json(prodotti, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SellforCf(string cf)
        {
            List<Vendite> vendite = db.Vendite.Where(x => x.CodiceFiscaleCliente == cf).ToList();
            List<Prodotti> prodotti = new List<Prodotti>();
            foreach (Vendite v in vendite)
            {
                var a = db.Prodotti.Where(x => x.ID == v.ID).FirstOrDefault();
                Prodotti prodotto = new Prodotti()
                {
                    Nome = a.Nome,
                    DittaFornitrice = a.DittaFornitrice,
                    UsoPossibile = a.UsoPossibile,
                };
                prodotti.Add(prodotto);
            }
            return Json(prodotti, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VenditePerCliente()
        {
            return View();
        }
    }
}
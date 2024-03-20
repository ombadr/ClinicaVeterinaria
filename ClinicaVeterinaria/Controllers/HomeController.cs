using ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public ActionResult RicercaMicrochip()
        {
            ViewBag.Title = "RicercaMicrochip";

            return View();
        }


        [HttpGet]
        public JsonResult CercaAnimalePerMicrochip(string numeroMicrochip)
        {

            var animaleRicoverato =         db.Ricoveri
                                            .Where(r => r.Animali.Microchip == true && r.Animali.NumeroMicrochip == numeroMicrochip)
                                            .OrderByDescending(r => r.DataInizioRicovero).FirstOrDefault();  

            if (animaleRicoverato == null)
            {
                return Json(new { success = false, message = "Animale non trovato." }, JsonRequestBehavior.AllowGet); 
            }

            var dataRisposta = new
            {
                Nome = animaleRicoverato.Animali.Nome,
                Tipologia = animaleRicoverato.Animali.Tipologia,
                Foto = animaleRicoverato.Foto,
                DataRicovero = animaleRicoverato.DataInizioRicovero.ToString("dd/MM/yyyy")
            };

            return Json(new { success = true, data = dataRisposta }, JsonRequestBehavior.AllowGet);
        }





    }
}

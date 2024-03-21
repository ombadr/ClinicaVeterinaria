using ClinicaVeterinaria.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Veterinario")]
    public class RimborsoController : Controller
    {
        private DBContext db = new DBContext();

        public async Task<ActionResult> RicoveriAttivi()
        {
            var ricoveriAttivi = await db.Ricoveri.ToListAsync();
            return Json(ricoveriAttivi, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> RimborsiMensili()
        {
            var dataCorrente = DateTime.Now;
            var rimborsiMensili = await db.RimborsiRicoveri
                .Where(r => r.DataRimborso.Year == dataCorrente.Year && r.DataRimborso.Month == dataCorrente.Month)
                .ToListAsync();
            return Json(rimborsiMensili, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RimborsoMensile()
        {
            var RimborsiRicoveri = db.RimborsiRicoveri.ToList();
            return View(RimborsiRicoveri);
        }
    }
}

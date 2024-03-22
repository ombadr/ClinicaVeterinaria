using ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace ClinicaVeterinaria.Controllers
{
    public class AuthController : Controller
    {

        DBContext db = new DBContext();

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login( Utenti user, bool? keepLogged)
        {
            bool keepUserLogged = keepLogged.HasValue && keepLogged.Value;
            var loggedUser = db.Utenti.FirstOrDefault(u => u.Email == user.Email);
            if (loggedUser != null)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(user.PasswordHash, loggedUser.PasswordHash);
                if (validPassword)
                {
                    FormsAuthentication.SetAuthCookie(loggedUser.Id.ToString(), keepUserLogged);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["ErrorLogin"] = true;
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {

            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Nome,Cognome,Email,PasswordHash")]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Utenti.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser == null)
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);

                    var newUser = new Utenti
                    {
                        Nome = model.Nome,
                        Cognome = model.Cognome,
                        Email = model.Email,
                        PasswordHash = hashedPassword,
                        Ruolo = "Farmacista"
                    };
                    db.Utenti.Add(newUser);
                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(newUser.Id.ToString(), false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "An account with this email already exists.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
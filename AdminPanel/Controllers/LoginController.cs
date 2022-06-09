using ProjeModel.Entities;
using ProjeModel.Enumlar;
using ProjeModel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class LoginController : Controller
    {
        Entities db = new Entities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string parola)
        {
            var kull = db.Kullanicilar.FirstOrDefault(x => x.Email == email && x.Parola == parola && (x.AktifMi == null || x.AktifMi == true) && (x.SilindiMi == null || x.SilindiMi == false));
            if (kull != null)
            {
                Session["Kullanici"] = kull;
                return RedirectToAction("Index", "Home");
            }
            TempData["Mesaj"] = "Hatalı bilgi girişi yaptınız.".ToAlert(Alert.Danger);
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}
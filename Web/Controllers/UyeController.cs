using ProjeModel.Entities;
using ProjeModel.Enumlar;
using ProjeModel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UyeController : Controller
    {
        Entities db = new Entities();

        // GET: Uye
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kayit(string Ad, string Soyad, string Email, string Parola, string ParolaTekrar)
        {
            if (Parola != ParolaTekrar)
            {
                TempData["Mesaj"] = "Parolalar birbirleri ile uyuşmuyor.".ToAlert(Alert.Danger);
            }
            else
            {
                Uyeler uye = new Uyeler()
                {
                    Ad = Ad,
                    Soyad = Soyad,
                    Email = Email,
                    Parola = Parola,
                    KayitTarihi = DateTime.Now,
                    AktifMi = true,
                    SilindiMi = false
                };
                db.Uyeler.Add(uye);
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Üye kayıt işlemi başarı ile oluşturuldu.".ToAlert(Alert.Success);
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Üye kayıt işlemi esnasında bir hata oluşturuldu.".ToAlert(Alert.Danger);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Giris(string Email, string Parola)
        {
            var uye = db.Uyeler.FirstOrDefault(x => x.Email == Email && x.Parola == Parola && (x.AktifMi == true || x.AktifMi == null) && (x.SilindiMi == null || x.SilindiMi == false));
            if (uye != null)
            {
                Session["Uye"] = uye;
            }
            else
            {
                TempData["Mesaj"] = "Girilen bilgileri kontrol ederek tekrar deneyiniz.".ToAlert(Alert.Danger);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Cikis()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Siparislerim()
        {
            if (Session["Uye"] == null)
                return Redirect(Request.UrlReferrer.ToString());

            var uye = Session["Uye"].ToUye();
            var siparisler = db.Sepet.Where(x => x.UyeID == uye.id && (x.AktifMi == true || x.AktifMi == null) && (x.SilindiMi == false || x.SilindiMi == null)).ToList();

            return View(siparisler);
        }


        public ActionResult Bilgilerim()
        {
            if (Session["Uye"] == null)
                return Redirect(Request.UrlReferrer.ToString());

            var uye = Session["Uye"].ToUye();
            return View(uye);
        }
    }
}
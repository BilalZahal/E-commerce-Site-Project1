using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjeModel.Entities;
using ProjeModel.Enumlar;
using ProjeModel.Extensions;

namespace AdminPanel.Controllers
{
    public class KullanicilarsController : Controller
    {
        private Entities db = new Entities();

        // GET: Kullanicilars
        public ActionResult Index()
        {
            return View(db.Kullanicilar.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList());
        }

        // GET: Kullanicilars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // GET: Kullanicilars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanicilars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Ad,Soyad,Parola,Email,AktifMi")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                kullanicilar.KayitTarihi = DateTime.Now;
                db.Kullanicilar.Add(kullanicilar);
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Kayıt işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Kayıt işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(kullanicilar);
                }
            }
            return View(kullanicilar);
        }

        // GET: Kullanicilars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            kullanicilar.Parola = "";
            return View(kullanicilar);
        }

        // POST: Kullanicilars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Ad,Soyad,Email,Parola,AktifMi")] Kullanicilar kullanicilar, string parolaTekrar)
        {
            if (ModelState.IsValid)
            {
                var kullanici = db.Kullanicilar.Find(kullanicilar.id);
                kullanici.Ad = kullanicilar.Ad;
                kullanici.Soyad = kullanicilar.Soyad;
                kullanici.Email = kullanicilar.Email;
                if (!string.IsNullOrEmpty(kullanicilar.Parola) && kullanicilar.Parola == parolaTekrar)
                    kullanici.Parola = kullanicilar.Parola;
                kullanici.AktifMi = kullanicilar.AktifMi;

                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Güncelleme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Güncelleme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(kullanicilar);
                }
            }
            return View(kullanicilar);
        }

        // GET: Kullanicilars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            kullanicilar.SilindiMi = true;
            kullanicilar.SilinmeTarihi = DateTime.Now;
            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = "Silme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);                
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Silme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

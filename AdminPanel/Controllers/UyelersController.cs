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
    public class UyelersController : Controller
    {
        private Entities db = new Entities();

        // GET: Uyelers
        public ActionResult Index()
        {
            return View(db.Uyeler.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList());
        }

        // GET: Uyelers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uyeler uyeler = db.Uyeler.Find(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            return View(uyeler);
        }

        // GET: Uyelers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uyelers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Uyeler uyeler, string parolaTekrar)
        {
            if (ModelState.IsValid)
            {
                uyeler.KayitTarihi = DateTime.Now;
                db.Uyeler.Add(uyeler);
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Kayıt işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Kayıt işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(uyeler);
                }
            }

            return View(uyeler);
        }

        // GET: Uyelers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uyeler uyeler = db.Uyeler.Find(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            return View(uyeler);
        }

        // POST: Uyelers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Ad,Soyad,Email,Telefon,Adres,Parola,Cinsiyet,TcNo,DogumTarihi,AktifMi")] Uyeler uyeler, string parolaTekrar)
        {
            if (ModelState.IsValid)
            {
                var uye = db.Uyeler.Find(uyeler.id);
                uye.Ad = uyeler.Ad;
                uye.Soyad = uyeler.Soyad;
                uye.Email = uyeler.Email;
                uye.Telefon = uyeler.Telefon;
                uye.Adres = uyeler.Adres;
                if (!string.IsNullOrEmpty(uyeler.Parola) && uyeler.Parola == parolaTekrar)
                    uye.Parola = uyeler.Parola;
                uye.Cinsiyet = uyeler.Cinsiyet;
                uye.TcNo = uyeler.TcNo;
                uye.DogumTarihi = uyeler.DogumTarihi;
                uye.AktifMi = uyeler.AktifMi;

                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Güncelleme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Güncelleme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(uyeler);
                }
            }
            return View(uyeler);
        }

        // GET: Uyelers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uyeler uyeler = db.Uyeler.Find(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            uyeler.SilindiMi = true;
            uyeler.SilinmeTarihi = DateTime.Now;
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

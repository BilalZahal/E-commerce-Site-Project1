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
    public class SayfaKategorilerisController : Controller
    {
        private Entities db = new Entities();

        // GET: SayfaKategorileris
        public ActionResult Index()
        {
            return View(db.SayfaKategorileri.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList());
        }

        // GET: SayfaKategorileris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SayfaKategorileri sayfaKategorileri = db.SayfaKategorileri.Find(id);
            if (sayfaKategorileri == null)
            {
                return HttpNotFound();
            }
            return View(sayfaKategorileri);
        }

        // GET: SayfaKategorileris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SayfaKategorileris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Adi,Aciklama,AktifMi")] SayfaKategorileri sayfaKategorileri)
        {
            if (ModelState.IsValid)
            {
                sayfaKategorileri.KayitTarihi = DateTime.Now;
                db.SayfaKategorileri.Add(sayfaKategorileri);
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Kayıt işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Kayıt işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                }
            }

            return View(sayfaKategorileri);
        }

        // GET: SayfaKategorileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SayfaKategorileri sayfaKategorileri = db.SayfaKategorileri.Find(id);
            if (sayfaKategorileri == null)
            {
                return HttpNotFound();
            }
            return View(sayfaKategorileri);
        }

        // POST: SayfaKategorileris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Adi,Aciklama,AktifMi")] SayfaKategorileri sayfaKategorileri)
        {
            if (ModelState.IsValid)
            {
                var sayfa = db.SayfaKategorileri.Find(sayfaKategorileri.id);
                sayfa.Adi = sayfaKategorileri.Adi;
                sayfa.Aciklama = sayfaKategorileri.Aciklama;
                sayfa.AktifMi = sayfaKategorileri.AktifMi;
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Güncelleme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Güncelleme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                }
            }
            return View(sayfaKategorileri);
        }

        // GET: SayfaKategorileris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SayfaKategorileri sayfaKategorileri = db.SayfaKategorileri.Find(id);
            if (sayfaKategorileri == null)
            {
                return HttpNotFound();
            }
            sayfaKategorileri.SilindiMi = true;
            sayfaKategorileri.SilinmeTarihi = DateTime.Now;
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

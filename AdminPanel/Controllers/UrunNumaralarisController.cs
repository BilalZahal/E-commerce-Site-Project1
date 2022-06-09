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
    public class UrunNumaralarisController : Controller
    {
        private Entities db = new Entities();

        // GET: UrunNumaralaris
        public ActionResult Index()
        {
            return View(db.UrunNumaralari.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList());
        }

        // GET: UrunNumaralaris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunNumaralari urunNumaralari = db.UrunNumaralari.Find(id);
            if (urunNumaralari == null)
            {
                return HttpNotFound();
            }
            return View(urunNumaralari);
        }

        // GET: UrunNumaralaris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrunNumaralaris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Numara,AktifMi")] UrunNumaralari urunNumaralari)
        {
            if (ModelState.IsValid)
            {
                urunNumaralari.KayitTarihi = DateTime.Now;
                db.UrunNumaralari.Add(urunNumaralari);
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Kayıt işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Kayıt işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(urunNumaralari);
                }
            }

            return View(urunNumaralari);
        }

        // GET: UrunNumaralaris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunNumaralari urunNumaralari = db.UrunNumaralari.Find(id);
            if (urunNumaralari == null)
            {
                return HttpNotFound();
            }
            return View(urunNumaralari);
        }

        // POST: UrunNumaralaris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Numara,AktifMi")] UrunNumaralari urunNumaralari)
        {
            if (ModelState.IsValid)
            {
                var numara = db.UrunNumaralari.Find(urunNumaralari.id);
                numara.Numara = urunNumaralari.Numara;
                numara.AktifMi = urunNumaralari.AktifMi;
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Güncelleme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Güncelleme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View();
                }
            }
            return View(urunNumaralari);
        }

        // GET: UrunNumaralaris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunNumaralari urunNumaralari = db.UrunNumaralari.Find(id);
            if (urunNumaralari == null)
            {
                return HttpNotFound();
            }
            urunNumaralari.SilindiMi = true;
            urunNumaralari.SilinmeTarihi = DateTime.Now;
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

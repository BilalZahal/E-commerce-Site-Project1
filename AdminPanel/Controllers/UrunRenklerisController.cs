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
    public class UrunRenklerisController : Controller
    {
        private Entities db = new Entities();

        // GET: UrunRenkleris
        public ActionResult Index()
        {
            return View(db.UrunRenkleri.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList());
        }

        // GET: UrunRenkleris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunRenkleri urunRenkleri = db.UrunRenkleri.Find(id);
            if (urunRenkleri == null)
            {
                return HttpNotFound();
            }
            return View(urunRenkleri);
        }

        // GET: UrunRenkleris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrunRenkleris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Renk,AktifMi")] UrunRenkleri urunRenkleri)
        {
            if (ModelState.IsValid)
            {
                urunRenkleri.KayitTarihi = DateTime.Now;
                db.UrunRenkleri.Add(urunRenkleri);
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Kayıt işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Kayıt işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(urunRenkleri);
                }
            }

            return View(urunRenkleri);
        }

        // GET: UrunRenkleris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunRenkleri urunRenkleri = db.UrunRenkleri.Find(id);
            if (urunRenkleri == null)
            {
                return HttpNotFound();
            }
            return View(urunRenkleri);
        }

        // POST: UrunRenkleris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Renk,AktifMi")] UrunRenkleri urunRenkleri)
        {
            if (ModelState.IsValid)
            {
                var renk = db.UrunRenkleri.Find(urunRenkleri.id);
                renk.Renk = urunRenkleri.Renk;
                renk.AktifMi = urunRenkleri.AktifMi;
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Güncelleme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Geüncelleme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(urunRenkleri);
                }
            }
            return View(urunRenkleri);
        }

        // GET: UrunRenkleris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunRenkleri urunRenkleri = db.UrunRenkleri.Find(id);
            if (urunRenkleri == null)
            {
                return HttpNotFound();
            }
            urunRenkleri.SilindiMi = true;
            urunRenkleri.SilinmeTarihi = DateTime.Now;
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

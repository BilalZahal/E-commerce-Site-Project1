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
    public class SayfalarsController : Controller
    {
        private Entities db = new Entities();

        // GET: Sayfalars
        public ActionResult Index()
        {
            var sayfalar = db.Sayfalar.Include(s => s.SayfaKategorileri).Where(x => x.SilindiMi == null || x.SilindiMi == false);
            return View(sayfalar.ToList());
        }

        // GET: Sayfalars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sayfalar sayfalar = db.Sayfalar.Find(id);
            if (sayfalar == null)
            {
                return HttpNotFound();
            }
            return View(sayfalar);
        }

        // GET: Sayfalars/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.SayfaKategorileri, "id", "Adi");
            return View();
        }

        // POST: Sayfalars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Ad,Aciklama,KategoriID,AktifMi")] Sayfalar sayfalar)
        {
            if (ModelState.IsValid)
            {
                sayfalar.KayitTarihi = DateTime.Now;
                db.Sayfalar.Add(sayfalar);
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

            ViewBag.KategoriID = new SelectList(db.SayfaKategorileri, "id", "Adi", sayfalar.KategoriID);
            return View(sayfalar);
        }

        // GET: Sayfalars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sayfalar sayfalar = db.Sayfalar.Find(id);
            if (sayfalar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.SayfaKategorileri, "id", "Adi", sayfalar.KategoriID);
            return View(sayfalar);
        }

        // POST: Sayfalars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Ad,Aciklama,KategoriID,AktifMi")] Sayfalar sayfalar)
        {
            if (ModelState.IsValid)
            {
                var sayfa = db.Sayfalar.Find(sayfalar.id);
                sayfa.Ad = sayfalar.Ad;
                sayfa.Aciklama = sayfalar.Aciklama;
                sayfa.KategoriID = sayfalar.KategoriID;
                sayfa.AktifMi = sayfalar.AktifMi;
                //db.Entry(sayfalar).State = EntityState.Modified;
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
            ViewBag.KategoriID = new SelectList(db.SayfaKategorileri, "id", "Adi", sayfalar.KategoriID);
            return View(sayfalar);
        }

        // GET: Sayfalars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sayfalar sayfalar = db.Sayfalar.Find(id);
            if (sayfalar == null)
            {
                return HttpNotFound();
            }
            sayfalar.SilindiMi = true;
            sayfalar.SilinmeTarihi = DateTime.Now;
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

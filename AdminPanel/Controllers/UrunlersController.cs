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
    public class UrunlersController : Controller
    {
        private Entities db = new Entities();

        // GET: Urunlers
        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Kategoriler).Include(u => u.Kullanicilar).Where(x => x.SilindiMi == null || x.SilindiMi == false);
            return View(urunler.ToList());
        }

        // GET: Urunlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoriler.Where(x => x.SilindiMi == null || x.SilindiMi == false), "id", "Adi");
            ViewBag.Renkler = db.UrunRenkleri.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList();
            ViewBag.Numaralar = db.UrunNumaralari.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList();
            return View();
        }

        // POST: Urunlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Adi,KategoriID,Fiyat,indirim,Aciklama,TedarikSureci,AktifMi")] Urunler urunler, string[] renkler, string[] numaralar)
        {
            if (ModelState.IsValid)
            {
                string urunRenk = string.Empty, urunNumara = string.Empty;
                for (int i = 0; i < renkler.Length; i++)
                    urunRenk += (i == 0 ? "" : ",") + renkler[i];
                for (int i = 0; i < numaralar.Length; i++)
                    urunNumara += (i == 0 ? "" : ",") + numaralar[i];

                urunler.KayitTarihi = DateTime.Now;
                urunler.RenkBilgisi = urunRenk;
                urunler.NumaraBilgisi = urunNumara;
                db.Urunler.Add(urunler);
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Kayıt işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Kayıt işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(urunler);
                }
            }

            ViewBag.KategoriID = new SelectList(db.Kategoriler.Where(x => x.SilindiMi == null || x.SilindiMi == false), "id", "Adi", urunler.KategoriID);
            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler.Where(x => x.SilindiMi == null || x.SilindiMi == false), "id", "Adi", urunler.KategoriID);
            ViewBag.Renkler = db.UrunRenkleri.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList();
            ViewBag.Numaralar = db.UrunNumaralari.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList();
            return View(urunler);
        }

        // POST: Urunlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Adi,KategoriID,indirim,Aciklama,TedarikSureci,AktifMi")] Urunler urunler, string[] renkler, string[] numaralar, string Fiyat)
        {
            if (ModelState.IsValid)
            {
                var urun = db.Urunler.Find(urunler.id);
                urun.Adi = urunler.Adi;
                urun.KategoriID = urunler.KategoriID;
                urun.Fiyat = Fiyat.ToDecimal();
                urun.indirim = urunler.indirim;
                urun.TedarikSureci = urunler.TedarikSureci;
                urun.AktifMi = urunler.AktifMi;

                string urunRenk = string.Empty, urunNumara = string.Empty;
                for (int i = 0; i < renkler.Length; i++)
                    urunRenk += (i == 0 ? "" : ",") + renkler[i];
                for (int i = 0; i < numaralar.Length; i++)
                    urunNumara += (i == 0 ? "" : ",") + numaralar[i];

                urun.RenkBilgisi = urunRenk;
                urun.NumaraBilgisi = urunNumara;
                try
                {
                    db.SaveChanges();
                    TempData["Mesaj"] = "Güncelleme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Güncelleme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                    return View(urunler);
                }
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler.Where(x => x.SilindiMi == null || x.SilindiMi == false), "id", "Adi", urunler.KategoriID);
            ViewBag.Renkler = db.UrunRenkleri.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList();
            ViewBag.Numaralar = db.UrunNumaralari.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList();
            return View(urunler);
        }

        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();
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

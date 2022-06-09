using PagedList;
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
    public class KategoriController : Controller
    {
        Entities db = new Entities();
        // GET: Kategori
        public ActionResult Index(int? page)
        {
            var kategoriler = db.Kategoriler.Where(x => x.SilindiMi == null || x.SilindiMi == false).ToList().ToPagedList(page ?? 1, 2);
            return View(kategoriler);
        }


        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kategoriler kategori)
        {
            db.Kategoriler.Add(kategori);
            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = "Kayıt işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Kayıt işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                return View();
            }
        }

        public ActionResult Duzenle(int id)
        {
            var kategori = db.Kategoriler.Find(id);
            return View(kategori);
        }

        [HttpPost]
        public ActionResult Duzenle(Kategoriler kategori)
        {
            var edit = db.Kategoriler.Find(kategori.id);
            edit.Adi = kategori.Adi;
            edit.AktifMi = kategori.AktifMi;
            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = "Güncelleme işlemi başarı ile tamamlanmıştır.".ToAlert(Alert.Success);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Güncelleme işlemi esnasında bir sorun oluştu.".ToAlert(Alert.Danger);
                return View(kategori);
            }
        }

        public ActionResult Sil(int id)
        {
            var kategori = db.Kategoriler.Find(id);
            kategori.SilindiMi = true;
            kategori.SilinmeTarihi = DateTime.Now;
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
    }
}
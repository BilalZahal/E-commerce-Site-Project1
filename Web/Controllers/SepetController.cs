using ProjeModel.Entities;
using ProjeModel.Enumlar;
using ProjeModel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class SepetController : Controller
    {
        Entities db = new Entities();

        // GET: Sepet
        public ActionResult Index()
        {
            var sepetDetay = db.SepetDetay.Where(x => x.SepetID == null && (x.AktifMi == null || x.AktifMi == true) && (x.SilindiMi == null || x.SilindiMi == false)).ToList();
            return View(sepetDetay);
        }

        [HttpPost]
        public ActionResult Ekle(int id, string renk, string numara, int adet)
        {
            if (Session["Uye"] == null)
            {
                TempData["Mesaj"] = "Sepete ürün eklemek için önce üye girişi yapmalısınız.".ToAlert(Alert.Danger);
                return RedirectToAction("Detay", "Urun", new { id = id });
            }


            var urun = db.Urunler.Find(id);
            if (urun == null)
            {
                TempData["Mesaj"] = "Bu ürüne ait sepet eklemesi yapılamadı.".ToAlert(Alert.Danger);
                return RedirectToAction("Detay", "Urun", new { id = id });
            }

            var sepettekiUrun = db.SepetDetay.FirstOrDefault(x => x.SepetID == null && x.UrunID == id && x.Renk == renk && x.Numara == numara && (x.AktifMi == null || x.AktifMi == true) && (x.SilindiMi == null || x.SilindiMi == false));
            if (sepettekiUrun == null)
            {
                SepetDetay sepetDetay = new SepetDetay()
                {
                    UrunID = id,
                    Fiyat = urun.Fiyat,
                    indirim = urun.indirim,
                    Adet = adet,
                    Renk = renk,
                    Numara = numara,
                    AktifMi = true,
                    KayitTarihi = DateTime.Now
                };
                db.SepetDetay.Add(sepetDetay);
            }
            else
            {
                sepettekiUrun.Adet += adet;
            }

            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Sepete ekleme işlemi esnasında bir hata oluştu.".ToAlert(Alert.Danger);
                return RedirectToAction("Detay", "Urun", new { id = id });
            }
        }


        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                TempData["Mesaj"] = "Ürün silme işlemi gerçekleşmedi.".ToAlert(Alert.Danger);
            }
            var sepetDetay = db.SepetDetay.Find(id);
            if (sepetDetay == null)
            {
                TempData["Mesaj"] = "Ürün silme işlemi gerçekleşmedi.".ToAlert(Alert.Danger);
            }
            sepetDetay.SilindiMi = true;
            sepetDetay.SilinmeTarihi = DateTime.Now;
            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = "Ürün silme işlemi başarı ile gerçekleşti.".ToAlert(Alert.Success);
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Ürün silme işlemi gerçekleşmedi.".ToAlert(Alert.Danger);
            }
            return RedirectToAction("Index");
        }

        public JsonResult SepeteEkle(int id, int adet, string renk, string numara)
        {
            var urun = db.Urunler.Find(id);

            var sepettekiUrun = db.SepetDetay.FirstOrDefault(x => x.SepetID == null && x.UrunID == id && x.Renk == renk && x.Numara == numara && (x.AktifMi == null || x.AktifMi == true) && (x.SilindiMi == null || x.SilindiMi == false));
            if (sepettekiUrun == null)
            {
                var sepetDetay = new SepetDetay()
                {
                    UrunID = urun.id,
                    Fiyat = urun.Fiyat,
                    indirim = urun.indirim,
                    Adet = adet,
                    Renk = renk,
                    Numara = numara,
                    KayitTarihi = DateTime.Now,
                    AktifMi = true
                };
                db.SepetDetay.Add(sepetDetay);
            }
            else
            {
                sepettekiUrun.Adet += adet;
            }

            try
            {
                db.SaveChanges();
                return Json(new JsonClass() { Success = true, Message = Methodlar.SepetUrunGoster() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonClass() { Success = false, Message = "Hata Oluştu." }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Tamamla()
        {
            if (Session["Uye"] == null)
            {
                TempData["Mesaj"] = "Alışverişi tamamlamak için önce üye girişi yapmalısınız.".ToAlert(Alert.Danger);
                return RedirectToAction("Index");
            }
            var uye = Session["Uye"].ToUye();
            var sepet = new Sepet()
            {
                UyeID = uye.id,
                AktifMi = true,
                KayitTarihi = DateTime.Now,
                SiparisNumarasi = new Random().Next(100000, 999999).ToString(),
                SiparisDurumu = (int)SiparisDurumu.Hazirlaniyor
            };
            db.Sepet.Add(sepet);

            try
            {
                db.SaveChanges();

                var sepetDetay = db.SepetDetay.Where(x => x.SepetID == null && (x.AktifMi == null || x.AktifMi == true) && (x.SilindiMi == null || x.SilindiMi == false));
                foreach (var item in sepetDetay)
                {
                    item.SepetID = sepet.id;
                }
                db.SaveChanges();

                TempData["Mesaj"] = "Sipariş tamamlama işlemi gerçekleşti.".ToAlert(Alert.Success);
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Sipariş tamamlama işlemi esnasında bir hata oluştu.".ToAlert(Alert.Danger);
            }

            return RedirectToAction("Index");
        }

    }
}
using ProjeModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class KategoriController : Controller
    {
        Entities db = new Entities();

        // GET: Kategori
        public ActionResult Index(int? id, int[] renk, int[] numara, decimal? min, decimal? max)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var kategori = db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }

            var urunList = kategori.Urunler.Where(x => (x.SilindiMi == null || x.SilindiMi == false) && (x.AktifMi == true || x.AktifMi == null)).ToList();

            if (renk != null && renk.Length > 0)
            {
                string renkler = string.Empty;
                for (int i = 0; i < renk.Length; i++)
                {
                    renkler = (i == 0 ? "" : ",") + renk[i];
                }
                urunList = urunList.Where(x => x.RenkBilgisi.Contains(renkler)).ToList();
            }
            if (numara != null && numara.Length > 0)
            {
                string numaralar = string.Empty;
                for (int i = 0; i < numara.Length; i++)
                {
                    numaralar = (i == 0 ? "" : ",") + numara[i];
                }
                urunList = urunList.Where(x => x.NumaraBilgisi.Contains(numaralar)).ToList();
            }

            if (min != null)
            {
                urunList = urunList.Where(x => x.Fiyat > min).ToList();
            }
            if (max != null)
            {
                urunList = urunList.Where(x => x.Fiyat < max).ToList();
            }


            ViewBag.Urunler = urunList;
            ViewBag.Kategoriler = db.Kategoriler.Where(x => (x.SilindiMi == null || x.SilindiMi == false) && (x.AktifMi == true || x.AktifMi == null)).ToList();
            ViewBag.Renkler = db.UrunRenkleri.Where(x => (x.SilindiMi == null || x.SilindiMi == false) && (x.AktifMi == true || x.AktifMi == null)).ToList();
            ViewBag.Numaralar = db.UrunNumaralari.Where(x => (x.SilindiMi == null || x.SilindiMi == false) && (x.AktifMi == true || x.AktifMi == null)).ToList();
            return View(kategori);
        }
    }
}
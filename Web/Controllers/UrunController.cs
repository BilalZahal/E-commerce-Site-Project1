using ProjeModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UrunController : Controller
    {
        Entities db = new Entities();

        // GET: Urun
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detay(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var urun = db.Urunler.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }

            ViewBag.Renkler = db.UrunRenkleri.Where(x => (x.SilindiMi == null || x.SilindiMi == false) && (x.AktifMi == true || x.AktifMi == null)).ToList();
            ViewBag.Numaralar = db.UrunNumaralari.Where(x => (x.SilindiMi == null || x.SilindiMi == false) && (x.AktifMi == true || x.AktifMi == null)).ToList();
            return View(urun);
        }
    }
}
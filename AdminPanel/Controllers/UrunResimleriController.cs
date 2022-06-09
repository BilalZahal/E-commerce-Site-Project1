using ProjeModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class UrunResimleriController : Controller
    {
        Entities db = new Entities();

        // GET: UrunResimleri
        public ActionResult Index(int urunid)
        {
            var resimler = db.UrunResimleri.Where(x => x.UrunID == urunid && (x.SilindiMi == null || x.SilindiMi == false)).ToList();
            return View(resimler);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase resim, int urunId)
        {
            if (resim.ContentLength > 0)
            {
                string resimAdi = Guid.NewGuid() + "_" + resim.FileName;
                string resimYolu = "/resimler/";
                resim.SaveAs(HttpContext.Server.MapPath(resimYolu + resimAdi));

                db.UrunResimleri.Add(new UrunResimleri()
                {
                    UrunID = urunId,
                    Resim = resimYolu + resimAdi,
                    AktifMi = true,
                    KayitTarihi = DateTime.Now
                });
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { urunid = urunId });
        }
    }
}
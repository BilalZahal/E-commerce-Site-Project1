using ProjeModel.Entities;
using ProjeModel.Enumlar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjeModel.Extensions
{
    public static class Methodlar
    {
        readonly static Entities.Entities db = new Entities.Entities();
        public static string ToAktifMi(this object v)
        {
            string txt = string.Empty;

            if (v == null)
            {
                return "<span class='label label-success'>Aktif</span>";
            }

            if (Convert.ToBoolean(v))
            {
                txt = "<span class='label label-success'>Aktif</span>";
            }
            else
            {
                txt = "<span class='label label-danger'>Pasif</span>";
            }

            return txt;
        }
        public static string ToAlert(this string v, Alert alert)
        {
            string txt = string.Empty;
            switch (alert)
            {
                case Alert.Success:
                    txt = "<div class='alert alert-success' role='alert'>" + v + "</div>";
                    break;
                case Alert.Info:
                    txt = "<div class='alert alert-info' role='alert'>" + v + "</div>";
                    break;
                case Alert.Warning:
                    txt = "<div class='alert alert-warning' role='alert'>" + v + "</div>";
                    break;
                case Alert.Danger:
                    txt = "<div class='alert alert-danger' role='alert'>" + v + "</div>";
                    break;
            }
            return txt;
        }
        public static string ToTextDecimal(this decimal? v)
        {
            if (v == null)
                return "0";
            try
            {
                return v.Value.ToString("#.##");
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string IndirimliFiyat(this decimal? fiyat, double? indirim)
        {
            if (fiyat == null)
                return "0";
            try
            {
                if (indirim != null || indirim > 0)
                    return (fiyat.Value * (1 - ((decimal)indirim.Value / 100))).ToString("C");
                return fiyat.Value.ToString("C");
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static decimal ToDecimal(this string v)
        {
            if (string.IsNullOrEmpty(v))
                return 0;
            try
            {
                return decimal.Parse(v);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Kullanicilar ToUser(this object v)
        {
            if (v == null)
                HttpContext.Current.Response.Redirect("/Login");
            Kullanicilar kull = new Kullanicilar();
            try
            {
                kull = (Kullanicilar)v;
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/Login");
            }
            return kull;
        }

        public static Uyeler ToUye(this object v)
        {
            if (v == null)
                HttpContext.Current.Response.Redirect("/");
            Uyeler uye = new Uyeler();
            try
            {
                uye = (Uyeler)v;
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/");
            }
            return uye;
        }

        public static string RenkGoster(this string renk)
        {
            try
            {
                int renkId = int.Parse(renk);
                return db.UrunRenkleri.FirstOrDefault(x => x.id == renkId).Renk;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string NumaraGoster(this string numara)
        {
            try
            {
                int numaraId = int.Parse(numara);
                return db.UrunNumaralari.FirstOrDefault(x => x.id == numaraId).Numara;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static decimal ToDecimal(this decimal? v)
        {
            if (v == null)
                return 0;
            return v.Value;
        }
        public static double ToDouble(this double? v)
        {
            if (v == null)
                return 0;
            return v.Value;
        }

        public static decimal SiparisHesapla(this SepetDetay detay)
        {
            decimal fiyat = detay.Fiyat.ToDecimal();
            decimal indirim = (decimal)(1 - (detay.indirim.ToDouble() / 100));
            int adet = detay.Adet.Value;
            return (fiyat * indirim) * adet;
        }

        public static int SepetUrunSayisi()
        {
            return db.SepetDetay.Where(x => x.SepetID == null && (x.AktifMi == null || x.AktifMi == true) && (x.SilindiMi == null || x.SilindiMi == false)).Count();
        }

        public static string SepetUrunGoster()
        {
            decimal siparisToplam = 0;
            string txt = string.Empty;
            var sepetDetay = new Entities.Entities().SepetDetay.Where(x => x.SepetID == null && (x.AktifMi == null || x.AktifMi == true) && (x.SilindiMi == null || x.SilindiMi == false)).ToList();
            if (sepetDetay.Count > 0)
            {
                // Sepet Başlıklar
                txt += "<div class='row'>" +
                            "<div class='col-sm-6'><b>Ürün Adı</b></div>" +
                            "<div class='col-sm-3'><b>Adet</b></div>" +
                            "<div class='col-sm-3'><b>Fiyat</b></div>" +
                        "</div>";
                foreach (var item in sepetDetay)
                {
                    siparisToplam += item.SiparisHesapla();
                    // Ürün Detay
                    txt += "<div class='row'>" +
                                "<div class='col-sm-6'>" + item.Urunler.Adi + "</div>" +
                                "<div class='col-sm-3'>" + item.Adet + "</div>" +
                                "<div class='col-sm-3'>" + item.Fiyat.IndirimliFiyat(item.indirim) + "</div>" +
                            "</div>";
                }
            }
            else
            {
                // Ürün Bulunamadı
                txt += "<div class='text-center'><i class='fa fa-shopping-cart mb-2' aria-hidden='true' style='font-size:36px;'></i><div>Sepetinizde Ürün Bulunmamaktadır.</div></div>";
            }

            // Genel Toplam
            txt += "<div class='bg-secondary text-white p-1 mb-2 mt-2 text-center'>Genel Toplam: "+ siparisToplam.ToString("C") + "</div>";

            return txt;
        }

        public static string ToSiparisDurumu(this int? v) {
            string txt = string.Empty;
            switch (v)
            {
                case (int)SiparisDurumu.Beklemede:
                    txt = "Beklemede";
                    break;
                case (int)SiparisDurumu.Hazirlaniyor:
                    txt = "Hazırlanıyor";
                    break;
                case (int)SiparisDurumu.Kargoda:
                    txt = "Kargoda";
                    break;
                case (int)SiparisDurumu.Tamamlandi:
                    txt = "Tamamlandı";
                    break;
                case (int)SiparisDurumu.TeslimEdildi:
                    txt = "Teslim Edildi";
                    break;
            }
            return txt;
        }
    }
}

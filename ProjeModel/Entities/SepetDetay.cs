//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjeModel.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class SepetDetay
    {
        public int id { get; set; }
        public Nullable<int> SepetID { get; set; }
        public Nullable<int> UrunID { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public Nullable<double> indirim { get; set; }
        public Nullable<int> Adet { get; set; }
        public string Renk { get; set; }
        public string Numara { get; set; }
        public Nullable<bool> AktifMi { get; set; }
        public Nullable<bool> SilindiMi { get; set; }
        public Nullable<System.DateTime> KayitTarihi { get; set; }
        public Nullable<System.DateTime> SilinmeTarihi { get; set; }
    
        public virtual Sepet Sepet { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}
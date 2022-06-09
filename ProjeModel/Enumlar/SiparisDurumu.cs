using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeModel.Enumlar
{
    public enum SiparisDurumu
    {
        Beklemede,
        [Display(Name ="Hazırlanıyor")]
        Hazirlaniyor,
        Kargoda,
        TeslimEdildi,
        [Display(Name = "Tamamlandı")]
        Tamamlandi
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.Entities
{
    [Table("Dues")]
    public class Dues : MyEntitiyBase
    {
        [DisplayName("Aidat Dönemi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int DuesPeriodId { get; set; }

        [DisplayName("Bu Ay Toplam Aidat TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal ThisMonthTotalDues { get; set; }

        [DisplayName("Demirbaş Tutar TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal NotDuesAmount { get; set; }

        [DisplayName("Aidat Tutar TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal DuesAmount { get; set; }

        [DisplayName("Son Ödeme Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public DateTime LastPaymentDate { get; set; }

        [DisplayName("Kategori"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Category { get; set; }

        [DisplayName("Aidat Dağıtım Şekli"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string DuesDeliveryWay { get; set; }
    }
}

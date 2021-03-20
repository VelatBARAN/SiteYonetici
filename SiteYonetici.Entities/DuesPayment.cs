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
    [Table("DuesPayment")]
    public class DuesPayment : MyEntitiyBase
    {
        [DisplayName("Son Ödeme Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public DateTime LastPaymentDate { get; set; }

        [DisplayName("Aidat Dönemi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int DuesPeriodId { get; set; }

        [DisplayName("Daire No"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int ApartmentId { get; set; }

        [DisplayName("Demirbaş Tutar TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal NotDuesAmount { get; set; }

        [DisplayName("Aidat Toplam Tutar TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal TotalDuesAmount { get; set; }

        [DisplayName("Ödenen TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal PaidAmount { get; set; }

        [DisplayName("Kalan TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal RemainingAmount { get; set; }

        [DisplayName("Ödendi Mi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public bool IsPaid { get; set; }

        public virtual DuesPeriod DuesPeriod { get; set; }
        public virtual Apartment Apartment { get; set; }
        public virtual List<DuesPaymentDetails> DuesPaymentDetails { get; set; }

        public DuesPayment()
        {
            DuesPaymentDetails = new List<DuesPaymentDetails>();
        }

    }
}

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
    [Table("DuesPaymentDetails")]
    public class DuesPaymentDetails : MyEntitiyBase
    {
        [DisplayName("Aidat Ödeme No"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int DuesPaymentId { get; set; }

        [DisplayName("Kasa"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int SafeId { get; set; }

        [DisplayName("Ödenen TL"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal PaidAmount { get; set; }

        [DisplayName("Ödeme Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public DateTime PaymentDate { get; set; }

        public virtual DuesPayment DuesPayment { get; set; }
        public virtual Safe Safe { get; set; }
    }
}

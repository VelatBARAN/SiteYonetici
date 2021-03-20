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
    [Table("Expense")]
    public class Expense : MyEntitiyBase
    {
        [DisplayName("Cari"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int ClientId { get; set; }

        [DisplayName("Gider Hesabı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int ExpenseAccountId { get; set; }

        [DisplayName("Kasa"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int SafeId { get; set; }

        [DisplayName("Tutar"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal Amount { get; set; }

        [DisplayName("AidatMı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public bool IsDues { get; set; }

        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(350, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Description { get; set; }

        public virtual Safe Safe { get; set; }
        public virtual ExpenseAccount ExpenseAccount { get; set; }
        public virtual Client Client { get; set; }
    }
}

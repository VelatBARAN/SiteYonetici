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
    [Table("Income")]
    public class Income : MyEntitiyBase
    {
        [DisplayName("Kasa"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int SafeId { get; set; }

        [DisplayName("Gelir Hesabı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int IncomeAccountId { get; set; }

        [DisplayName("Tutar"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal Amount { get; set; }

        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Description { get; set; }

        public virtual Safe Safe { get; set; }
        public virtual IncomeAccount IncomeAccount { get; set; }
    }
}

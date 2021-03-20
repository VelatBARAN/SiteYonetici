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
    [Table("Safe")]
    public class Safe : MyEntitiyBase
    {
        [DisplayName("Kasa Tipi"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string SafeType { get; set; }

        [DisplayName("Cari Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        public virtual List<Income> Incomes { get; set; }
        public virtual List<Expense> Expenses { get; set; }
        public virtual List<DuesPaymentDetails> DuesPaymentDetails { get; set; }
        public Safe()
        {
            Incomes = new List<Income>();
            Expenses = new List<Expense>();
            DuesPaymentDetails = new List<DuesPaymentDetails>();
        }
    }
}

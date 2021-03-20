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
    [Table("ExpenseAccount")]
    public class ExpenseAccount : MyEntitiyBase
    {
        [DisplayName("Hesap Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string AccountName { get; set; }

        public virtual List<Expense> Expenses { get; set; }
        public ExpenseAccount()
        {
            Expenses = new List<Expense>();
        }
    }
}

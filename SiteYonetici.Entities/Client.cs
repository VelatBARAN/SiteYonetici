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
    [Table("Client")]
    public class Client : MyEntitiyBase
    {
        [DisplayName("Cari Adı"), Required(ErrorMessage ="{0} alanı boş geçilemez.") , StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Vergi No"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TaxNo { get; set; }

        [DisplayName("Vergi Dairesi"),  StringLength(200, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TaxOffice { get; set; }

        [DisplayName("Hesap/Iban No"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string AccountOrIbanNo { get; set; }

        public virtual List<Expense> Expenses { get; set; }
        public Client()
        {
            Expenses = new List<Expense>();
        }
    }
}

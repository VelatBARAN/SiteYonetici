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
    [Table("DuesPeriod")]
    public class DuesPeriod : MyEntitiyBase
    {
        [DisplayName("Aidat Yılı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(4, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string DuesYear { get; set; }

        [DisplayName("Aidat Ayı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(10, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string DuesMonth { get; set; }

        public virtual List<Dues> Dues { get; set; }
        public virtual List<DuesPayment> DuesPayment { get; set; }
        public DuesPeriod()
        {
            Dues = new List<Dues>();
            DuesPayment = new List<DuesPayment>();
        }
    }
}

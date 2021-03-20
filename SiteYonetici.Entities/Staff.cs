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
    [Table("Staff")]
    public class Staff : MyEntitiyBase
    {
        [DisplayName("Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("TC"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TC { get; set; }

        [DisplayName("Telefon"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz telefon numarası")]
        public string Phone { get; set; }

        [DisplayName("Maaş"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public decimal Salary { get; set; }

        [DisplayName("Sigorta No"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string InsuranceNo { get; set; }

        [DisplayName("İş Tanımı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string JobDefinition { get; set; }

        [DisplayName("Görev Türü"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int TaskTypeId { get; set; }

        [DisplayName("Aktif Mi")]
        public bool IsActive { get; set; }

        public virtual TaskType TaskType { get; set; }
    }
}

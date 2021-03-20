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
    [Table("Managers")]
    public class Managers : MyEntitiyBase
    {
        [DisplayName("Adı"), Required(ErrorMessage ="{0} alanı boş geçilemez.") , StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Surname"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("TC"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TC { get; set; }

        [DisplayName("Telefon"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz telefon numarası")]
        public string Phone { get; set; }

        [DisplayName("Eposta"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),
            DataType(DataType.EmailAddress)]
        public string Eposta { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),
    DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Aktif Mi")]
        public bool IsActive { get; set; }
    }
}

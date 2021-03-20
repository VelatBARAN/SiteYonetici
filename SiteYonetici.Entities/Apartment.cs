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
    [Table("Apartment")]
    public class Apartment : MyEntitiyBase
    {
        [DisplayName("Kat"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int Floor { get; set; }

        [DisplayName("Daire No"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int ApartmentNo { get; set; }

        [DisplayName("Sahibin Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string NameOfOwner { get; set; }

        [DisplayName("Sahibin Soyadı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string SurnameOfOwner { get; set; }

        [DisplayName("Sahibin Telefonu"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz telefon numarası")]
        public string PhoneOfOwner { get; set; }

        [DisplayName("Kiralayan Adı"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string NameOfHirer { get; set; }

        [DisplayName("Kiralayan Soyadı"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string SurnameOfHirer { get; set; }

        [DisplayName("Kiralayan Telefonu"), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz telefon numarası")]
        public string PhoneOfHirer { get; set; }

        [DisplayName("Daire Tipi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int ApartmentTypeId { get; set; }

        [DisplayName("Blok"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int BlockId { get; set; }

        public virtual ApartmentType ApartmentTypes { get; set; }
        public virtual Block Block { get; set; }
        public virtual List<DuesPayment> DuesPayment { get; set; }
        public Apartment()
        {
            DuesPayment = new List<DuesPayment>();
        }
    }
}

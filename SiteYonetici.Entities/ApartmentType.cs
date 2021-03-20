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
    [Table("ApartmentTypes")]
    public class ApartmentType : MyEntitiyBase
    {
        [DisplayName("Daire Türü"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string ApartmentTypeName { get; set; }

        public virtual List<Apartment> Apartments { get; set; }
        public ApartmentType()
        {
            Apartments = new List<Apartment>();
        }
    }
}

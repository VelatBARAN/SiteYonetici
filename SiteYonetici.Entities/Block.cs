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
    [Table("Block")]
    public class Block : MyEntitiyBase
    {
        [DisplayName("Blok Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Daires Sayısı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int ApartmentCount { get; set; }

        [DisplayName("Görevli Personel Sayısı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int TaskedEmployeeCount { get; set; }

        [DisplayName("Açıklama")]
        public string Description { get; set; }

        public virtual List<Apartment> Apartments { get; set; }
        public Block()
        {
            Apartments = new List<Apartment>();
        }
    }
}

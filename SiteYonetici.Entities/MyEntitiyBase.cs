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
    public class MyEntitiyBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Kayıt Tarihi"),Required(ErrorMessage ="{0} alanı boş geçilemez")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Güncelleme Tarihi")]
        public DateTime ModifiedDate { get; set; }

        [DisplayName("Kaydeden"),StringLength(50,ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string SavedUsername { get; set; }
    }
}

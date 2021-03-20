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
    [Table("TaskTypes")]
    public class TaskType : MyEntitiyBase
    {
        [DisplayName("Görev Türü"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TaskTypeName { get; set; }

        public virtual List<Staff> Staffs { get; set; }
        public TaskType()
        {
            Staffs = new List<Staff>();
        }
    }
}

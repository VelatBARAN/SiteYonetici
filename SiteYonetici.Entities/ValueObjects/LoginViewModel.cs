using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.Entities.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("Eposta"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.EmailAddress)]
        public string Eposta { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

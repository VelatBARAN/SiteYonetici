using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.Entities.ValueObjects
{
    public class ForgetPasswordModel
    {
        [DisplayName("Eposta"), Required(ErrorMessage = "{0} alanı boş geçilemez.."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalı."), EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-mail adresi giriniz.")]
        public string Eposta { get; set; }
    }
}

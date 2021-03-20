using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.Entities.ValueObjects
{
    public class ResetPasswordModel
    {
        [DisplayName("Eposta"), Required(ErrorMessage = "{0} alanı boş geçilemez.."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalı."), EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-mail adresi giriniz.")]
        public string Eposta { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez.."), DataType(DataType.Password),
            StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalı.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez.."), DataType(DataType.Password),
            StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalı."), Compare("Password", ErrorMessage = "{0} ile {1} alanı uyuşmuyor.")]
        public string RePassword { get; set; }
    }
}

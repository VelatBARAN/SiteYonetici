using PersonnelPermissionFollowing.Common.Helpers;
using SiteYonetici.BusinessLayer.Abstract;
using SiteYonetici.BusinessLayer.Result;
using SiteYonetici.Common.Message;
using SiteYonetici.Entities;
using SiteYonetici.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace SiteYonetici.BusinessLayer
{
    public class ManagersManager : ManagerBase<Managers>
    {
        private BusinessLayerResult<Managers> layerResult = new BusinessLayerResult<Managers>();

        public BusinessLayerResult<Managers> Login(LoginViewModel model)
        {
            string pass = Crypto.Hash(model.Password.ToString(), "MD5");
            layerResult.Result = Find(x => x.Eposta == model.Eposta && x.Password == pass);
            if (layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.EpostaOrPasswordError, "Kullanıcı adı yada şifreyi hatalı girdiniz.");
            }
            else
            {
                if (layerResult.Result.IsActive == false)
                {
                    layerResult.AddError(ErrorMessageCode.EpostaOrPasswordError, "Kullanıcı yönetici pozisyonu pasif durumundadır.");
                }
            }
            return layerResult;
        }

        public BusinessLayerResult<Managers> PasswordChange(ResetPasswordModel model)
        {

            layerResult.Result = Find(x => x.Eposta == model.Eposta);
            if (layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.PasswordChangeOccurredError, "Kullanıcı bulunamadı");
            }

            layerResult.Result.Password = Crypto.Hash(model.Password.ToString(), "MD5");

            if (Update(layerResult.Result) == 0)
            {
                layerResult.AddError(ErrorMessageCode.PasswordChangeOccurredError, "Şifre değiştirilirken hata oluştu.");
            }

            return layerResult;
        }

        public BusinessLayerResult<Managers> PasswordForget(ForgetPasswordModel model)
        {
            layerResult.Result = Find(x=>x.Eposta == model.Eposta);
            if (layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.UserNotFound, "Sistemde böyle bir kayıtlı eposta bulunmamaktadır.");
                return layerResult;
            }
            int newnumber = 0;
            Random rnd = new Random();
            newnumber = rnd.Next();
            layerResult.Result.Password = Crypto.Hash(newnumber.ToString(), "MD5");

            if (Update(layerResult.Result) > 0)
            {
                string body = $"Merhaba {layerResult.Result.Name} {layerResult.Result.Surname}; <br> <br> Yeni Şİfre : {newnumber}";
                MailHelper.SendMail(body, layerResult.Result.Eposta, "Site Yönetim Sistemi - Yeni Şifre Talebi", true);
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.CouldNotSendPassword, "Şifre gönderilirken hata oluştu!");
            }
            return layerResult;
        }
    }
}

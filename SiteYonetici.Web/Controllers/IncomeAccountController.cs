using SiteYonetici.BusinessLayer;
using SiteYonetici.Entities;
using SiteYonetici.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SiteYonetici.Web.Controllers
{
    [Auth]
    public class IncomeAccountController : Controller
    {
        private IncomeAccountManager incomeAccountManager = new IncomeAccountManager();
        public ActionResult Index()
        {
            return View(incomeAccountManager.ListQueryable().OrderByDescending(x=>x.CreatedDate).ToList());
        }

        [HttpPost]
        public JsonResult Create(IncomeAccount incomeAccount)
        {
            try
            {
                if (incomeAccount.AccountName != null)
                {
                    int result = incomeAccountManager.Insert(incomeAccount);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Gelir hesap kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Gelir hesap kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Gelir hesap kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = incomeAccountManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.AccountName }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(IncomeAccount incomeAccount)
        {
            try
            {
                if (incomeAccount.AccountName != null)
                {
                    IncomeAccount i = incomeAccountManager.Find(x => x.Id == incomeAccount.Id);
                    i.AccountName = incomeAccount.AccountName;
                    int results = incomeAccountManager.Update(i);
                    if (results > 0)
                    {
                        return Json(new { hasError = false, Message = "Güncelleme işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Güncelleme yapılırken hata oluştu!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Güncelleme yapılırken hata oluştu!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeAccount i = incomeAccountManager.Find(x => x.Id == id.Value);
            if (i == null)
            {
                return HttpNotFound();
            }

            if (incomeAccountManager.Delete(i) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(i);
        }
    }
}
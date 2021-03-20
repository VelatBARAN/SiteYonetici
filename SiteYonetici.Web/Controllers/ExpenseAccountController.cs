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
    public class ExpenseAccountController : Controller
    {
        private ExpenseAccountManager expenseAccountManager = new ExpenseAccountManager();
        public ActionResult Index()
        {
            return View(expenseAccountManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList());
        }

        [HttpPost]
        public JsonResult Create(ExpenseAccount expenseAccount)
        {
            try
            {
                if (expenseAccount.AccountName != null)
                {
                    int result = expenseAccountManager.Insert(expenseAccount);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Gider hesap kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Gider hesap kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Gider hesap kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = expenseAccountManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.AccountName }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(ExpenseAccount expenseAccount)
        {
            try
            {
                if (expenseAccount.AccountName != null)
                {
                    ExpenseAccount ea = expenseAccountManager.Find(x => x.Id == expenseAccount.Id);
                    ea.AccountName = expenseAccount.AccountName;
                    int results = expenseAccountManager.Update(ea);
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
            ExpenseAccount ea = expenseAccountManager.Find(x => x.Id == id.Value);
            if (ea == null)
            {
                return HttpNotFound();
            }

            if (expenseAccountManager.Delete(ea) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(ea);
        }
    }
}
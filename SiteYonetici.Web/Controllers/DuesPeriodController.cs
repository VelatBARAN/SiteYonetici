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
    public class DuesPeriodController : Controller
    {
        private DuesPeriodManager duesPeriodManager = new DuesPeriodManager();
        public ActionResult Index()
        {
            return View(duesPeriodManager.ListQueryable().OrderByDescending(x=>x.CreatedDate).ToList());
        }

        [HttpPost]
        public JsonResult Create(DuesPeriod duesPeriod)
        {
            try
            {
                if (duesPeriod.DuesYear != null && duesPeriod.DuesMonth != null)
                {
                    int result = duesPeriodManager.Insert(duesPeriod);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Dönem kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Dönem kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Dönem kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = duesPeriodManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.DuesYear,x.DuesMonth }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(DuesPeriod duesPeriod)
        {
            try
            {
                if (duesPeriod.DuesYear != null && duesPeriod.DuesMonth != null)
                {
                    DuesPeriod dp = duesPeriodManager.Find(x => x.Id == duesPeriod.Id);
                    dp.DuesYear = duesPeriod.DuesYear;
                    dp.DuesMonth = duesPeriod.DuesMonth;
                    int results = duesPeriodManager.Update(dp);
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
            DuesPeriod dp = duesPeriodManager.Find(x => x.Id == id.Value);
            if (dp == null)
            {
                return HttpNotFound();
            }

            if (duesPeriodManager.Delete(dp) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(dp);
        }
    }
}
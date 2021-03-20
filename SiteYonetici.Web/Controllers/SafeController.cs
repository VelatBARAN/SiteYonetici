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
    public class SafeController : Controller
    {
        private SafeManager safeManager = new SafeManager();
        public ActionResult Index()
        {
            return View(safeManager.List());
        }

        [HttpPost]
        public JsonResult Create(Safe safe)
        {
            try
            {
                if (safe.SafeType != null && safe.Name != null)
                {
                    int result = safeManager.Insert(safe);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Kasa kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }               
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Kasa kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Kasa kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = safeManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.SafeType, x.Name }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var result = safeManager.Find(x => x.Id == id.Value);
            //if (result == null)
            //{
            //    return HttpNotFound();
            //}
            //return PartialView("_PartialEditSafe", result);
        }

        [HttpPost]
        public JsonResult Edit(Safe safe)
        {
            try
            {
                if (safe.SafeType != null && safe.Name != null)
                {
                    Safe s = safeManager.Find(x => x.Id == safe.Id);
                    s.SafeType = safe.SafeType;
                    s.Name = safe.Name;
                    int results = safeManager.Update(s);
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
            Safe s = safeManager.Find(x => x.Id == id.Value);
            if (s == null)
            {
                return HttpNotFound();
            }

            if (safeManager.Delete(s) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(s);
        }
    }
}
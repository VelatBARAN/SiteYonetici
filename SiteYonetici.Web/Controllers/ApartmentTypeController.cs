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
    public class ApartmentTypeController : Controller
    {
        private ApartmentTypeManager apartmentTypeManager = new ApartmentTypeManager();
        public ActionResult Index()
        {
            return View(apartmentTypeManager.List());
        }

        [HttpPost]
        public JsonResult Create(ApartmentType apartmentType)
        {
            try
            {
                if (apartmentType.ApartmentTypeName != null)
                {
                    int result = apartmentTypeManager.Insert(apartmentType);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Daire tipi kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Daire tipi kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Daire tipi kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = apartmentTypeManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.ApartmentTypeName }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(ApartmentType apartmentType)
        {
            try
            {
                if (apartmentType.ApartmentTypeName != null)
                {
                    ApartmentType a = apartmentTypeManager.Find(x => x.Id == apartmentType.Id);
                    a.ApartmentTypeName = apartmentType.ApartmentTypeName;
                    int results = apartmentTypeManager.Update(a);
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
            ApartmentType a = apartmentTypeManager.Find(x => x.Id == id.Value);
            if (a == null)
            {
                return HttpNotFound();
            }

            if (apartmentTypeManager.Delete(a) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(a);
        }
    }
}
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
    public class TaskTypeController : Controller
    {
        private TaskTypeManager taskTypeManager = new TaskTypeManager();
        public ActionResult Index()
        {
            return View(taskTypeManager.List());
        }

        [HttpPost]
        public JsonResult Create(TaskType taskType)
        {
            try
            {
                if (taskType.TaskTypeName != null)
                {
                    int result = taskTypeManager.Insert(taskType);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Görev türü kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Görev türü kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Görev türü kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = taskTypeManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.TaskTypeName }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(TaskType taskType)
        {
            try
            {
                if (taskType.TaskTypeName != null)
                {
                    TaskType t = taskTypeManager.Find(x => x.Id == taskType.Id);
                    t.TaskTypeName = taskType.TaskTypeName;
                    int results = taskTypeManager.Update(t);
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
            TaskType t = taskTypeManager.Find(x => x.Id == id.Value);
            if (t == null)
            {
                return HttpNotFound();
            }

            if (taskTypeManager.Delete(t) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(t);
        }
    }
}
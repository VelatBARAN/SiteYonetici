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
    public class BlockController : Controller
    {
        private BlockManager blockManager = new BlockManager();
        public ActionResult Index()
        {
            return View(blockManager.List()); ;
        }

        [HttpPost]
        public JsonResult Create(Block block)
        {
            try
            {
                if (block.Name != null && block.ApartmentCount.ToString() != null && block.TaskedEmployeeCount.ToString() != null)
                {
                    int result = blockManager.Insert(block);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Blok kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Blok kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Blok kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = blockManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.Name, x.ApartmentCount,x.TaskedEmployeeCount,x.Description }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(Block block)
        {
            try
            {
                if (block.Name != null && block.ApartmentCount.ToString() != "0" && block.TaskedEmployeeCount.ToString() != "0")
                {
                    Block b = blockManager.Find(x => x.Id == block.Id);
                    b.Name = block.Name;
                    b.ApartmentCount = block.ApartmentCount;
                    b.TaskedEmployeeCount = block.TaskedEmployeeCount;
                    b.Description = block.Description;
                    int results = blockManager.Update(b);
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
            Block b = blockManager.Find(x => x.Id == id.Value);
            if (b == null)
            {
                return HttpNotFound();
            }

            if (blockManager.Delete(b) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(b);
        }
    }
}
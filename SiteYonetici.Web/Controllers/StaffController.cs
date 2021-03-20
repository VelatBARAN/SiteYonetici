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
    public class StaffController : Controller
    {
        private StaffManager staffManager = new StaffManager();
        private TaskTypeManager taskTypeManager = new TaskTypeManager();
        public ActionResult Index()
        {
            return View(staffManager.ListQueryable().OrderByDescending(x=>x.CreatedDate).ToList());
        }

        [HttpGet]
        public ActionResult ListByStaffId(int? taskTypeId)
        {
            return View("Index", staffManager.List().Where(x => x.TaskType.Id == taskTypeId.Value));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TaskType = new SelectList(taskTypeManager.List(), "Id", "TaskTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                staff.IsActive = true;
                if (staffManager.Insert(staff) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(staff);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = staffManager.Find(x => x.Id == id.Value);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskType = new SelectList(taskTypeManager.List(), "Id", "TaskTypeName", staff.TaskTypeId);
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                Staff s = staffManager.Find(x => x.Id == staff.Id);
                s.Name = staff.Name;
                s.Surname = staff.Surname;
                s.TC = staff.TC;
                s.Phone = staff.Phone;
                s.Salary = staff.Salary;
                s.InsuranceNo = staff.InsuranceNo;
                s.JobDefinition = staff.JobDefinition;
                s.TaskTypeId = staff.TaskTypeId;
                s.IsActive = staff.IsActive;

                if (staffManager.Update(s) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(staff);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Staff staff = staffManager.Find(x => x.Id == id.Value);
            if (staff != null)
            {
                int res = staffManager.Delete(staff);
                if (res > 0)
                {
                    //CacheHelper.RemoveGetPersonnelsFromCache();
                    //CacheHelper.RemoveGetActivePersonnelsFromCache();
                    return Json(new { hasError = false, Message = "Kayıt başarılı bir şekilde silindi." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { hasError = true, Message = "Kayıt silinirken hata oluştu." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { hasError = true, Message = "Böyle bir kayıt bulunamadı." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = staffManager.Find(x => x.Id == id.Value);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialStaffDetail", staff);
        }
    }
}
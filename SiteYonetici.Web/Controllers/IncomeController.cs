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
    public class IncomeController : Controller
    {
        private IncomeManager incomeManager = new IncomeManager();
        private IncomeAccountManager incomeAccountManager = new IncomeAccountManager();
        private SafeManager safeManager = new SafeManager();
        public ActionResult Index()
        {
            return View(incomeManager.ListQueryable().OrderByDescending(x=>x.CreatedDate).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> safeList = (from c in safeManager.List()
                                             select new SelectListItem
                                             {
                                                 Text = c.SafeType,
                                                 Value = c.Id.ToString()
                                             }).ToList();
            ViewBag.IncomeAccount = new SelectList(incomeAccountManager.List(), "Id", "AccountName");
            ViewBag.Safe = safeList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Income income)
        {
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                if (incomeManager.Insert(income) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(income);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = incomeManager.Find(x => x.Id == id.Value);
            if (income == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> safeList = (from c in safeManager.List()
                                             select new SelectListItem
                                             {
                                                 Text = c.SafeType,
                                                 Value = c.Id.ToString()
                                             }).ToList();
            ViewBag.IncomeAccount = new SelectList(incomeAccountManager.List(), "Id", "AccountName",income.SafeId);
            ViewBag.Safe = safeList;
            return View(income);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Income income)
        {
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                Income i = incomeManager.Find(x => x.Id == income.Id);
                i.SafeId = income.SafeId;
                i.IncomeAccountId = income.IncomeAccountId;
                i.Amount = income.Amount;
                i.Description = income.Description;
                if (incomeManager.Update(i) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(income);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Income income = incomeManager.Find(x => x.Id == id.Value);
            if (income != null)
            {
                int res = incomeManager.Delete(income);
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
            Income income = incomeManager.Find(x => x.Id == id.Value);
            if (income == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialIncomeDetail", income);
        }
    }
}
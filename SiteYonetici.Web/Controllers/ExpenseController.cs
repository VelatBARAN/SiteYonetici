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
    public class ExpenseController : Controller
    {
        private ExpenseManager expenseManager = new ExpenseManager();
        private ClientManager clientManager = new ClientManager();
        private ExpenseAccountManager expenseAccountManager = new ExpenseAccountManager();
        private SafeManager safeManager = new SafeManager();
        public ActionResult Index()
        {
            return View(expenseManager.ListQueryable().OrderByDescending(x=>x.CreatedDate).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> clientList = (from c in clientManager.List()
                                               select new SelectListItem
                                               {
                                                   Text = c.Name,
                                                   Value = c.Id.ToString()
                                               }).ToList();
            List<SelectListItem> safeList = (from c in safeManager.List()
                                               select new SelectListItem
                                               {
                                                   Text = c.SafeType,
                                                   Value = c.Id.ToString()
                                               }).ToList();
            ViewBag.Client = clientList;
            ViewBag.ExpenseAccount = new SelectList(expenseAccountManager.List(), "Id","AccountName");
            ViewBag.Safe = safeList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expense expense)
        {
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                if (expense.IsDues == true)
                    expense.IsDues = true;
                else
                    expense.IsDues = false;
                if (expenseManager.Insert(expense) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(expense);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = expenseManager.Find(x => x.Id == id.Value);
            if (expense == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> clientList = (from c in clientManager.List()
                                               select new SelectListItem
                                               {
                                                   Text = c.Name,
                                                   Value = c.Id.ToString()
                                               }).ToList();
            List<SelectListItem> safeList = (from c in safeManager.List()
                                             select new SelectListItem
                                             {
                                                 Text = c.SafeType,
                                                 Value = c.Id.ToString()
                                             }).ToList();
            ViewBag.Client = clientList;
            ViewBag.ExpenseAccount = new SelectList(expenseAccountManager.List(), "Id", "AccountName",expense.ExpenseAccountId);
            ViewBag.Safe = safeList;
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Expense expense)
        {
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                Expense e = expenseManager.Find(x => x.Id == expense.Id);
                e.ClientId = expense.ClientId;
                e.SafeId = expense.SafeId;
                e.ExpenseAccountId = expense.ExpenseAccountId;
                e.Amount = expense.Amount;
                e.Description = expense.Description;
                e.IsDues  = expense.IsDues;
                if (expenseManager.Update(e) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(expense);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Expense expense = expenseManager.Find(x => x.Id == id.Value);
            if (expense != null)
            {
                int res = expenseManager.Delete(expense);
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
            Expense expense = expenseManager.Find(x => x.Id == id.Value);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialExpenseDetail", expense);
        }
    }
}
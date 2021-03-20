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
    public class DuesController : Controller
    {
        private SafeManager safeManager = new SafeManager();
        private DuesPayment duesPayment = new DuesPayment();
        private DuesManager duesManager = new DuesManager();
        private BlockManager blockManager = new BlockManager();
        private DuesPeriodManager duesPeriodManager = new DuesPeriodManager();
        private ExpenseManager expenseManager = new ExpenseManager();
        private ApartmentManager apartmentManager = new ApartmentManager();
        private DuesPaymentManager duesPaymentManager = new DuesPaymentManager();
        private DuesPaymentDetailsManager duesPaymentDetailsManager = new DuesPaymentDetailsManager();
        private decimal totalNotDuesAmount = 0, totalDuesAmount = 0;

        [HttpGet]
        public ActionResult DuesDelivery()
        {
            List<SelectListItem> listDuesPeriod = (from k in duesPeriodManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList()
                                                   select new SelectListItem()
                                                   {
                                                       Text = k.DuesYear + "-" + k.DuesMonth,
                                                       Value = k.Id.ToString()
                                                   }).ToList();
            ViewBag.DuesPeriod = listDuesPeriod;

            totalNotDuesAmount = expenseManager.ListQueryable()
                                     .Where(x => x.CreatedDate.Month + "-" + x.CreatedDate.Year == DateTime.Now.Month + "-" + DateTime.Now.Year && x.IsDues == false)
                                     .ToList()
                                     .Sum(x => x.Amount);
            ViewBag.TotalNotDuesAmount = totalNotDuesAmount;

            totalDuesAmount = expenseManager.ListQueryable()
                                     .Where(x => x.CreatedDate.Month + "-" + x.CreatedDate.Year == DateTime.Now.Month + "-" + DateTime.Now.Year && x.IsDues == true)
                                     .ToList()
                                     .Sum(x => x.Amount);
            ViewBag.TotalDuesAmount = totalDuesAmount;

            ViewBag.TotalExpense = totalNotDuesAmount + totalDuesAmount;

            ViewBag.Category = "Aidat";
            ViewBag.DuesDeliveryWay = "Bloklara Göre Dağıtım";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DuesDelivery(Dues dues)
        {
            if (ModelState.IsValid)
            {
                if (duesManager.Insert(dues) > 0)
                {
                    List<Block> blockList = blockManager.List();
                    decimal thisMonthTotalDues_Block = dues.ThisMonthTotalDues / blockList.Count();
                    int count = 0;
                    for (int i = 0; i < blockList.Count(); i++)
                    {
                        List<Apartment> apartmentList = apartmentManager.List().Where(x => x.BlockId == blockList[i].Id).ToList();
                        for (int j = 0; j < apartmentList.Count(); j++)
                        {
                            decimal thisMonthTotalDues_Apartment = thisMonthTotalDues_Block / apartmentList.Count();

                            duesPayment.DuesPeriodId = dues.DuesPeriodId;
                            duesPayment.LastPaymentDate = dues.LastPaymentDate;
                            duesPayment.ApartmentId = apartmentList[j].Id;
                            duesPayment.NotDuesAmount = (dues.NotDuesAmount / apartmentList.Count);
                            duesPayment.TotalDuesAmount = thisMonthTotalDues_Apartment;
                            duesPayment.PaidAmount = 0;
                            duesPayment.RemainingAmount = thisMonthTotalDues_Apartment;
                            duesPayment.IsPaid = false;
                            if (duesPaymentManager.Insert(duesPayment) > 0)
                            {
                                count++;
                            }
                            else
                            {
                                TempData["DuesDeliveryInfo"] = "0";
                            }
                        }
                    }

                    if (count == apartmentManager.List().Count())
                        TempData["DuesDeliveryInfo"] = "1";
                    else if (count == 0)
                        TempData["DuesDeliveryInfo"] = "0";
                    else
                        TempData["DuesDeliveryInfo"] = "2";
                    return RedirectToAction("DuesDelivery");

                }
                else
                {
                    TempData["DuesDeliveryInfo"] = "0";
                    return RedirectToAction("DuesDelivery");
                }
            }
            return View(dues);
        }

        public ActionResult DuesPaymentList()
        {
            return View(duesPaymentManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList());
        }

        [HttpGet]
        public ActionResult DuesPayment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DuesPayment dpm = duesPaymentManager.Find(x => x.Id == id.Value);
            if (dpm == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> safeType = (from k in safeManager.List()
                                             select new SelectListItem()
                                             {
                                                 Text = k.SafeType,
                                                 Value = k.Id.ToString()
                                             }).ToList();
            ViewBag.SafeType = safeType;
            return PartialView("_PartialDuesPayment", dpm);
        }

        [HttpPost]
        public JsonResult DuesPayment(DuesPaymentDetails duesPaymentDetails)
        {
            DuesPayment duesPayment = duesPaymentManager.Find(x => x.Id == duesPaymentDetails.DuesPaymentId);
            decimal paidAmountPaymentDetails = duesPaymentDetails.PaidAmount;
            decimal paidAmountPayment = duesPayment.PaidAmount;
            decimal totalDuesAmount = duesPayment.TotalDuesAmount;
            decimal remaingAmount = duesPayment.RemainingAmount;

            if (ModelState.IsValid)
            {
                if (duesPaymentDetails.PaidAmount > 0)
                {
                    if (paidAmountPaymentDetails > remaingAmount)
                    {
                        return Json(new { hasError = true, Message = "Girilen tutar kalan tutardan büyüktür" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (paidAmountPaymentDetails <= remaingAmount)
                    {
                        remaingAmount -= paidAmountPaymentDetails;
                        paidAmountPayment += paidAmountPaymentDetails;
                        duesPayment.PaidAmount = paidAmountPayment;
                        duesPayment.RemainingAmount = remaingAmount;

                        if (remaingAmount == 0)
                        {
                            duesPayment.IsPaid = true;
                            if (duesPaymentManager.Update(duesPayment) > 0 && duesPaymentDetailsManager.Insert(duesPaymentDetails) > 0)
                            {
                                return Json(new { hasError = false, Message = "Aidatınızı tamamen ödediniz. Teşekkürler" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else if (totalDuesAmount != paidAmountPayment)
                        {
                            if (duesPaymentManager.Update(duesPayment) > 0 && duesPaymentDetailsManager.Insert(duesPaymentDetails) > 0)
                            {
                                return Json(new { hasError = false, Message = "Aidatınızı kısmi olarak ödediniz. Teşekkürler" }, JsonRequestBehavior.AllowGet);
                            }
                        }

                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen Sıfır(0) dan büyük miktar giriniz." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun." }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult DuesPaymentShowById(int? id)
        //{
        //    DuesPayment duesPayment = duesPaymentManager.Find(x => x.Id == id.Value);
        //    if (duesPayment.IsPaid)
        //    {
        //        return Json(new { hasError = false, Message = "Bu dönem aidat borcunuz bulunmamaktadır." }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { hasError = true,Message=$"Bu döneme ait " +duesPayment.RemainingAmount+ " TL aidat borcunuz bulunmaktadır." , _id=id.Value}, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult DuesPaymentDetails()
        {
            return View(duesPaymentDetailsManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList());
        }
    }
}
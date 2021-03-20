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
    public class ApartmentController : Controller
    {
        private ApartmentManager apartmentManager = new ApartmentManager();
        private ApartmentTypeManager apartmentTypeManager = new ApartmentTypeManager();
        private BlockManager blockManager = new BlockManager();
        public ActionResult Index()
        {
            return View(apartmentManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ApartmentType = new SelectList(apartmentTypeManager.List(), "Id", "ApartmentTypeName");
            List<SelectListItem> blockList = (from b in blockManager.List()
                                              select new SelectListItem
                                              {
                                                  Text = b.Name,
                                                  Value = b.Id.ToString()
                                              }).ToList();
            ViewBag.Block = blockList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Apartment apartment)
        {
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                if (apartmentManager.Insert(apartment) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(apartment);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = apartmentManager.Find(x => x.Id == id.Value);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentType = new SelectList(apartmentTypeManager.List(), "Id", "ApartmentTypeName", apartment.ApartmentTypeId);
            List<SelectListItem> blockList = (from b in blockManager.List()
                                              select new SelectListItem
                                              {
                                                  Text = b.Name,
                                                  Value = b.Id.ToString()
                                              }).ToList();
            ViewBag.Block = blockList;
            return View(apartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Apartment apartment)
        {
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("SavedUsername");
            if (ModelState.IsValid)
            {
                Apartment a = apartmentManager.Find(x => x.Id == apartment.Id);
                a.Floor = apartment.Floor;
                a.ApartmentNo = apartment.ApartmentNo;
                a.NameOfOwner = apartment.NameOfOwner;
                a.SurnameOfOwner = apartment.SurnameOfOwner;
                a.PhoneOfOwner = apartment.PhoneOfOwner;
                a.NameOfHirer = apartment.NameOfHirer;
                a.SurnameOfHirer = apartment.SurnameOfHirer;
                a.PhoneOfHirer = apartment.PhoneOfHirer;
                a.ApartmentTypeId = apartment.ApartmentTypeId;
                a.BlockId = apartment.BlockId;
                if (apartmentManager.Update(a) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(apartment);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Apartment apartment = apartmentManager.Find(x => x.Id == id.Value);
            if (apartment != null)
            {
                int res = apartmentManager.Delete(apartment);
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
            Apartment apartment = apartmentManager.Find(x => x.Id == id.Value);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialApartmentDetail", apartment);
        }

    }
}
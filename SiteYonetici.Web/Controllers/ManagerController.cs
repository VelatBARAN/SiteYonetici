using SiteYonetici.BusinessLayer;
using SiteYonetici.BusinessLayer.Result;
using SiteYonetici.Entities;
using SiteYonetici.Entities.ValueObjects;
using SiteYonetici.Web.Filter;
using SiteYonetici.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SiteYonetici.Web.Controllers
{
    public class ManagerController : Controller
    {
        private ManagersManager managersManager = new ManagersManager();
        private ResetPasswordModel resetPasswordModel = new ResetPasswordModel();

        [Auth]
        public ActionResult Index()
        {
            return View(managersManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList());
        }

        [Auth]
        [HttpPost]
        public JsonResult Create(Managers managers)
        {
            try
            {
                if (string.IsNullOrEmpty(managers.Name) == false && string.IsNullOrEmpty(managers.Surname) == false && string.IsNullOrEmpty(managers.TC) == false &&
                    string.IsNullOrEmpty(managers.Phone) == false && string.IsNullOrEmpty(managers.Eposta) == false && string.IsNullOrEmpty(managers.Password) == false)
                {
                    var pass = Crypto.Hash(managers.Password.ToString(), "MD5");
                    managers.Password = pass;
                    managers.IsActive = true;

                    Managers m = managersManager.List().Where(x => x.TC == managers.TC || x.Eposta == managers.Eposta).FirstOrDefault();
                    if (m != null)
                    {
                        return Json(new { hasError = false, Message = "Aynı TC No veya Eposta zaten kayıtlıdır." }, JsonRequestBehavior.AllowGet);
                    }

                    int result = managersManager.Insert(managers);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Yönetici kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Yönetici kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Yönetici kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = managersManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.Name, x.Surname, x.TC, x.Phone,x.Eposta, x.IsActive }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Auth]
        [HttpPost]
        public JsonResult Edit(Managers managers)
        {
            try
            {
                if (string.IsNullOrEmpty(managers.Name) == false && string.IsNullOrEmpty(managers.Surname) == false && string.IsNullOrEmpty(managers.TC) == false &&
                    string.IsNullOrEmpty(managers.Phone) == false && string.IsNullOrEmpty(managers.Eposta) == false)
                {
                    Managers m = managersManager.List().Where(x => x.TC == managers.TC || x.Eposta == managers.Eposta).FirstOrDefault();
                    if (m != null && m.Id != managers.Id)
                    {
                        return Json(new { hasError = false, Message = "Aynı TC No veya Eposta zaten kayıtlıdır." }, JsonRequestBehavior.AllowGet);
                    }
                    m.Name = managers.Name;
                    m.Surname = managers.Surname;
                    m.TC = managers.TC;
                    m.Phone = managers.Phone;
                    m.Eposta = managers.Eposta;
                    m.IsActive = managers.IsActive;
                    if (string.IsNullOrEmpty(managers.Password) == false)
                        m.Password = Crypto.Hash(managers.Password.ToString(), "MD5");
                    int results = managersManager.Update(m);
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

        [Auth]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Managers m = managersManager.Find(x => x.Id == id.Value);
            if (m == null)
            {
                return HttpNotFound();
            }

            if (managersManager.Delete(m) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(m);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Managers> res = managersManager.Login(loginViewModel);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(loginViewModel);
                }

                CurrentSession.Set<Managers>("login", res.Result);
                return RedirectToAction("Index", "Home");
            }

            return View(loginViewModel);
        }

        public ActionResult LogOut()
        {
            CurrentSession.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult ShowProfile()
        {
            Managers manager = managersManager.Find(x => x.Eposta == CurrentSession.Manager.Eposta);
            return View("ShowProfile", manager);
        }

        public ActionResult ShowProfileDetail(int? id)
        {
            Managers manager = managersManager.Find(x => x.Id == id.Value);
            return PartialView("_PartialProfileDetail", manager);
        }

        [HttpGet]
        public ActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordChange(ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Managers> res = managersManager.PasswordChange(resetPasswordModel);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(resetPasswordModel);
                }
                ViewBag.ChangePassword = "Şifreniz başarılı bir şekilde değiştirilmiştir";
            }

            return View(resetPasswordModel);
        }

        [HttpGet]
        public ActionResult ProfileDelete()
        {
            Managers m = managersManager.Find(x => x.Eposta == CurrentSession.Manager.Eposta);
            if (m == null)
            {
                return HttpNotFound();
            }

            m.IsActive = false;
            if (managersManager.Update(m) > 0)
            {
                CurrentSession.Clear();
                return RedirectToAction("Index");
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Managers> res = managersManager.PasswordForget(forgetPasswordModel);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(forgetPasswordModel);
                }
                ViewBag.ForgetPassword = "Şifre gönderme işlemi başarıyla gerçekleşti. Lütfen eposta adresinizi kontrol ediniz..";
                return View(forgetPasswordModel);
            }

            return View(forgetPasswordModel);
        }
    }
}
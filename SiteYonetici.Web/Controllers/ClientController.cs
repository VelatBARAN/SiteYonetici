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
    public class ClientController : Controller
    {
        private ClientManager clientManager = new ClientManager();
        public ActionResult Index()
        {
            return View(clientManager.ListQueryable().OrderByDescending(x=>x.CreatedDate).ToList());
        }

        [HttpPost]
        public JsonResult Create(Client client)
        {
            try
            {
                if (client.Name != null && client.AccountOrIbanNo != null)
                {
                    int result = clientManager.Insert(client);
                    if (result > 0)
                    {
                        return Json(new { hasError = false, Message = "Cari kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Cari kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, Message = "Cari kaydı yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int? id)
        {
            try
            {
                var result = clientManager.List().Where(x => x.Id == id.Value).Select(x => new { x.Id, x.Name, x.TaxNo,x.TaxOffice,x.AccountOrIbanNo }).FirstOrDefault();
                return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Herhangi bir kayıt bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(Client client)
        {
            try
            {
                if (client.Name != null && client.AccountOrIbanNo != null)
                {
                    Client c = clientManager.Find(x => x.Id == client.Id);
                    c.Name = client.Name;
                    c.TaxNo = client.TaxNo;
                    c.TaxOffice = client.TaxOffice;
                    c.AccountOrIbanNo = client.AccountOrIbanNo;
                    int results = clientManager.Update(c);
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
            Client c = clientManager.Find(x => x.Id == id.Value);
            if (c == null)
            {
                return HttpNotFound();
            }

            if (clientManager.Delete(c) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(c);
        }
    }
}
using SiteYonetici.Common.GetUsername;
using SiteYonetici.Entities;
using SiteYonetici.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteYonetici.Web.InitGetUsername
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            Managers manager = CurrentSession.Manager;
            if (manager != null)
                return manager.Eposta;
            else
                return "system";
        }
    }
}
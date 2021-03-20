using SiteYonetici.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Managers managers = new Managers()
            {
                Name = "Welat",
                Surname = "BARAN",
                TC="41950077384",
                Phone = "5393711268",
                Eposta = "baranvelat021@gmail.com",
                IsActive = true,
                Password = "liceli21",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddMinutes(5),
                SavedUsername = "welatbaran"
            };

            context.Managers.Add(managers);
            context.SaveChanges();
        }
    }
}

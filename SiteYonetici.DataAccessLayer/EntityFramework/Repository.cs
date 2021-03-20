using SiteYonetici.Common.GetUsername;
using SiteYonetici.Core.IRepository;
using SiteYonetici.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        private DbSet<T> _db;

        public Repository()
        {
            _db = context.Set<T>();
        }

        public int Delete(T obj)
        {
            _db.Remove(obj);
            return Save();
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _db.FirstOrDefault(where);
        }

        public int Insert(T obj)
        {
            _db.Add(obj);
            if (obj is MyEntitiyBase)
            {
                MyEntitiyBase meb = obj as MyEntitiyBase;
                meb.CreatedDate = DateTime.Now;
                meb.ModifiedDate = DateTime.Now;
                meb.SavedUsername = App.Common.GetUsername();
            }
            return Save();
        }

        public List<T> List(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _db.Where(where).ToList();
        }

        public List<T> List()
        {
            return _db.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _db.AsQueryable<T>();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public int Update(T obj)
        {
            if (obj is MyEntitiyBase)
            {
                MyEntitiyBase meb = obj as MyEntitiyBase;
                meb.ModifiedDate = DateTime.Now;
                meb.SavedUsername = App.Common.GetUsername();
            }
            return Save();
        }
    }
}

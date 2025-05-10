using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.DataAccess;

namespace TeknikServis.Business.Abstract
{

    public class Repository<T> : IRepository<T> where T : class
        {
        protected readonly TeknikServisDbContext _context;
       
        public Repository(TeknikServisDbContext context)
        {
            _context = context;
        }
  //      public void Edit(T entity)
		//{
		//	_context.Entry(entity).State = EntityState.Modified;
		//	_context.SaveChanges();
		//}
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
             _context.SaveChanges(); 
        }
        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {

            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                return query.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
           
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public List<T> List()
        {
            return _context.Set<T>().ToList();
        }
        public void Save()
        {
			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				// Hata günlüğü tutma veya daha fazla işleme burada yapılabilir.
				throw new Exception("Veri kaydedilirken bir hata oluştu.", ex);
			}
		}
        public void Update(T entity)
		{
			Console.WriteLine("🟢 [REPO] Güncelleme çağrıldı: " + entity.ToString());

			_context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }


    }
}

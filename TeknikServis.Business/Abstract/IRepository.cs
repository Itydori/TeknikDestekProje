using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Entities.Servis;

namespace TeknikServis.Business.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
	}
}

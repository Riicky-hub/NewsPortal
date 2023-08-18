using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category in this case
        IEnumerable<T> GetAll(bool track = true);
        T Get(Expression<Func<T, bool>> filter); // General code to get the link expression type(In this case, firstOrDefault())
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}

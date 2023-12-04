using HamburgerWebApp.Entity.Abstract;
using System.Linq.Expressions;

namespace HamburgerWebApp.BLL.Abstract;

public interface IBaseService<T> where T : class, IBaseEntity
{
    //CRUD
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
}
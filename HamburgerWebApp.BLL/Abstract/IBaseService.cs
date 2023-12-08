using HamburgerWebApp.BLL.ResponsePattern;
using HamburgerWebApp.Entity.Abstract;
using System.Linq.Expressions;

namespace HamburgerWebApp.BLL.Abstract;

public interface IBaseService<T> where T : class, IBaseEntity
{
    Task<Response> Add(T entity);
    Task<Response> Update(T entity);
    Task<Response> Delete(T entity);
    Task<Response<T>> GetById(int id);
    Task<Response<IEnumerable<T>>> GetAll();
    Task<Response<IEnumerable<T>>> GetAll(Expression<Func<T, bool>> predicate);
}
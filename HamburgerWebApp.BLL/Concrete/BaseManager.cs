using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Abstract;
using System.Linq.Expressions;

namespace HamburgerWebApp.BLL.Concrete;

public class BaseManager<T> : IBaseService<T> where T : class, IBaseEntity
{
    protected readonly IBaseRepository<T> baseRepository;

    public BaseManager(IBaseRepository<T> baseRepository)
    {
        this.baseRepository = baseRepository;
    }

    public async Task<T> Add(T entity)
    {
        return await baseRepository.Add(entity);
    }

    public async Task<T> Delete(T entity)
    {
        return await baseRepository.Delete(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await baseRepository.GetAll();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
    {
        return await baseRepository.GetAll(predicate);
    }

    public async Task<T> GetById(int id)
    {
        return await baseRepository.GetById(id);
    }

    public virtual async Task<T> Update(T entity)
    {
        return await baseRepository.Update(entity);
    }
}
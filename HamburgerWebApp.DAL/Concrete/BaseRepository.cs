using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.DAL.Context;
using HamburgerWebApp.Entity.Abstract;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HamburgerWebApp.DAL.Concrete;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetById(int id)
    {
        var query = _context.Set<T>().AsQueryable();

        var navigationProperties = _context.Model.FindEntityType(typeof(T)).GetNavigations();
        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty.Name);
        }

        if (typeof(T) == typeof(Order))
        {
            query = query.Include("Extras");
        }
        return await query.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var query = _context.Set<T>().AsQueryable();

        var navigationProperties = _context.Model.FindEntityType(typeof(T)).GetNavigations();
        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty.Name);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
    {
        var query = _context.Set<T>().AsQueryable();

        var navigationProperties = _context.Model.FindEntityType(typeof(T)).GetNavigations();
        foreach (var navigationProperty in navigationProperties)
        {
            query = query.Include(navigationProperty.Name);
        }

        return await query.Where(predicate).ToListAsync();
    }
}

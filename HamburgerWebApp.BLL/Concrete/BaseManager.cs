using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.BLL.ResponsePattern;
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

    public async Task<Response> Add(T entity)
    {
        try
        {
            await baseRepository.Add(entity);
            return Response.Success("Insert Success");
        }
        catch (Exception e)
        {
            return Response.Failure("Insert Failed");
        }
    }

    public async Task<Response> Delete(T entity)
    {
        try
        {
            await baseRepository.Delete(entity);
            return Response.Success("Delete Success");
        }
        catch (Exception e)
        {
            return Response.Failure("Delete Failed");
        }
    }

    public async Task<Response<IEnumerable<T>>> GetAll()
    {
        try
        {
            var entities = await baseRepository.GetAll();
            return Response<IEnumerable<T>>.Success(entities, "Get All Success");

        }
        catch (Exception e)
        {
            return Response<IEnumerable<T>>.Failure("Get All Failed");
        }
    }

    public async Task<Response<IEnumerable<T>>> GetAll(Expression<Func<T, bool>> predicate)
    {
        try
        {
            var entities = await baseRepository.GetAll(predicate);
            return Response<IEnumerable<T>>.Success(entities, "Get All Success");

        }
        catch (Exception e)
        {
            return Response<IEnumerable<T>>.Failure("Get All Failed");
        }
    }

    public async Task<Response<T>> GetById(int id)
    {
        try
        {
            var entity = await baseRepository.GetById(id);
            return Response<T>.Success(entity, "Get By Id Success");
        }
        catch (Exception e)
        {
            return Response<T>.Failure("Get By Id Failed");
        }
    }

    public async Task<Response> Update(T entity)
    {
        try
        {
            await baseRepository.Update(entity);
            return Response.Success("Update Success");
        }
        catch (Exception e)
        {
            return Response.Failure("Update Failed");
        }
    }
}
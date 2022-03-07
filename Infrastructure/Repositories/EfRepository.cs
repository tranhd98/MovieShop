using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EfRepository<T> : IRespository<T> where T: class
{
    protected readonly MovieShopDbContext _dbContext;

    
    public EfRepository(MovieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public virtual T GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T Add(T entity)
    {
        throw new NotImplementedException();
    }

    public T Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public T Update(T entity)
    {
        throw new NotImplementedException();
    }
}
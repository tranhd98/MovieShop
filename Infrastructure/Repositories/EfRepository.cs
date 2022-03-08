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
    
    public virtual async Task<T> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<T> Add(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }
}
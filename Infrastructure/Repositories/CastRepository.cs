using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository: EfRepository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
        
    }

    public override Cast GetById(int id)
    {
        var cast = _dbContext.Casts
            .Include(c => c.MovieCasts).ThenInclude(mc => mc.Movie)
            .FirstOrDefault(c => c.Id == id);
        return cast;
    }
}
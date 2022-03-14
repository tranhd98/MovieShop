using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IFavoriteRepository: IRespository<Favorite>
{
    Task<Favorite> GetFavoriteByUser(FavoriteRequestModel model);

    Task<IEnumerable<Favorite>> GetAllFavoriteByUser(int id);
}
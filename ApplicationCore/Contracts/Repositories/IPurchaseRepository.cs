using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository : IRespository<Purchase>
{
    Task<Purchase> GetPurchaseByUser(int movieId, int userId);
    Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int id);
}
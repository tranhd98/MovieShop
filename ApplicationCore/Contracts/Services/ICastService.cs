using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface ICastService
{
    Task<CastDetailsModel> GetCastDetails(int id);
}
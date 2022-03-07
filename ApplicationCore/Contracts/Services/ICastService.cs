using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface ICastService
{
    CastDetailsModel GetCastDetails(int id);
}
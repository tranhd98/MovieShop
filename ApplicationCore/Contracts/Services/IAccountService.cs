using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAccountService
{
    Task<bool> ValidateUser(UserLoginRequestModel model);
    Task<int> CreateUser(UserRegisterRequestModel model);
}
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IUserRepository: IRespository<User>
{
    Task<User>GetUserByEmails(string email);
}
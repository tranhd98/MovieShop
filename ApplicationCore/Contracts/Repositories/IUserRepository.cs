using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IUserRepository: IRespository<User>
{
    Task<User>GetUserByEmails(string email);
}
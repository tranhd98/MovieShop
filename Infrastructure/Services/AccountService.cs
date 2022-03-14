using System.Security.Cryptography;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> ValidateUser(UserLoginRequestModel model)
    {
        
        var user = await _userRepository.GetUserByEmails(model.Email);
        if (user == null)
        {
            return null;
        }
        if (hashed(model.Password, user.Salt) != user.HashedPassword)
        {
            return null;
        }

        var userLoginResponse = new LoginResponse
        {
            Email = user.Email,
            Id = user.Id,
            DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
            FirstName = user.FirstName,
            LastName = user.LastName
        };
        return userLoginResponse;
    }

    public async Task<int> CreateUser(UserRegisterRequestModel model)
    {
        // check if email already existed or not
        var user = await _userRepository.GetUserByEmails(model.Email);
        if (user != null)
        {
            throw new Exception("Already existed");
        }

        string salt = GenerateSalt();
        string hashedPassword = hashed(model.Password, salt);
        var userWithHashed = new User
        {
            FirstName = model.FirstName,
            Salt = salt,
            HashedPassword = hashedPassword,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth,
            LastName = model.LastName
        };
        var createdUser = await _userRepository.Add(userWithHashed);
        return createdUser.Id;
    }

    private string GenerateSalt()
    {
        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }

        return Convert.ToBase64String(salt);
    }

    private string hashed(string password, string salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hashed;
    }
}
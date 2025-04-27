using System.Runtime.CompilerServices;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistance;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public AuthenticationResult Login(string email, string password)
    {
        // Check if user exists
        if (_userRepository.GetByEmail(email) is not User exuser) {
            throw new Exception("User does not exist");
        }

        var user = _userRepository.GetByEmail(email);

        if (user == null) {
            throw new Exception("User does not exist");
        }

        // Check if password is correct
        if (user.Password != password) {
            throw new Exception("Invalid password");
        }

        // Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        if (token == null) {
            throw new Exception("Failed to generate token");
        }
        return new AuthenticationResult(
            user,
            token
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if user exists
        if (_userRepository.GetByEmail(email) is not null) {
            throw new Exception("User already exists");
        }

        // Create user (generate unique id)
        var user = new User {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);

        // Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        if (token == null) {
            throw new Exception("Failed to generate token");
        }
        return new AuthenticationResult(
            user,
            token
        );
    }
}

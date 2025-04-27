using BuberDinner.Domain.Entities;
using BuberDinner.Application.Common.Interfaces.Persistance;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new List<User>();

    public User? GetByEmail(string email)
    {
        return _users.SingleOrDefault(user => user.Email == email);
    }

    public void Add(User user)
    {
        if (_users.Any(u => u.Email == user.Email))
        {
            throw new Exception("User already exists");
        }

        _users.Add(user);
    }
}

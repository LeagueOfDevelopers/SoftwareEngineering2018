using System;
using System.Collections.Generic;
using LeagueGram.Domain;
using LeagueGram.Domain.Exception;

namespace LeagueGram.Infrastructure
{
  public class InMemoryUserRepository : IUserRepository
  {
    public User LoadUser(Guid userId)
    {
      if (!_users.ContainsKey(userId))
      {
        throw new UserNotFoundException(userId);
      }

      return _users[userId];
    }

    public void SaveUser(User user)
    {
      _users[user.Id] = user;
    }

    private readonly Dictionary<Guid, User> _users = new Dictionary<Guid, User>();
  }
}
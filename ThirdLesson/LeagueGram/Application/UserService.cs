using System;
using LeagueGram.Domain;

namespace LeagueGram.Application
{
  public class UserService : IUserService
  {
    public UserService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public Guid RegisterUser(string nickname)
    {
      var userId = Guid.NewGuid();
      var user = new User(userId, nickname, DateTimeOffset.UtcNow);
      _userRepository.SaveUser(user);
      return userId;
    }

    private readonly IUserRepository _userRepository;
  }
}
using System;

namespace LeagueGram.Domain
{
  public interface IUserRepository
  {
    User LoadUser(Guid userId);
    void SaveUser(User user);
  }
}
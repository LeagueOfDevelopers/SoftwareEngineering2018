using System;

namespace LeagueGram.Application
{
  public interface IUserService
  {
    Guid RegisterUser(string nickname);
  }
}
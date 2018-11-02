using LeagueGram.Infrastructure;
using System;

namespace LeagueGram.Domain
{
  public class User
  {
    public User(Guid id, string nickname, DateTimeOffset registrationDate)
    {
      Id = id;
      Nickname = nickname;
      RegistrationDate = registrationDate;
    }

    public Guid Id { get; }
    public string Nickname { get; }
	public Option<string> AvatarUrl { get; }
    public DateTimeOffset RegistrationDate { get; }
  }
}
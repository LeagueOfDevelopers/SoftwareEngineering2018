using System;
using LeagueGram.Domain;
using LeagueGram.Infrastructure;

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
		public void Test()
		{
			var chatRepository = new InMemoryRepository<Chat>();
			var userRepository = new InMemoryRepository<User>();
			var chats = chatRepository.GetAll();
			var users = userRepository.GetAll();
			var user = userRepository.Load(Guid.NewGuid());
			if (user.AvatarUrl.HasValue)
			{
				user.AvatarUrl.Value.Split();
			}
		}
    private readonly IUserRepository _userRepository;
  }
}
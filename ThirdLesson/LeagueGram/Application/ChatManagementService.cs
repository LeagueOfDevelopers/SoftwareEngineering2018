using System;
using LeagueGram.Domain;

namespace LeagueGram.Application
{
  public class ChatManagementService : IChatManagementService
  {
    public ChatManagementService(IChatRepository chatRepository, IUserRepository userRepository)
    {
      _chatRepository = chatRepository;
      _userRepository = userRepository;
    }

    public Guid CreatePrivateChat(Guid creatorId, Guid companionId)
    {
      var creator = _userRepository.LoadUser(creatorId);
      var companion = _userRepository.LoadUser(companionId);
      var newChatId = Guid.NewGuid();
      var privateChat = new PrivateChat(
        newChatId,
        new Message[0],
        new ChatMember(creator.Id, creator.Nickname, ChatMemberRole.User),
        new ChatMember(companion.Id, companion.Nickname, ChatMemberRole.User));
      _chatRepository.SaveChat(privateChat);
      return newChatId;
    }

    public Guid CreateGroup(Guid creatorId)
    {
      var creator = _userRepository.LoadUser(creatorId);
      var newGroupId = Guid.NewGuid();
      var group = new Group(newGroupId, creator.Id, new Message[0], new[]
      {
        new ChatMember(creator.Id, creator.Nickname, ChatMemberRole.Admin) 
      });
      _chatRepository.SaveChat(@group);
      return newGroupId;
    }

    public Guid CreateChannel(Guid creatorId)
    {
      var creator = _userRepository.LoadUser(creatorId);
      var newChannelId = Guid.NewGuid();
      var channel = new Channel(newChannelId, creator.Id, new Message[0], new []
      {
        new ChatMember(creator.Id, creator.Nickname, ChatMemberRole.Admin) 
      });
      _chatRepository.SaveChat(channel);
      return newChannelId;
    }

    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
  }
}
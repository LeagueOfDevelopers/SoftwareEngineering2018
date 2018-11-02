using System;
using LeagueGram.Domain;
using LeagueGram.Domain.Exception;

namespace LeagueGram.Application
{
  public class ChatMembershipService : IChatMembershipService
  {
    public ChatMembershipService(IUserRepository userRepository, IChatRepository chatRepository)
    {
      _userRepository = userRepository;
      _chatRepository = chatRepository;
    }

    public void InviteMemberToChat(Guid chatId, Guid invitingUserId, Guid invitedUserId)
    {
      var chat = GetPublicChatOrThrow(chatId);
      var invited = _userRepository.LoadUser(invitedUserId);
      chat.InviteMember(invitingUserId, invitedUserId, invited.Nickname);
      _chatRepository.SaveChat(chat);
    }

    public void PromoteMemberToAdmin(Guid chatId, Guid actorId, Guid targetId)
    {
      var chat = GetPublicChatOrThrow(chatId);
      chat.PromoteToAdmin(actorId, targetId);
      _chatRepository.SaveChat(chat);
    }

    public void DemoteUserFromAdmin(Guid chatId, Guid actorId, Guid targetId)
    {
      var chat = GetPublicChatOrThrow(chatId);
      chat.DemoteFromAdmin(actorId, targetId);
      _chatRepository.SaveChat(chat);
    }

    private IPublicChat GetPublicChatOrThrow(Guid chatId)
    {
      var chat = _chatRepository.LoadChat(chatId);
      var publicChat = chat as IPublicChat;
      if (publicChat == null)
      {
        throw new ChatNotFoundException(chatId);
      }

      return publicChat;
    }

    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;
  }
}
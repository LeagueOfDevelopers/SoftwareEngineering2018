using System;
using LeagueGram.Domain;

namespace LeagueGram.Application
{
  public class ChatMessagingService : IChatMessagingService
  {
    public ChatMessagingService(IChatRepository chatRepository)
    {
      _chatRepository = chatRepository;
    }

    public Guid SendMessage(Guid chatId, Guid senderId, string textMessage)
    {
      var chat = _chatRepository.LoadChat(chatId);
      var messageId = chat.SendMessage(textMessage, senderId);
      _chatRepository.SaveChat(chat);

      return messageId;
    }

    public void EditMessage(Guid chatId, Guid actorMemberId, Guid messageId, string newMessageText)
    {
      var chat = _chatRepository.LoadChat(chatId);
      chat.EditMessage(actorMemberId, messageId, newMessageText);
      _chatRepository.SaveChat(chat);
    }

    public void DeleteMessage(Guid chatId, Guid actorMemberId, Guid messageId)
    {
      var chat = _chatRepository.LoadChat(chatId);
      chat.DeleteMessage(actorMemberId, messageId);
      _chatRepository.SaveChat(chat);
    }

    private readonly IChatRepository _chatRepository;
  }
}
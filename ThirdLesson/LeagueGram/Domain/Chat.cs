using System;
using System.Collections.Generic;
using LeagueGram.Domain.Exception;

namespace LeagueGram.Domain
{
  public abstract class Chat : IChat
  {
    protected Chat(Guid id, IEnumerable<Message> messages, IEnumerable<ChatMember> members)
    {
      Id = id;
      Messages = messages;
      Members = members;
    }

    public Guid Id { get; }

    public IEnumerable<Message> Messages { get; private set; }

    public IEnumerable<ChatMember> Members { get; protected set; }

    public Guid SendMessage(string messageText, Guid senderId)
    {
      var sender = GetMember(senderId);
      if (sender == null)
      {
        throw new UserNotFoundException(senderId);
      }

      if (!CanSendMessage(sender))
      {
        throw new InsufficientRightsException(senderId, nameof(SendMessage));
      }

      var message = new Message(Guid.NewGuid(), messageText, senderId, DateTimeOffset.UtcNow);
      var messagesList = new List<Message>(Messages);
      messagesList.Add(message);
      Messages = messagesList;

      return message.MessageId;
    }

    public void EditMessage(Guid actorMemberId, Guid messageId, string newMessage)
    {
      var message = GetMessage(messageId);
      if (message == null)
      {
        throw new MessageNotFoundException(Id, messageId);
      }

      var actor = GetMember(actorMemberId);
      if (actor == null)
      {
        throw new UserNotFoundException(actorMemberId);
      }

      if (!CanEditMessage(actor, message))
      {
        throw new InsufficientRightsException(actorMemberId, nameof(EditMessage));
      }

      message.Edit(newMessage);
    }

    public void DeleteMessage(Guid actorMemberId, Guid messageId)
    {
      var messageToDelete = GetMessage(messageId);
      if (messageToDelete == null)
      {
        return;
      }

      var actor = GetMember(actorMemberId);
      if (actor == null)
      {
        throw new UserNotFoundException(actorMemberId);  
      }

      if (!CanDeleteMessage(actor, messageToDelete))
      {
        throw new InsufficientRightsException(actorMemberId, nameof(DeleteMessage));
      }

      var messagesList = new List<Message>();
      foreach (var message in Messages)
      {
        if (message.MessageId != messageToDelete.MessageId)
        {
          messagesList.Add(message);
        }
      }

      Messages = messagesList;
    }

    protected abstract bool CanSendMessage(ChatMember chatMember);
    protected abstract bool CanEditMessage(ChatMember chatMember, Message message);
    protected abstract bool CanDeleteMessage(ChatMember chatMember, Message message);

    protected ChatMember GetMember(Guid memberId)
    {
      foreach (var member in Members)
      {
        if (member.Id == memberId)
        {
          return member;
        }
      }

      return null;
    }

    protected Message GetMessage(Guid messageId)
    {
      foreach (var message in Messages)
      {
        if (message.MessageId == messageId)
        {
          return message;
        }
      }

      return null;
    }
  }
}

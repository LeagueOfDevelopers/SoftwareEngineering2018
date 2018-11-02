using System;
using System.Linq;
using LeagueGram.Domain;
using LeagueGram.Domain.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeagueGram.UnitTests
{
  [TestClass]
  public class PrivateChatTests
  {
    [TestMethod]
    public void SendMessage_AddsNewMessage()
    {
      var chat = CreateChat(withMessage:false);
      var sender = chat.Members.First().Id;

      chat.SendMessage("new message", sender);

      Assert.AreEqual(1, chat.Messages.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void SendMessage_WhenNoSenderInChat_ThrowsUserNotFound()
    {
      var chat = CreateChat(withMessage:false);
      var sender = Guid.NewGuid();

      chat.SendMessage("new message", sender);
    }

    [TestMethod]
    public void EditMessage_ChangesMessageInList()
    {
      var chat = CreateChat(withMessage:true);
      var message = chat.Messages.First();
      var editedMessage = "edited message";

      chat.EditMessage(message.SenderId, message.MessageId, editedMessage);

      Assert.AreEqual(editedMessage, chat.Messages.First().Text);
    }

    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void EditMessage_WhenActorNotFoundInChat_ThrowsUserNotFound()
    {
      var chat = CreateChat(withMessage: true);
      var message = chat.Messages.First();
      var editedMessage = "editedMessage";

      chat.EditMessage(Guid.NewGuid(), message.MessageId, editedMessage);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void EditCompanionMessage_ThrowsInsufficcientRightsException()
    {
      var chat = CreateChat(withMessage: true);
      var message = chat.Messages.First();
      var editedMessage = "editedMessage";
      var actor = chat.Members.Last();

      chat.EditMessage(actor.Id, message.MessageId, editedMessage);
    }

    [TestMethod]
    [ExpectedException(typeof(MessageNotFoundException))]
    public void EditMessageNotFoundInChat_ThrowsMessageNotFoundException()
    {
      var chat = CreateChat(withMessage: true);
      var editedMessage = "edited message";
      var actor = chat.Members.First();

      chat.EditMessage(actor.Id, Guid.NewGuid(), editedMessage);
    }

    [TestMethod]
    public void DeleteMessage_RemovesMessageFromList()
    {
      var chat = CreateChat(withMessage: true);
      var messageToDelete = chat.Messages.First();
      var actor = messageToDelete.SenderId;

      chat.DeleteMessage(actor, messageToDelete.MessageId);

      Assert.AreEqual(0, chat.Messages.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void DeleteMessage_WhenActorNotFoundInChat_ThrowsUserNotFound()
    {
      var chat = CreateChat(withMessage: true);
      var messageToDelete = chat.Messages.First();
      
      chat.DeleteMessage(Guid.NewGuid(), messageToDelete.MessageId);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void DeleteMessageForCompanion_ThrowsInsufficientRightsException()
    {
      var chat = CreateChat(withMessage: true);
      var messageToDelete = chat.Messages.First();
      var actor = chat.Members.Last();

      chat.DeleteMessage(actor.Id, messageToDelete.MessageId);
    }

    private PrivateChat CreateChat(bool withMessage)
    {
      var creatorId = Guid.NewGuid();
      var companionId = Guid.NewGuid();
      var messages = withMessage
        ? new[] {new Message(Guid.NewGuid(), "message", creatorId, DateTimeOffset.UtcNow)}
        : new Message[0];
      return new PrivateChat(
        Guid.NewGuid(), 
        messages, 
        new ChatMember(creatorId, "creator", ChatMemberRole.User),
        new ChatMember(companionId, "companion", ChatMemberRole.User));
    }
  }
}
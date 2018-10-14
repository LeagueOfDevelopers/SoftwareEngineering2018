using System;
using System.Linq;
using LeagueGram.Domain;
using LeagueGram.Domain.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeagueGram.UnitTests
{
  [TestClass]
  public class ChannelTests
  {
    [TestInitialize]
    public void Initialize()
    {
      _creatorId = Guid.NewGuid();
      _addedAdminId = Guid.NewGuid();
      _userId = Guid.NewGuid();
    }

    [TestMethod]
    public void AdminSendMessage_MessageAddedToList()
    {
      var channel = CreateChannel(withMessage: false);
      var messageText = "new message";

      channel.SendMessage(messageText, _addedAdminId);
      var message = channel.Messages.First().Text;

      Assert.AreEqual(messageText, message);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserSendMessage_ThrowsInsufficientRightsException()
    {
      var channel = CreateChannel(withMessage: false);
      var messageText = "new message";

      channel.SendMessage(messageText, _userId);
    }

    [TestMethod]
    public void AdminEditMessage_MessageTextChangedInList()
    {
      var channel = CreateChannel(withMessage: true);
      var message = channel.Messages.First();
      var newMessageText = "new message text";

      channel.EditMessage(_addedAdminId, message.MessageId, newMessageText);
      var changedMessage = channel.Messages.First();

      Assert.AreEqual(newMessageText, changedMessage.Text);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserEditMessage_ThrowsInsufficientRightsException()
    {
      var channel = CreateChannel(withMessage: true);
      var message = channel.Messages.First();
      var newMessageText = "new message text";

      channel.EditMessage(_userId, message.MessageId, newMessageText);
    }

    [TestMethod]
    public void AdminDeletesMessage_MessageRemovedFromList()
    {
      var channel = CreateChannel(withMessage: true);
      var messageToDelete = channel.Messages.First();

      channel.DeleteMessage(_addedAdminId, messageToDelete.MessageId);
      var messagesCount = channel.Messages.Count();

      Assert.AreEqual(0, messagesCount);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserDeletesMessage_ThrowsInsufficientRightsException()
    {
      var channel = CreateChannel(withMessage: true);
      var messageToDelete = channel.Messages.First();

      channel.DeleteMessage(_userId, messageToDelete.MessageId);
    }

    [TestMethod]
    public void AdminInvitesUser_UserIsAddedToMembersList()
    {
      var channel = CreateChannel(withMessage: false);
      var newMemberNickname = "tester";

      channel.InviteMember(_addedAdminId, Guid.NewGuid(), newMemberNickname);
      var lastMember = channel.Members.Last();

      Assert.AreEqual(newMemberNickname, lastMember.NickName);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserInvitesUser_ThrowsInsuffucientRightsException()
    {
      var channel = CreateChannel(withMessage: false);
      var newMemberNickname = "tester";

      channel.InviteMember(_userId, Guid.NewGuid(), newMemberNickname);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void AddedAdminPromotesUser_ThrowsInsufficientRightsException()
    {
      var channel = CreateChannel(withMessage: false);

      channel.PromoteToAdmin(_addedAdminId, _userId);
    }

    [TestMethod]
    public void CreatorPromotesUser_UserRoleChangesToAdmin()
    {
      var channel = CreateChannel(withMessage: false);

      channel.PromoteToAdmin(_creatorId, _userId);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void AddedAdminDemotesUser_ThrowsInsufficientRightsException()
    {
      var channel = CreateChannel(withMessage: false);

      channel.DemoteFromAdmin(_addedAdminId, _creatorId);
    }

    [TestMethod]
    public void CreatorDemotesAdmin_AdminRoleChangesToUser()
    {
      var channel = CreateChannel(withMessage: false);

      channel.DemoteFromAdmin(_creatorId, _addedAdminId);
      var demoted = channel.Members.ToArray()[1];

      Assert.AreEqual(ChatMemberRole.User, demoted.Role);
    }

    private Channel CreateChannel(bool withMessage)
    {
      var messages = withMessage
        ? new[] { new Message(Guid.NewGuid(), "message", _creatorId, DateTimeOffset.UtcNow) }
        : new Message[0];
      return new Channel(
        Guid.NewGuid(),
        _creatorId,
        messages,
        new[]
        {
          new ChatMember(_creatorId, "creator", ChatMemberRole.Admin),
          new ChatMember(_addedAdminId, "added admin", ChatMemberRole.Admin),
          new ChatMember(_userId, "user", ChatMemberRole.User)
        });
    }

    private Guid _creatorId;
    private Guid _addedAdminId;
    private Guid _userId;
  }
}
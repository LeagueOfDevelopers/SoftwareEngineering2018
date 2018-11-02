using System;
using System.Linq;
using LeagueGram.Domain;
using LeagueGram.Domain.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeagueGram.UnitTests
{
  [TestClass]
  public class GroupTests
  {
    [TestInitialize]
    public void Initialize()
    {
      _creatorId = Guid.NewGuid();
      _addedAdminId = Guid.NewGuid();
      _userId = Guid.NewGuid();
    }

    [TestMethod]
    public void UserSendsMessage_MessageAddedToList()
    {
      var group = CreateGroup(withMessage: false);
      var messageText = "new message";

      group.SendMessage(messageText, _userId);
      var message = group.Messages.First().Text;

      Assert.AreEqual(messageText, message);
    }

    [TestMethod]
    public void UserEditsHisMessage_MessageChangedInList()
    {
      var group = CreateGroup(withMessage: false);
      group.SendMessage("new message", _userId);
      var messageId = group.Messages.First().MessageId;
      var editedMessageText = "edited message";

      group.EditMessage(_userId, messageId, editedMessageText);
      var editedMessage = group.Messages.First();

      Assert.AreEqual(editedMessageText, editedMessage.Text);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserEditsOthersMessage_ThrowsInsufficientRightsException()
    {
      var group = CreateGroup(withMessage: true);
      var messageId = group.Messages.First().MessageId;

      group.EditMessage(_userId, messageId, "edited message");
    }

    [TestMethod]
    public void AdminDeletesMessage_MessageRemovedFromList()
    {
      var group = CreateGroup(withMessage: true);
      var messageToDelete = group.Messages.First();

      group.DeleteMessage(_addedAdminId, messageToDelete.MessageId);
      var messagesCount = group.Messages.Count();

      Assert.AreEqual(0, messagesCount);
    }

    [TestMethod]
    public void UserDeletesHisMessage_MessageRemovedFromList()
    {
      var group = CreateGroup(withMessage: false);
      group.SendMessage("new message", _userId);
      var messageId = group.Messages.First().MessageId;

      group.DeleteMessage(_userId, messageId);
      var messagesCount = group.Messages.Count();

      Assert.AreEqual(0, messagesCount);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserDeletesOthersMessage_ThrowsInsufficientRightsException()
    {
      var group = CreateGroup(withMessage: true);
      var messageId = group.Messages.First().MessageId;

      group.DeleteMessage(_userId, messageId);
    }

    [TestMethod]
    public void AdminInvitesUser_UserIsAddedToMembersList()
    {
      var group = CreateGroup(withMessage: false);
      var newMemberNickname = "tester";

      group.InviteMember(_addedAdminId, Guid.NewGuid(), newMemberNickname);
      var lastMember = group.Members.Last();

      Assert.AreEqual(newMemberNickname, lastMember.NickName);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserInvitesUser_ThrowsInsuffucientRightsException()
    {
      var group = CreateGroup(withMessage: false);
      var newMemberNickname = "tester";

      group.InviteMember(_userId, Guid.NewGuid(), newMemberNickname);
    }

    [TestMethod]
    public void AdminPromotesUser_UserRoleChangedToAdmin()
    {
      var group = CreateGroup(withMessage: false);
      
      group.PromoteToAdmin(_addedAdminId, _userId);
      var promotedUser = group.Members.Last();

      Assert.AreEqual(ChatMemberRole.Admin, promotedUser.Role);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void UserPromotesUser_ThrowsInsufficientRightsException()
    {
      var group = CreateGroup(withMessage: false);
      var newUserId = Guid.NewGuid();
      group.InviteMember(_addedAdminId, newUserId, "user to promote");

      group.PromoteToAdmin(_userId, newUserId);
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientRightsException))]
    public void AddedAdminDemotesUser_ThrowsInsufficientRightsException()
    {
      var group = CreateGroup(withMessage: false);

      group.DemoteFromAdmin(_addedAdminId, _creatorId);
    }

    [TestMethod]
    public void CreatorDemotesAdmin_AdminRoleChangesToUser()
    {
      var group = CreateGroup(withMessage: false);

      group.DemoteFromAdmin(_creatorId, _addedAdminId);
      var demoted = group.Members.ToArray()[1];

      Assert.AreEqual(ChatMemberRole.User, demoted.Role);
    }

    private Group CreateGroup(bool withMessage)
    {
      var messages = withMessage
        ? new[] { new Message(Guid.NewGuid(), "message", _creatorId, DateTimeOffset.UtcNow) }
        : new Message[0];
      return new Group(
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
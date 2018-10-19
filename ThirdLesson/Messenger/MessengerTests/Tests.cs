using System;
using System.Collections.Generic;
using System.Linq;
using Messenger;
using Xunit;

namespace MessengerTests
{
   public class Tests
   {
      [Fact]
      public void CreateUserMethod_Created()
      {
         var messanger = new TextMessenger("ligagram");
         messanger.CreateUser("Vasya");

         Assert.True(messanger.Users.GetAll().Count == 1);
      }

      [Fact]
      public void UserCreatePrivate_CreatedRight()
      {
         var messanger = new TextMessenger("ligagram");
         var user1 = messanger.CreateUser("Vasya");
         var user2 = messanger.CreateUser("Petya");

         var privateChat = user1.CreatePrivate(user2);

         Assert.True(user1.Chats.Exists(privateChat) && user2.Chats.Exists(privateChat));
      }
      
      [Fact]
      public void UserCreateGroupSingle_Created()
      {
         var messenger = new TextMessenger("ligagram");
         var user = messenger.CreateUser("Vasya");

         user.CreateGroup("group");
         
         Assert.True(user.Chats.GetAll().Count == 1);
      }

      [Fact]
      public void UserCreateGroupWithParticipants_CreatedRight()
      {
         var messenger = new TextMessenger("ligagram");
         
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");
         var user3 = messenger.CreateUser("Kolya");

         var participants = new List<User> {user2, user3};

         var group = user1.CreateGroup("group", participants);
         
         Assert.True(user1.Chats.Exists(group) &&
                     user2.Chats.Exists(group) &&
                     user3.Chats.Exists(group));
      }
      
      [Fact]
      public void UserCreateChannel_Created()
      {
         var messenger = new TextMessenger("ligagram");
         var user = messenger.CreateUser("Vasya");

         user.CreateChannel("channel");
         
         Assert.True(user.Chats.GetAll().Count == 1);
      }
      
      [Fact]
      public void UserSendMessageInPrivate_SentRight()
      {
         var messanger = new TextMessenger("ligagram");
         var user1 = messanger.CreateUser("Vasya");
         var user2 = messanger.CreateUser("Petya");

         var privateChat = user1.CreatePrivate(user2);

         user1.SendMessage(privateChat, "message");

         Assert.True(privateChat.Messages.GetAll().Count == 1 &&
                     user1.Chats.GetAll()[0].Messages.GetAll().Count == 1 &&
                     user2.Chats.GetAll()[0].Messages.GetAll().Count == 1);
      }

      [Fact]
      public void UserRemoveMessageInPrivate_Removed()
      {
         var messanger = new TextMessenger("ligagram");
         var user1 = messanger.CreateUser("Vasya");
         var user2 = messanger.CreateUser("Petya");

         var privateChat = user1.CreatePrivate(user2);

         user1.SendMessage(privateChat, "message");
         user2.SendMessage(privateChat, "message2");

         user1.RemoveMessage(privateChat, privateChat.Messages.GetAll()[0]);

         Assert.True(privateChat.Messages.GetAll().Count == 1);
      }
      
      [Fact]
      public void UserRemoveMessageInPrivate_NotRemoved()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var privateChat = user1.CreatePrivate(user2);

         user1.SendMessage(privateChat, "message");
         user2.SendMessage(privateChat, "message2");
         
         user1.RemoveMessage(privateChat, privateChat.Messages.GetAll()[1]);
         
         Assert.True(privateChat.Messages.GetAll().Count == 2);
      }
      
      [Fact]
      public void UserEditMessageInPrivate_Edited()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var privateChat = user1.CreatePrivate(user2);

         user1.SendMessage(privateChat, "message");
         user2.SendMessage(privateChat, "message2");
         
         user1.EditMessage(privateChat, privateChat.Messages.GetAll()[0], "newText");
         
         Assert.True(privateChat.Messages.GetAll()[0].Text == "newText");
      }
      
      [Fact]
      public void UserEditMessageInPrivate_NotEdited()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var privateChat = user1.CreatePrivate(user2);

         user1.SendMessage(privateChat, "message");
         user2.SendMessage(privateChat, "message2");
         
         user1.EditMessage(privateChat, privateChat.Messages.GetAll()[1], "newText");
         
         Assert.True(privateChat.Messages.GetAll()[1].Text != "newText");
      }
      
      [Fact]
      public void UserSendMessageToChannel_Sent()
      {
         var messenger = new TextMessenger("ligagram");
         var user = messenger.CreateUser("Vasya");

         var channel = user.CreateChannel("channel");

         user.SendMessage(channel, "message");
         
         Assert.True(channel.Messages.GetAll().Count == 1);
      }
      
      [Fact]
      public void UserJoinChannel_Joined()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var channel = user1.CreateChannel("channel");
         
         user2.JoinChannel(channel);
         
         Assert.True(channel.Participants.GetAll().Count == 1);
      }
      
      [Fact]
      public void UserSendMessageToChannel_NotSent()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var channel = user1.CreateChannel("channel");
         
         user2.SendMessage(channel, "message");
         
         Assert.True(channel.Messages.GetAll().Count == 0);
      }
      
      [Fact]
      public void UserAddParticipantToGroup_Added()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var group = user1.CreateGroup("group");
         
         user1.AddParticipantToGroup(user2, group);
         
         Assert.True(group.Participants.GetAll().Count == 2 &&
                     user2.Chats.GetAll().Count == 1);
      }
      
      [Fact]
      public void UserAddParticipantToGroup_NotAdded()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");
         var user3 = messenger.CreateUser("Kolya");

         var group = user1.CreateGroup("group");
         
         user1.AddParticipantToGroup(user2, group);
         user2.AddParticipantToGroup(user3, group);
         
         Assert.True(group.Participants.GetAll().Count == 2 &&
                     user3.Chats.GetAll().Count == 0);
      }
      
      [Fact]
      public void UserAddAdminToGroup_Added()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var group = user1.CreateGroup("group");
         
         user1.AddAdminToGroup(user2, group);
         
         Assert.True(group.Admins.GetAll().Count == 2 &&
                     user2.Chats.GetAll().Count == 1);
      }
      
      [Fact]
      public void UserAddAdminToGroup_NotAdded()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");
         var user3 = messenger.CreateUser("Kolya");

         var group = user1.CreateGroup("group");
         
         user1.AddParticipantToGroup(user2, group);
         user2.AddAdminToGroup(user3, group);
         
         Assert.True(group.Admins.GetAll().Count == 1 &&
                     user3.Chats.GetAll().Count == 0);
      }
      
      [Fact]
      public void UserLeaveChannel_Left()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var channel = user1.CreateChannel("channel");
         
         user2.JoinChannel(channel);
         user2.LeaveChannel(channel);
         
         Assert.True(user2.Chats.GetAll().Count == 0 &&
                     channel.Participants.GetAll().Count == 0);
      }
      
      [Fact]
      public void UserLeaveGroup_Left()
      {
         var messenger = new TextMessenger("ligagram");
         var user1 = messenger.CreateUser("Vasya");
         var user2 = messenger.CreateUser("Petya");

         var group = user1.CreateGroup("group");
         
         user1.AddParticipantToGroup(user2, group);
         user2.LeaveGroup(group);
         
         Assert.True(user2.Chats.GetAll().Count == 0 &&
                     group.Participants.GetAll().Count == 1);
      }
   }
}
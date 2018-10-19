using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Messenger
{
   public class User
   {
      public Guid   Id   { get; }
      public string Name { get; }
      
      public ChatRepository Chats { get; } = new ChatRepository();

      public User(Guid id, string name)
      {
         Id   = id;
         Name = name;
      }

      public void SendMessage(IChat chat, string text)
      {
         if (Chats.Exists(chat))
            chat.AddMessage(this, new Message(Guid.NewGuid(), Id, text));
      }

      public void RemoveMessage(IChat chat, Message message)
      {
         if (Chats.Exists(chat))
            chat.RemoveMessage(this, message);
      }

      public void EditMessage(IChat chat, Message message, string newText)
      {
         if (Chats.Exists(chat))
            chat.EditMessage(this, message, newText);
      }

      public Private CreatePrivate(User companion)
      {
         if (Chats.ExistsPrivate(this, companion)) 
            throw new InvalidDataException("Dialog already exists");
         
         var privateChat = new Private(Guid.NewGuid(), this, companion);
         Chats.Add(privateChat);
         companion.Chats.Add(privateChat);
         return privateChat;
      }

      public Channel CreateChannel(string name)
      {
         var channel = new Channel(Guid.NewGuid(), this, name);
         Chats.Add(channel);
         return channel;
      }

      public Group CreateGroup(string name)
      {
         var group = new Group(Guid.NewGuid(), this, name);
         Chats.Add(group);
         return group;
      }
      
      public Group CreateGroup(string name, List<User> participants)
      {
         var group = new Group(Guid.NewGuid(), this, name, participants);
         
         Chats.Add(group);
         foreach (var participant in participants)
         {
            participant.Chats.Add(group);
         }
         return group;
      }

      public void AddParticipantToGroup(User user, Group group)
      {
         if (Chats.Exists(group))
            group.AddParticipant(this, user);
      }
      
      public void AddAdminToGroup(User user, Group group)
      {
         if (Chats.Exists(group))
            group.AddAdmin(this, user);
      }

      public void JoinChannel(Channel channel)
      {
         if (!Chats.Exists(channel))
            Chats.Add(channel);
         
         channel.AddParticipant(this);
      }

      public void LeaveChannel(Channel channel)
      {
         if (!Chats.Exists(channel) || channel.Admin.Id == Id)
            return;
         
         Chats.Remove(channel);
         channel.RemoveParticipant(this);
      }

      public void LeaveGroup(Group group)
      {
         if (!Chats.Exists(group) || 
             group.Admins.Exists(this) && group.Admins.GetAll().Count == 1)
            return;
         
         Chats.Remove(group);
         group.RemoveParticipant(this);
      }
   }
}
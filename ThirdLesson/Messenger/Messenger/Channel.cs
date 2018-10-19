using System;
using System.Collections.Generic;

namespace Messenger
{
   public class Channel : IChat
   {
      public Guid   Id    { get; }
      public User   Admin { get; }
      public string Name  { get; }
      
      public UserRepository    Participants { get; } = new UserRepository();
      public MessageRepository Messages     { get; } = new MessageRepository();
      
      public Channel(Guid id, User admin, string name)
      {
         Id    = id;
         Admin = admin;
         Name  = name;
      }
      
      public void AddMessage(User user, Message message)
      {
         if (!IsAdmin(user))
            return;
         if (!Messages.Exists(message))
            Messages.Add(message);
      }

      public void RemoveMessage(User user, Message message)
      {
         if (!IsAdmin(user))
            return;
         if (Messages.Exists(message))
            Messages.Remove(message);
      }

      public void EditMessage(User user, Message message, string newText)
      {
         if (!IsAdmin(user))
            return;
         if (Messages.Exists(message))
            Messages.Edit(message, newText);
      }

      public void AddParticipant(User user)
      {
         if (!Participants.Exists(user))
            Participants.Add(user);
      }

      public void RemoveParticipant(User user)
      {
         if (Participants.Exists(user))
            Participants.Remove(user);
      }

      private bool IsAdmin(User user)
      {
         return user.Id == Admin.Id;
      }
   }
}
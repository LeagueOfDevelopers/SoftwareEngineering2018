using System;
using System.Collections.Generic;

namespace Messenger
{
   public class Group : IChat
   {
      public Guid   Id    { get; }
      public string Name  { get; }
      
      public UserRepository    Admins       { get; } = new UserRepository();
      public UserRepository    Participants { get; } = new UserRepository();
      public MessageRepository Messages     { get; } = new MessageRepository();
      
      public Group(Guid id, User admin, string name)
      {
         Id    = id;
         Name  = name;
         
         Admins.Add(admin);
         Participants.Add(admin);
      }
      
      public Group(Guid id, User admin, string name, List<User> participants)
      {
         Id    = id;
         Name  = name;

         Admins.Add(admin);
         Participants.Add(admin);
         foreach (var participant in participants)
         {
            Participants.Add(participant);
         }
      }
      
      public void AddMessage(User user, Message message)
      {
         if (Participants.Exists(user) && !Messages.Exists(message))
            Messages.Add(message);
      }

      public void RemoveMessage(User user, Message message)
      {
         if (Participants.Exists(user) && Messages.Exists(message) && 
             (message.Owner == user.Id || IsAdmin(user)))
            Messages.Remove(message);
      }

      public void EditMessage(User user, Message message, string newText)
      {
         if (Participants.Exists(user) && Messages.Exists(message) &&
             (message.Owner == user.Id || IsAdmin(user)))
            Messages.Edit(message, newText);
      }

      public void AddParticipant(User user, User participant)
      {
         if (!IsAdmin(user) || Participants.Exists(participant))
            return;
         
         Participants.Add(participant);
         participant.Chats.Add(this);
      }

      public void RemoveParticipant(User user)
      {
         if (IsAdmin(user))
            Admins.Remove(user);
         if (Participants.Exists(user))
            Participants.Remove(user);
      }

      public void AddAdmin(User user, User newAdmin)
      {
         if (!IsAdmin(user) || Admins.Exists(newAdmin))
            return;
         
         Admins.Add(newAdmin);

         if (Participants.Exists(newAdmin)) 
            return;
         
         Participants.Add(newAdmin);
         newAdmin.Chats.Add(this);
      }

      private bool IsAdmin(User user)
      {
         return Admins.Exists(user);
      }
   }
}
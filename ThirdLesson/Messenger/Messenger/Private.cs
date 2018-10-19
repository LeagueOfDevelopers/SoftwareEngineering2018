using System;
using System.Collections.Generic;

namespace Messenger
{
   public class Private : IChat
   {         
      public Guid Id { get; }
      public MessageRepository Messages { get; } = new MessageRepository();

      public User FParticipant { get; }
      public User SParticipant { get; }

      public Private(Guid id, User fParticipant, User sParticipant)
      {
         Id = id;
         FParticipant = fParticipant;
         SParticipant = sParticipant;
      }
      
      public void AddMessage(User user, Message message)
      {
         if (!AllowedUser(user))
            return;
         if (!Messages.Exists(message))
            Messages.Add(message);
      }

      public void RemoveMessage(User user, Message message)
      {
         if (!AllowedUser(user))
            return;
         if (Messages.Exists(message) && message.Owner == user.Id)
            Messages.Remove(message);
      }

      public void EditMessage(User user, Message message, string newText)
      {
         if (!AllowedUser(user))
            return;
         if (Messages.Exists(message) && message.Owner == user.Id)
            Messages.Edit(message, newText);
      }

      private bool AllowedUser(User user)
      {
         return user.Id == FParticipant.Id || user.Id == SParticipant.Id;
      }
   }
}
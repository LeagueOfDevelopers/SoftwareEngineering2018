using System;
using System.Collections;
using System.Collections.Generic;

namespace Messenger
{
   public interface IChat
   {      
      Guid Id { get; }
      MessageRepository Messages { get; }

      void AddMessage(User user, Message message);
      void RemoveMessage(User user, Message message);
      void EditMessage(User user, Message message, string newText);
   }
}
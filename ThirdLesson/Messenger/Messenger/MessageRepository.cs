using System;
using System.Collections.Generic;

namespace Messenger
{
   public class MessageRepository : IMessageRepository
   {
      private readonly List<Message> _messages = new List<Message>();

      public bool Exists(Message message)
      {
         return _messages.Find(cht => cht == message) != null;
      }

      public void Add(Message message)
      {
         _messages.Add(message);
      }

      public void Remove(Message message)
      {
         _messages.Remove(message);
      }

      public void Edit(Message message, string newText)
      {
         message.Edit(newText);
      }
      
      public List<Message> GetAll()
      {
         return _messages;
      }
   }
}
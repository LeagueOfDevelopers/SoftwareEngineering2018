using System;
using System.Collections.Generic;

namespace Messenger
{
   public interface IMessageRepository
   {
      bool Exists(Message message);
      
      void Add(Message message);
      void Remove(Message message);

      List<Message> GetAll();
   }
}
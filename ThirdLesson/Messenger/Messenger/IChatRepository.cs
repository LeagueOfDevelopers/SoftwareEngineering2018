using System;
using System.Collections.Generic;

namespace Messenger
{
   public interface IChatRepository
   {
      bool Exists(IChat chat);
      
      void Add(IChat chat);
      void Remove(IChat chat);

      List<IChat> GetAll();
   }
}
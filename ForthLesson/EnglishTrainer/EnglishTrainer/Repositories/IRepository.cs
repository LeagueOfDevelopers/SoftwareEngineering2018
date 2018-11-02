using System;
using System.Collections.Generic;

namespace EnglishTrainer
{
   public interface IRepository<T>
   {
      T Get(Guid id);

      void Save(T obj);
   }
}
using System;
using System.Collections.Generic;

namespace EnglishTrainer
{
   public class Word
   {
      public readonly string Original;
      public readonly string Translation;
      public int Progress { get; set; }

      public Word(string original, string translation, int progress)
      {
         Original = original;
         Translation = translation;
         Progress = progress;
      }
   }
}
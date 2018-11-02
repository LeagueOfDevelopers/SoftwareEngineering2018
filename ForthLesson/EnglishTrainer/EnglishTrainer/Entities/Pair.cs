namespace EnglishTrainer
{
   public class Pair
   {
      public readonly Word Original;
      public readonly string Translation;

      public Pair(Word original, string translation)
      {
         Original = original;
         Translation = translation;
      }
   }
}
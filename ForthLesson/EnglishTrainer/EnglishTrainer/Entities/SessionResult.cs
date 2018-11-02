namespace EnglishTrainer
{
   public class SessionResult
   {
      public readonly Word[] Unknown;
      public readonly Word[] Studied;

      public SessionResult(Word[] unknown, Word[] studied)
      {
         Unknown = unknown;
         Studied = studied;
      }
   }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace EnglishTrainer
{
   public class Session
   {
      public Guid SessionId { get; }
      public Guid UserId { get; }
      public bool Completed { get; private set; }
      public Pair CurrentPair { get; private set; }
      
      public readonly Word[] Words;
      private readonly string[] _translations;
      private int _currentWordNumber;

      public Session(Guid sessionId, Guid userId, Word[] words)
      {
         SessionId = sessionId;
         UserId = userId;
         Words = words.Length == 0 ? throw new ArgumentException("Words array cannot be empty") : words;
         _translations = words.Select(word => word.Translation).ToArray();
      }

      public void Start()
      {
         NextPair();
      }

      public void Answer(bool answer)
      {
         if (Completed) throw new OperationCanceledException("Session is completed");
         
         if (answer == (CurrentPair.Original.Translation == CurrentPair.Translation))
            Words[_currentWordNumber].Progress++;

         _currentWordNumber++;
         NextPair();
      }

      public SessionResult End()
      {
         var list = new List<Word[]>();

         var unknown = Words.Where(word => word.Progress < 3).ToArray();
         var studied = Words.Where(word => word.Progress == 3).ToArray();
         
         return new SessionResult(unknown, studied);
      }
      
      private void NextPair()
      {
         if (_currentWordNumber >= Words.Length)
         {
            Completed = true;
            return;
         }
         
         CurrentPair = new Pair(Words[_currentWordNumber], GetRandomTranslation());
      }

      private string GetRandomTranslation()
      {
         var random = new Random();
         
         var goodTranslation = Words[_currentWordNumber].Translation;
         var translationsWithoutGood = _translations.Where(translation => translation != goodTranslation).ToArray();

         var badTranslation = translationsWithoutGood.Length != 0
            ? translationsWithoutGood[random.Next(0, translationsWithoutGood.Length)]
            : goodTranslation;

         return random.Next(0, 10) >= 5 ? goodTranslation : badTranslation;
      }
   }
}
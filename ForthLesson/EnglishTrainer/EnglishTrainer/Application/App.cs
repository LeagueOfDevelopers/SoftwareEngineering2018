using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnglishTrainer.Application;
using EnglishTrainer.Repositories;
using Newtonsoft.Json;

namespace EnglishTrainer
{
   public class App
   {
      private const string PathToUnknown = "../../../../EnglishTrainer/Words/unknown.json";
      private const string PathToStudied = "../../../../EnglishTrainer/Words/studied.json";
      
      private readonly SessionService _sessionService = new SessionService();
      private readonly UserService _userService = new UserService();

      public Guid RegisterUser(string name)
      {
         return _userService.Register(name);
      }
      
      public Guid CreateSession(Guid userId)
      {
         return _sessionService.Create(userId);
      }
      
      public void StartSession(Guid sessionId)
      {
         _sessionService.Start(sessionId);
      }

      public void AnswerSession(Guid sessionId, bool answer)
      {
         _sessionService.Answer(sessionId, answer);
      }

      public void EndSession(Guid sessionId)
      {
         var result = _sessionService.End(sessionId);
         
         SaveUnknown(result.Unknown);
         SaveStudied(result.Studied);
      }

      private static void SaveUnknown(IEnumerable<Word> words)
      {
         var unknown = JsonConvert.SerializeObject(words);
         File.WriteAllText(PathToUnknown, unknown);
      }

      private static void SaveStudied(IEnumerable<Word> words)
      {
         var studied = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText(PathToStudied));
         words.ToList().ForEach(word => studied.Add(word));
         
         var newStudied = JsonConvert.SerializeObject(studied);
         File.WriteAllText(PathToStudied, newStudied);
      }
   }
}
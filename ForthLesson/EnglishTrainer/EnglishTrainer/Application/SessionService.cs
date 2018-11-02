using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnglishTrainer.Repositories;
using Newtonsoft.Json;

namespace EnglishTrainer.Application
{
   public class SessionService
   {
      private const string Path = "../../../../EnglishTrainer/Words/unknown.json";
      private readonly SessionRepository _sessionRepository;

      public SessionService(SessionRepository sessionRepository = null)
      {
         _sessionRepository = sessionRepository ?? new SessionRepository();
      }
      
      public Guid Create(Guid userId)
      {
         var guid = Guid.NewGuid();
         _sessionRepository.Save(new Session(guid, userId, GrabWords()));   
         return guid;
      }

      public void Start(Guid sessionId)
      {
         var session = _sessionRepository.Get(sessionId) ?? 
                       throw new NullReferenceException("Unknown session identifier");
         session.Start();
      }

      public void Answer(Guid sessionId, bool answer)
      {
         var session = _sessionRepository.Get(sessionId) ?? 
                       throw new NullReferenceException("Unknown session identifier");
         session.Answer(answer);
      }
      
      public SessionResult End(Guid sessionId)
      {
         var session = _sessionRepository.Get(sessionId) ?? 
                       throw new NullReferenceException("Unknown session identifier");
         return session.End();
      }

      private static Word[] GrabWords()
      {
         return JsonConvert.DeserializeObject<IEnumerable<Word>>(File.ReadAllText(Path)).ToArray();
      }
   }
}
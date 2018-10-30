using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using EnglishTrainer;
using EnglishTrainer.Application;
using EnglishTrainer.Repositories;
using Newtonsoft.Json;

namespace EnglishTrainerTests
{
   public class Tests
   {
      [Fact]
      public void UserRepositoryGetUser_User()
      {
         var userId = Guid.NewGuid();
         var repo = CreateUserRepositoryWithUser(userId);

         Assert.True(repo.Get(userId).Id == userId);
      }

      [Fact]
      public void UserRepositoryGetUser_Null()
      {
         var repo = new UserRepository();

         Assert.True(repo.Get(Guid.NewGuid()) == null);
      }

      [Fact]
      public void UserRepositorySaveUser_Saved()
      {
         var repo = new UserRepository();
         var userId = Guid.NewGuid();
         var user = new User(userId, "name");

         repo.Save(user);

         Assert.True(repo.Get(userId) == user);
      }

      [Fact]
      public void UserServiceRegister_Registered()
      {
         var repo = new UserRepository();
         var userService = new UserService(repo);
         var userId = userService.Register("name");

         Assert.True(userId == repo.Get(userId).Id);
      }

      [Fact]
      public void SessionRepositoryGetSession_Session()
      {
         var sessionId = Guid.NewGuid();
         var repo = CreateSessionRepositoryWithSession(sessionId);

         Assert.True(repo.Get(sessionId).SessionId == sessionId);
      }

      [Fact]
      public void SessionRepositoryGetSession_Null()
      {
         var repo = CreateSessionRepositoryWithSession(Guid.NewGuid());

         Assert.True(repo.Get(Guid.NewGuid()) == null);
      }

      [Fact]
      public void SessionRepositorySaveSession_Saved()
      {
         var sessionId = Guid.NewGuid();
         var session = new Session(sessionId, Guid.NewGuid(), new[] {new Word("original", "оригинал", 0)});
         var repo = new SessionRepository();
         repo.Save(session);
         
         Assert.True(repo.Get(sessionId) == session);
      }
      
      [Fact]
      public void SessionServiceCreate_Created()
      {
         var repo = new SessionRepository();
         var sessionService = new SessionService(repo);
         var sessionId = sessionService.Create(Guid.NewGuid());
         
         Assert.True(repo.Get(sessionId).SessionId == sessionId);
      }
      
      [Fact]
      public void SessionCreateEmpty_ThrowException()
      {
         var exceptionText = "";
         
         try
         {
            var session = new Session(Guid.NewGuid(), Guid.NewGuid(), new Word[] {});
         }
         catch (Exception e)
         {
            exceptionText = e.Message;
         }
         finally
         {
            Assert.True(exceptionText == "Words array cannot be empty");  
         }
      }

      [Fact]
      public void SessionStart_Started()
      {
         var session = CreateSessionWithWord();
         session.Start();
         
         Assert.True(session.CurrentPair.Original.Translation == session.CurrentPair.Translation);
      }
      
      [Fact]
      public void SessionAnswer_Completed()
      {
         var session = CreateSessionWithWord();
         session.Start();
         session.Answer(true);
         
         Assert.True(session.Completed);
      }
      
      [Fact]
      public void SessionAnswer_ThrowException()
      {
         var session = CreateSessionWithWord();
         session.Start();
         session.Answer(true);
         var exceptionText = "";

         try
         {
            session.Answer(true);
         }
         catch (Exception e)
         {
            exceptionText = e.Message;
         }
         finally
         {
            Assert.True(exceptionText == "Session is completed");  
         }
      }
      
      [Fact]
      public void SessionEnd_CorrectResult()
      {
         var session = CreateSessionWithWord();
         session.Start();
         session.Answer(true);
         var result = session.End();

         Assert.True(result.Unknown.Length == 0 && result.Studied.Length == 1);
      }
      
      [Fact]
      public void SessionServiceStartUnknown_ThrowException()
      {
         var sessionService = new SessionService();
         var exceptionText = "";
         try
         {
            sessionService.Start(Guid.NewGuid());
         }
         catch (Exception e)
         {
            exceptionText = e.Message;
         }
         finally
         {
            Assert.True(exceptionText == "Unknown session identifier");
         }
      }
      
      [Fact]
      public void SessionServiceAnswerUnknown_ThrowException()
      {
         var sessionService = new SessionService();
         var exceptionText = "";
         try
         {
            sessionService.Answer(Guid.NewGuid(), true);
         }
         catch (Exception e)
         {
            exceptionText = e.Message;
         }
         finally
         {
            Assert.True(exceptionText == "Unknown session identifier");
         }
      }
      
      [Fact]
      public void SessionServiceEndUnknown_ThrowException()
      {
         var sessionService = new SessionService();
         var exceptionText = "";
         try
         {
            var result = sessionService.End(Guid.NewGuid());
         }
         catch (Exception e)
         {
            exceptionText = e.Message;
         }
         finally
         {
            Assert.True(exceptionText == "Unknown session identifier");
         }
      }
      
      [Fact]
      public void SessionServiceGrabWords_CreatedWithWords()
      {
         var repo = new SessionRepository();
         var sessionService = new SessionService(repo);
         var sessionId = sessionService.Create(Guid.NewGuid());
         
         Assert.True(repo.Get(sessionId).Words.Length == 5);
      }

      [Fact]
      public void AppEndSession_NothingChanges()
      {
         const string pathToUnknown = "../../../../EnglishTrainer/Words/unknown.json";
         const string pathToStudied = "../../../../EnglishTrainer/Words/studied.json";

         var startUnknown = File.ReadAllText(pathToUnknown);
         var startStudied = File.ReadAllText(pathToStudied);
         
         var App = new App();
         var sessionId = App.CreateSession(Guid.NewGuid());
         App.StartSession(sessionId);
         App.EndSession(sessionId);

         var endUnknown = File.ReadAllText(pathToUnknown);
         var endStudied = File.ReadAllText(pathToStudied);
         
         Assert.True(startUnknown == endUnknown && startStudied == endStudied);
      }

      private static Session CreateSessionWithWord()
      {
         var words = new[] {new Word("original", "оригинал", 2)};
         return new Session(Guid.NewGuid(), Guid.NewGuid(), words);
      }
      
      private static UserRepository CreateUserRepositoryWithUser(Guid userId)
      {
         var userList = new List<User> {new User(userId, "name")};
         return new UserRepository(userList);
      }

      private static SessionRepository CreateSessionRepositoryWithSession(Guid sessionId)
      {
         var user = new User(Guid.NewGuid(), "name");
         var words = new[] {new Word("original", "оригинал", 0)};
         var sessionList = new List<Session> {new Session(sessionId, user.Id, words)};

         return new SessionRepository(sessionList);
      }
   }
}
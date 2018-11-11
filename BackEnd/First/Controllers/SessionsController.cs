using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnglishLearner.Application;
using EnglishLearnerAPI.Models;

namespace EnglishLearnerAPI.Controllers
{
    [Produces("application/json")]
    public class SessionsController : Controller
    {
        private ISessionManager sessionManager;

        public SessionsController(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
        }

        [HttpPost]
        [Route("api/sessions")]
        public ActionResult CreateSession([FromBody]Session req)
        {
            Guid id = sessionManager.CreateSession(req.userId);
            return Ok(new Session {sessionId = id, userId = req.userId});
        }

        [HttpPost]
        [Route("api/sessions/{id}")]
        public ActionResult SaveResults(string id, [FromBody]TaskResult[] req)
        {
            Dictionary<Guid, string> answ = new Dictionary<Guid, string>();
            foreach(TaskResult res in req)
            {
                answ[res.wordId] = res.answer;
            }

            sessionManager.SaveResults(new Guid(id), answ);
            return Ok();
        }

        [HttpGet]
        [Route("api/sessions/{id}/exercise")]
        public ActionResult GetExercise(string id)
        {
            Word[] words = sessionManager.GetExercise(new Guid(id)).Select(el => new Word
            {
                id = el.Id,
                enWord = el.EnWord,
                transWord = el.TransWord
            }).ToArray();
            return Ok(words);
        }

        [HttpGet]
        [Route("api/sessions/{id}")]
        public ActionResult GetSession(string id)
        {
            return Ok(new Session());
        }

        //[HttpGet]
        //[Route("api/sessions")]
        //public ActionResult GetSessions()
        //{
        //    return Ok();
        //}

        [HttpDelete]
        [Route("api/sessions/{id}")]
        public ActionResult CloseSessions(string id)
        {
            return Ok();
        }
    }
}
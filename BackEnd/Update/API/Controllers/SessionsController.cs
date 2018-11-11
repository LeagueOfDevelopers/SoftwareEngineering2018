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
            return Ok(new Session(req.userId, id, false));
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
            return Ok(sessionManager.SaveResults(new Guid(id), answ));
        }

        [HttpGet]
        [Route("api/sessions/{id}/exercise")]
        public ActionResult StartSession(string id)
        {
            Word[] resp = sessionManager
                            .StartSession(new Guid(id))
                            .Select(el => new Word(el.EnWord, el.TransWord, el.Id)).ToArray();
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/sessions")]
        public ActionResult GetSessions(string id)
        {
            Session[] resp = sessionManager
                .GetSessions()
                .Select(el => new Session(el.User.Id, el.Id, el.IsOpen)).ToArray();
            return Ok(resp);
        }

        [HttpDelete]
        [Route("api/sessions/{id}")]
        public ActionResult CloseSessions(string id)
        {
            sessionManager.CloseSession(new Guid(id));
            return Ok("Closed");
        }
    }
}
using System;
using System.Collections.Generic;
using BusinessEntities;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WithDbLoDSprintApi.Models;

namespace WithDbLoDSprintApi.Controllers
{
    public class SessionServiceController : ControllerBase
    {
        public SessionServiceController(IFinishSessionService finishSessionService, IStartSessionService startSessionService)
        {
            _finishSessionService = finishSessionService ?? throw new ArgumentNullException(nameof(finishSessionService));
            _startSessionService = startSessionService ?? throw new ArgumentNullException(nameof(startSessionService));
        }

        [HttpGet]
        [Route("users/{traineeUserId}/sessions")]
        public ActionResult StartSession(Guid traineeUserId)
        {
            var session = _startSessionService.StartSession(traineeUserId);
            return Ok(new StartedSessionModel(session.Id, session.Questions));
        }

        [HttpPut]
        [Route("users/{traineeUserId}/sessions/{sessionId}")]
        public ActionResult FinishSession(Guid traineeUserId, Guid sessionId, [FromBody] IEnumerable<Answer> answers)
        {
            _finishSessionService.FinishSession(traineeUserId, sessionId, answers);
            return Ok();
        }

        private readonly IFinishSessionService _finishSessionService;
        private readonly IStartSessionService _startSessionService;
    }
}
using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using LeonLearn;
using LeonLearnAPI.Models;

namespace LeonLearnAPI.Controllers
{
    [Route("/lessons")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private static Service _mainService = new Service();

        [HttpGet("{userId}")]
        public ActionResult GetLesson(string userId)
        {
            if (!Guid.TryParse(userId, out var userGuid))
            {
                return BadRequest("Invalid id");
            }

            try
            {
                var lesson = _mainService.BeginLesson(userGuid);
                return Ok(lesson);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e);
                return BadRequest("No such user id");
            }
        }

        [HttpPost("{userId}")]
        public ActionResult EndLesson(string userId, [FromBody] EndLessonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Guid.TryParse(userId, out var userGuid))
            {
                return BadRequest("Invalid id");
            }

            try
            {
                var correct = _mainService.EndLesson(userGuid, request.Lesson, request.Answers);
                return Ok(correct);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
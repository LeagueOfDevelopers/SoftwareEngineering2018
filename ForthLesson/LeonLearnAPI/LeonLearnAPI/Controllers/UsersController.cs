using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using LeonLearn;
using LeonLearnAPI.Models;

namespace LeonLearnAPI.Controllers
{
    [Route("/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private Service _service;
        
        public UsersController(Service service)
        {
            _service = service;
        }
        
        [HttpPut]
        public ActionResult RegisterUser([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdId =  _service.RegisterUser(request.Name);
                return Ok($"User registered with id {createdId}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return Conflict("User already exists");
            }
        }

        [HttpGet]
        [Route("{userId}/learned")]
        public ActionResult GetLearnedWords(string userId)
        {
            if (!Guid.TryParse(userId, out var userGuid))
            {
                return BadRequest("Invalid id");
            }

            try
            {
                var learnedWords = _service.GetLearnedWords(userGuid);
                return Ok(learnedWords);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e);
                return BadRequest("No such user id");
            }
        }

        [HttpGet]
        [Route("{userId}/progress")]
        public ActionResult GetInProgressWords(string userId)
        {
            if (!Guid.TryParse(userId, out var userGuid))
            {
                return BadRequest("Invalid id");
            }

            try
            {
                var inProgressWords = _service.GetInProgressWords(userGuid);
                return Ok(inProgressWords);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e);
                return BadRequest("No such user id");
            }
        }
    }
}
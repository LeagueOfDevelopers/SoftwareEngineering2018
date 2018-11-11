using System;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WithDbLoDSprintApi.Controllers
{
    public class UserServiceController : ControllerBase
    {
        public UserServiceController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [Route("users/{userId}/words/learned")]
        public ActionResult GetUserLearnedWords(Guid userId)
        {
            return Ok(_userService.GetUserLearnedWords(userId));
        }

        [HttpGet]
        [Route("users/{userId}/words/studied")]
        public ActionResult GetUserStudiedWords(Guid userId)
        {
            return Ok(_userService.GetUserStudiedWords(userId));
        }

        [HttpPost]
        [Route("users")]
        public ActionResult RegisterUser([FromBody] string nickName)
        {
            return Ok(_userService.RegisterUser(nickName));
        }

        private readonly IUserService _userService;
    }
}
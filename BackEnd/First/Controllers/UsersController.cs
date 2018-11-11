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
    public class UsersController : Controller
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [Route("api/users")]
        public ActionResult RegisterUser([FromBody]User req)
        {
            Guid id = userService.RegisterUser(req.name);
            return Ok(id.ToString());
        }



        [HttpGet]
        [Route("api/users/{id}")]
        public ActionResult GetUser([FromBody] string id)
        {
            return Ok(new User());
        }

        //[HttpGet]
        //[Route("api/users")]
        //public ActionResult GetUsers()
        //{
        //    return Ok();
        //}

        [HttpPut]
        [Route("api/users/{id}")]
        public ActionResult EditUser(string id, [FromBody] User req)
        {
            return Ok();
        }
    }
}
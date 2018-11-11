using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLessons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLessonsAPI.Controllers
{

    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }


        [HttpPost]
        [Route("users")]
        public ActionResult CreateUser([FromBody]string name)
        {
            var userId = Guid.NewGuid();
            
            var user = new User(name, new Guid(), new List<RepWord>(), new Dictionary<string, UserWord>());
            _userRepository.SaveUser(user);
            return Ok(userId);
        }

        [HttpGet]
        [Route("users/{id}")]
        public ActionResult GetUser(Guid id)
        {
            return Ok(_userRepository.LoadUser(id));
        }

    }
}
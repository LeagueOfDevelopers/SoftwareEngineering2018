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

        [HttpPut]
        [Route("api/users/{id}/words/on-learning")]
        public ActionResult AddWordOnLearning(string id, [FromBody]string wordId)
        {
            userService.AddWordOnLearning(new Guid(id), new Guid(wordId));
            return Ok("Word was added");
        }

        [HttpPut]
        [Route("api/users/{id}")]
        public ActionResult EditUser(string id, [FromBody] User req)
        {
            userService.EditUser(new Guid(id), req.name);
            return Ok("Edited");
        }

        [HttpGet]
        [Route("api/users/{id}/words/on-learning")]
        public ActionResult GetWordsOnLearning(string id)
        {
             Word[] resp = userService.GetAllWordsOnLearning(new Guid(id))
                .Select(el => new Word(el.EnWord, el.TransWord, el.Id)).ToArray();
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/users/{id}/words/learned")]
        public ActionResult GetLearnedWords(string id)
        {
            Word[] resp = userService.GetAllLearnedWords(new Guid(id))
                .Select(el => new Word(el.EnWord, el.TransWord, el.Id)).ToArray();
            return Ok(resp);
        }
    }
}
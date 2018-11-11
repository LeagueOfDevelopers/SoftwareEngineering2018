using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnglishLearnerAPI.Models;
using EnglishLearner.Application;

namespace EnglishLearnerAPI.Controllers
{
    [Produces("application/json")]
    public class WordsController : Controller
    {
        [HttpGet]
        [Route("api/users/{id}/words/on-learning")]
        public ActionResult GetWordsOnLearning(string id)
        {
            return Ok(new Word[] { });
        }

        [HttpGet]
        [Route("api/users/{id}/words/learned")]
        public ActionResult GetLearnedWords(string id)
        {
            return Ok(new Word[] { });
        }

        [HttpPut]
        [Route("api/users/{id}/words/on-learning")]
        public ActionResult AddWordOnLearning(string id, [FromBody]Word[] req)
        {
            return Ok();
        }
    }
}
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
        private IWordManager wordManager;
        
        public WordsController(IWordManager wordManager)
        {
            this.wordManager = wordManager ?? throw new ArgumentNullException(nameof(wordManager));
        }

        [HttpPost]
        [Route("api/words")]
        public ActionResult AddWordToDictonary([FromBody]AddWordRequest word)
        {
            return Ok(wordManager.AddWordToDictonary(word.enWord, word.transWord));
        }

        [HttpGet]
        [Route("api/words")]
        public ActionResult GetDictionary()
        {
            IEnumerable<EnglishLearner.IPareOfWords> dict = wordManager.Words.ToArray();
            return Ok(dict);
        }
        
        
    }
}
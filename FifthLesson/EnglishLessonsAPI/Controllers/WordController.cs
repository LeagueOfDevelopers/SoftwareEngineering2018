using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLessons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLessonsAPI.Controllers
{

    public class WordController : Controller
    {
        private readonly IWordRepository _wordRepository;

        public WordController(IWordRepository WordRepository)
        {
            _wordRepository = WordRepository;
        }


        [HttpPost]
        [Route("words")]
        public ActionResult CreateWord([FromBody]string name)
        {
            string[] Parsed = name.Split(' ');
            string eng = Parsed[0];
            string rus = Parsed[1];
            
            var word = new RepWord(eng, rus);
            _wordRepository.SaveWord(word);
            return Ok();
        }

        [HttpGet]
        [Route("words")]
        public ActionResult GetUser(Guid id)
        {
            return Ok(_wordRepository.LoadWord());
        }

    }
}
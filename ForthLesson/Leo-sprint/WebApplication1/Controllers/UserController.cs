using Leo_sprint;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Leo_sprintAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("users")]
        public ActionResult CreateUser([FromBody] NicknameModel nickname)
        {
            var userId = _userRepository.CreateUser(nickname._nickname);
            return Ok($"User witn nickname {nickname._nickname} created with id {userId}");
        }
    
        [HttpGet]
        [Route("users/{id}")]
        public ActionResult GetUser(Guid id)
        {
            var user = _userRepository.LaodUser(id);
            if (user._nickname.Equals(string.Empty))
            {
                return BadRequest($"This user does not exist");
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("users/{id}/wordInProcess")]
        public ActionResult GetUsersWordsInProcess(Guid id)
        {
            var words = new List<string>();
            var user = _userRepository.LaodUser(id);
            return Ok(user.ShowWordInProgress());
        }
        [HttpGet]
        [Route("users/{id}/learnedWords")]
        public ActionResult GetUsersLearnedWords(Guid id)
        {
            var words = new List<string>();
            var user = _userRepository.LaodUser(id);
            return Ok(user.ShowLearnedWord());
        }

        [HttpPost]
        [Route("users/{id}/wordInProcess")]
        public ActionResult AddWordToUsersWordsInProcess(Guid id, [FromBody] WordModel word)
        {
            var user = _userRepository.LaodUser(id);
            user.AddNewWordInDictionary(new Word(word.In_english, word.In_russian, 0));
            _userRepository.SaveUser(user);
            return Ok($"Word added");
        }
        [HttpDelete]
        [Route("users/{id}/wordInProcess")]
        public ActionResult RemoveWordFromDictionary(Guid id, [FromBody] WordModel word)
        {
            var user = _userRepository.LaodUser(id);
            user.RemoveWordFromDictionary(new Word(word.In_english, word.In_russian, 0));
            _userRepository.SaveUser(user);
            return Ok($"Word removed");
        }
        [HttpPost]
        [Route("users/{id}/sessions")]
        public ActionResult StartSession(Guid id, [FromBody] int number_of_words)
        {
            var user = _userRepository.LaodUser(id);
            var session_id = SessionClient.StartSession(user, number_of_words);
            return Ok($"Session created with id {session_id}");
        }

        [HttpGet]
        [Route("users/{id}/sessions/{session_id}/showTask")]
        public ActionResult ShowTask(Guid session_id)
        {
            return Ok(SessionClient.ShowTask(session_id));
        }
        [HttpPost]
        [Route("users/{id}/sessions/{session_id}/sendAnswers")]
        public ActionResult GetAnswers([FromBody] AnswerModel answers, Guid session_id)
        {
            SessionClient.GetAnswers(answers.Answers, session_id);
            return Ok("Answers checked");
        }
        [HttpGet]
        [Route("users/{id}/sessions/{session_id}/showWrongAnswers")]
        public ActionResult ShowWrongAnswers(Guid session_id, Guid id)
        {
            var user = _userRepository.LaodUser(id);
            var wrong_answers = SessionClient.CheckAnswers(user, session_id);
            _userRepository.SaveUser(user);
            return Ok(wrong_answers);
        }


    }
}

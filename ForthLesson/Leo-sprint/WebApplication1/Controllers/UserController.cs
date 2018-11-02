using Leo_sprint;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Leo_sprintAPI.Controllers
{
    public class UsersController : Controller
    {

        [HttpGet]
        [Route("users/{id}")]
        public string GetUserById(Guid id)
        {
            return UserRepository.GetJson(id);
        }
        [HttpGet]
        [Route("users")]
        public IEnumerable<string> GetUsers(Guid id)
        {
            return UserRepository.GetAllUsers();
        }
        [HttpGet]
        [Route("users/{id}/word_in_process")]
        public IEnumerable<string> GetUsersWordsInProcess(Guid id)
        {
            var words = new List<string>();
            var userPub = JsonConvert.DeserializeObject<UserModel>(UserRepository.GetJson(id));
            foreach (var word in userPub.words_in_process)
            {
                words.Add(word._in_english + "-" + word._in_russian);
            }
            return words;
        }
        [HttpGet]
        [Route("users/{id}/learned_words")]
        public IEnumerable<string> GetUsersLearnedWords(Guid id)
        {
            var words = new List<string>();
            var user = JsonConvert.DeserializeObject<UserModel>(UserRepository.GetJson(id));
            foreach (var word in user.learned_words)
            {
                words.Add(word._in_english + "-" + word._in_russian);
            }
            return words;
        }
        [HttpPost]
        [Route("users")]
        public string CreateUser([FromBody]string nickname)
        {
            var id = UserRepository.Create(nickname);
            return $"User witn nickname {nickname} created with id {id}";
        }
        [HttpPost]
        [Route("users/{id}/word_in_process")]
        public string AddWordToUsersWordsInProcess(Guid id, [FromBody] string in_english, [FromBody] string in_russian)
        {
            var userPub = JsonConvert.DeserializeObject<UserModel>(UserRepository.GetJson(id));
            var user = new User(userPub._nickname, userPub._id, userPub.learned_words, userPub.words_in_process);
            user.AddNewWordInDictionary(new Word(in_english, in_russian, 0));
            return $"Word added";
        }
        [HttpDelete]
        [Route("users/{id}/word_in_process")]
        public string RemoveWordFromDictionary(Guid id, [FromBody] string in_english, [FromBody] string in_russian)
        {
            var userPub = JsonConvert.DeserializeObject<UserModel>(UserRepository.GetJson(id));
            var user = new User(userPub._nickname, userPub._id, userPub.learned_words, userPub.words_in_process);
            user.RemoveWordFromDictionary(new Word(in_english, in_russian, 0));
            return $"Word removed";
        }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoDSprintApi.Exceptions;
using LoDSprintApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoDSprintApi.Controllers
{
    [ApiController]
    public class StartSessionController : ControllerBase
    {
        public StartSessionController(InFileDictionaryRepository dictionaryRepository, InMemorySessionsRepository sessionsRepository, InFileUsersRepository usersRepository)
        {
            _dictionaryRepository = dictionaryRepository;
            _oneSessionWordsCount = 2;
            _sessionsRepository = sessionsRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet("users/{traineeUserId}/sessions")]
        public SessionModel StartSession(Guid traineeUserId)
        {
            var creator = _usersRepository
                .LoadUser(traineeUserId) ?? throw new NotFoundException($"User with Id {traineeUserId} not found");
            var newSession = new SessionModel(
                Guid.NewGuid(),
                traineeUserId,
                GetRandomQuestions(creator),
                new List<AnswerModel>()
                );

            _sessionsRepository
                .SaveSession(newSession);

            return newSession;
        }

        private IEnumerable<QuestionModel> GetRandomQuestions(UserModel player)
        {
            var questions = new List<QuestionModel>();

            for (int i = 0; i < _oneSessionWordsCount; ++i)
                questions
                    .Add(GetRandomQuestionForUserWhichNotContainIn
                    (player, questions));

            return questions;
        }

        private QuestionModel GetRandomQuestionForUserWhichNotContainIn(UserModel user, IEnumerable<QuestionModel> questions)
        {
            var RandomWord = GetRandomWordForUser(user);
            var IsRandomTranslation = new Random()
                                        .Next(2) == 1;
            var question = new QuestionModel(
                RandomWord,
                (IsRandomTranslation) ?
                GetRandomTranslation() :
                _dictionaryRepository
                    .GetWordTranslation(RandomWord)
                );

            return (questions.Contains(question)) ?
                GetRandomQuestionForUserWhichNotContainIn(user, questions) : question;
        }

        private WordModel GetRandomWordForUser(UserModel user)
        {
            var randomWord = _dictionaryRepository
                .GetRandomWord();

            return (user.WordIsLearned(randomWord)) ? GetRandomWordForUser(user) : randomWord;
        }

        private TranslationModel GetRandomTranslation()
        {
            var randomWord = _dictionaryRepository
                .GetRandomWord();

            return _dictionaryRepository
                .GetWordTranslation(randomWord);
        }

        private readonly int _oneSessionWordsCount;
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IUsersRepository _usersRepository;
    }
}
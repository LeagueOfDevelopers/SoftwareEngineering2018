using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using BusinessServices.Exceptions;
using BusinessServices.Interfaces;
using Data.Interfaces;

namespace BusinessServices.Services
{
    public class StartSessionService : IStartSessionService
    {
        public StartSessionService(int oneSessionWordsCount, IDictionaryRepository dictionaryRepository, ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _oneSessionWordsCount = oneSessionWordsCount;
            _dictionaryRepository = dictionaryRepository ?? throw new ArgumentNullException(nameof(dictionaryRepository));
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Session StartSession(Guid traineeUserId)
        {
            var creator = _userRepository
                              .LoadUser(traineeUserId) ?? throw new NotFoundException($"User with Id {traineeUserId} not found");
            var newSession = new Session(
                Guid.NewGuid(),
                traineeUserId,
                GetRandomQuestions(creator),
                new List<Answer>()
            );

            _sessionRepository
                .SaveSession(newSession);

            return newSession;
        }

        private IEnumerable<Question> GetRandomQuestions(TraineeUser player)
        {
            var questions = new List<Question>();

            for (int i = 0; i < _oneSessionWordsCount; ++i)
                questions
                    .Add(GetRandomQuestionForUserWhichNotContainIn
                        (player, questions));

            return questions;
        }

        private Question GetRandomQuestionForUserWhichNotContainIn(TraineeUser user, IEnumerable<Question> questions)
        {
            var randomWord = GetRandomWordForUser(user);
            var isRandomTranslation = new Random().Next(2) == 1;
            var question = new Question(
                randomWord,
                (isRandomTranslation) ?
                    GetRandomTranslation() :
                    _dictionaryRepository
                        .GetWordTranslation(randomWord)
            );

            return questions.Contains(question) ?
                GetRandomQuestionForUserWhichNotContainIn(user, questions) : question;
        }

        private Word GetRandomWordForUser(TraineeUser user)
        {
            var randomWord = _dictionaryRepository.LoadRandomWord();

            return user.WordIsLearned(randomWord) ? GetRandomWordForUser(user) : randomWord;
        }

        private Translation GetRandomTranslation()
        {
            var randomWord = _dictionaryRepository.LoadRandomWord();

            return _dictionaryRepository
                .GetWordTranslation(randomWord);
        }

        private readonly int _oneSessionWordsCount;
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
    }
}

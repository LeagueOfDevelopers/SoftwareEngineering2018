using LoDSprint.Exceptions;
using LoDSprint.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoDSprint.Application
{
    public class SessionService : ISessionService
    {
        public SessionService(int oneSessionWordsCount, InMemorySessionsRepository sessionsRepository, InFileUsersRepository usersRepository, InFileDictionaryRepository dictionaryRepository)
        {
            _oneSessionWordsCount = oneSessionWordsCount;
            _sessionsRepository = sessionsRepository;
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _dictionaryRepository = dictionaryRepository ?? throw new ArgumentNullException(nameof(dictionaryRepository));
        }

        public Session StartSession(Guid traineeUserId)
        {
            var creator = _usersRepository
                .LoadUser(traineeUserId);
            var newSession = new Session(
                Guid.NewGuid(),
                traineeUserId,
                GetRandomQuestions(creator),
                new List<Answer>()
                );

            _sessionsRepository
                .SaveSession(newSession);

            return newSession;
        }

        public void FinishSession(Session finishedSession)
        {

            if (_sessionsRepository.ContainsSession(finishedSession))
            {
                _sessionsRepository
                    .SaveSession(finishedSession);
                var sessionCreatorId = finishedSession
                    .CreatorId;
                var sessionCreator = _usersRepository
                    .LoadUser(sessionCreatorId);
                var userAnswers = finishedSession
                    .Answers;
                SaveUserAnswers(sessionCreator, userAnswers);
            }
            else
                throw new NotFoundException(
                    "Submitted session doesn't exist");
        }

        private IEnumerable<Question> GetRandomQuestions(IUser player)
        {
            var questions = new List<Question>();

            for(int i = 0; i < _oneSessionWordsCount; ++i)
                questions
                    .Add(GetRandomQuestionForUserWhichNotContainIn
                    (player, questions));

            return questions;
        }
        private Question GetRandomQuestionForUserWhichNotContainIn(IUser user, IEnumerable<Question> questions)
        {
            var RandomWord = GetRandomWordForUser(user);
            var IsRandomTranslation = new Random()
                                        .Next(2) == 1;
            var question = new Question(
                RandomWord,
                (IsRandomTranslation) ?
                GetRandomTranslation() :
                _dictionaryRepository
                    .GetWordTranslation(RandomWord)
                );

            return (questions.Contains(question)) ?
                GetRandomQuestionForUserWhichNotContainIn(user, questions) : question;
        }

        private Word GetRandomWordForUser(IUser user)
        {
            var randomWord = _dictionaryRepository
                .GetRandomWord();

            return (user.WordIsLearned(randomWord)) ? GetRandomWordForUser(user) : randomWord;
        }

        private Translation GetRandomTranslation()
        {
            var randomWord = _dictionaryRepository
                .GetRandomWord();

            return _dictionaryRepository
                .GetWordTranslation(randomWord);
        }

        private void SaveUserAnswers(IUser user, IEnumerable<Answer> answers)
        {
            var correctAnsweredWords = answers
                .Where(answer => 
                    IsTheCorrectAnswer(answer))
                .Select(correctAnswer =>
                    correctAnswer.Question.Word);
            var wrongAnsweredWords = answers
                .Where(answer => 
                    !IsTheCorrectAnswer(answer))
                .Select(incorrectAnswer => 
                    incorrectAnswer.Question.Word);
            user
                .SaveCorrectAnsweredWords(wrongAnsweredWords);
            user
                .SaveCorrectAnsweredWords(correctAnsweredWords);
            _usersRepository.SaveUser(user);
        }
        
        private bool IsTheCorrectAnswer(Answer answer)
        {
            var proposedWord = answer
                .Question
                .Word;
            var proposedTranslation = answer
                .Question
                .ProposedTranslation;
            var correctTranslation = _dictionaryRepository
                .GetWordTranslation(proposedWord);
            var userAnswer = answer
                .Value;
            var correctAnswer = proposedTranslation == correctTranslation;

            return userAnswer == correctAnswer;
        }

        private readonly int _oneSessionWordsCount;
        private readonly InMemorySessionsRepository _sessionsRepository;
        private readonly InFileUsersRepository _usersRepository;
        private readonly InFileDictionaryRepository _dictionaryRepository;        
    }
}

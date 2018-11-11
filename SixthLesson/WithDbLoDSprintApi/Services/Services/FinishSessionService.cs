using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using BusinessServices.Exceptions;
using BusinessServices.Interfaces;
using Data.Interfaces;

namespace BusinessServices.Services
{
    public class FinishSessionService : IFinishSessionService
    {
        public FinishSessionService(IDictionaryRepository dictionaryRepository, ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _dictionaryRepository = dictionaryRepository ?? throw new ArgumentNullException(nameof(dictionaryRepository));
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void FinishSession(Guid traineeUserId, Guid sessionId, IEnumerable<Answer> answers)
        {
            if (!_sessionRepository.ContainsSession(sessionId))
                throw new NotFoundException(
                    $"Session with id {sessionId} doesn't exist");

            var finishedSession = _sessionRepository.LoadSession(sessionId);

            if (finishedSession.CreatorId != traineeUserId)
                throw new PermissionDeniedException(
                    $"User with id {traineeUserId} doesn't have permissions to finish session with id {finishedSession.Id}");

            if (answers.Count() != finishedSession.Questions.Count())
                throw new IncorrectAnswersException(
                    "Not all questions have answers");

            finishedSession.AnswerTheQuestions(answers);
            var sessionCreator = _userRepository.LoadUser(finishedSession.CreatorId);
            SaveUserAnswers(sessionCreator, finishedSession.Answers);
            _sessionRepository.DeleteSession(finishedSession.Id);
        }

        private void SaveUserAnswers(TraineeUser user, IEnumerable<Answer> answers)
        {
            var correctAnsweredWords = answers.Where(IsTheCorrectAnswer)
                .Select(correctAnswer =>
                    correctAnswer.Question.Word);
            var wrongAnsweredWords = answers.Where(answer =>
                    !IsTheCorrectAnswer(answer))
                .Select(incorrectAnswer =>
                    incorrectAnswer.Question.Word);
            user.SaveCorrectAnsweredWords(correctAnsweredWords);
            user.SaveWrongAnsweredWords(wrongAnsweredWords);
            _userRepository.SaveUser(user);
        }

        private bool IsTheCorrectAnswer(Answer answer)
        {
            var proposedWord = answer.Question.Word;
            var proposedTranslation = answer.Question.ProposedTranslation;
            var correctTranslation = _dictionaryRepository
                .GetWordTranslation(proposedWord);

            var userAnswer = answer.Value;
            var correctAnswer = proposedTranslation == correctTranslation;

            return userAnswer == correctAnswer;
        }

        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
    }
}

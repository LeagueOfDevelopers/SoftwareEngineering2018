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
    public class FinishSessionController : ControllerBase
    {
        public FinishSessionController(InFileDictionaryRepository dictionaryRepository, InMemorySessionsRepository sessionsRepository, InFileUsersRepository usersRepository)
        {
            _dictionaryRepository = dictionaryRepository;
            _sessionsRepository = sessionsRepository;
            _usersRepository = usersRepository;
        }

        [HttpPut("users/{traineeUserId}/sessions")]
        public void FinishSession(Guid traineeUserId, [FromBody] SessionModel finishedSession)
        {
            if (!_sessionsRepository.ContainsSession(finishedSession.Id))
                throw new NotFoundException(
                    $"Session with id {finishedSession.Id} doesn't exist");

            var sessionCreatorId = finishedSession
                    .CreatorId;

            if (sessionCreatorId != traineeUserId)
                throw new PermissionDeniedException(
                        $"User with id {traineeUserId} doesn't have permissions to finish session with id {finishedSession.Id}");

            var sessionCreator = _usersRepository.LoadUser(sessionCreatorId);
            var userAnswers = finishedSession.Answers;
            _sessionsRepository.SaveSession(finishedSession);
            SaveUserAnswers(sessionCreator, userAnswers);
            _sessionsRepository.DeleteSession(finishedSession.Id);
        }

        private void SaveUserAnswers(UserModel user, IEnumerable<AnswerModel> answers)
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
                .SaveCorrectAnsweredWords(correctAnsweredWords);
            user
                .SaveWrongAnsweredWords(wrongAnsweredWords);
            _usersRepository.SaveUser(user);
        }

        private bool IsTheCorrectAnswer(AnswerModel answer)
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
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IUsersRepository _usersRepository;
    }
}
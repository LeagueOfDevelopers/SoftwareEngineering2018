using System;
using System.Collections.Generic;
using LoDSprint;
using LoDSprint.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoDSprintTests
{
    [TestClass]
    public class SesssionTests
    {
        [TestMethod]
        [ExpectedException(typeof(SeveralTimeAnsweringException))]
        public void AnswerTheQuestionsTest_TryToAnswersSeveralTimes_ThrowException()
        {
            var session = CreateSession(withQuestion: false);
            var answers = new List<Answer>();

            session.AnswerTheQuestions(answers);
            session.AnswerTheQuestions(answers);
        }

        [TestMethod]
        public void AnswerTheQuestionsTest_AnswerTheQuestions_AnswersAddedAnswer()
        {
            var session = CreateSession(withQuestion: true);
            var question = ((Question[])session.Questions)[0];
            var answer = new Answer(question, true);

            session.AnswerTheQuestions
                (new Answer[] { answer });

            var answers = (List<Answer>)session.Answers;
            CollectionAssert.Contains(answers, answer);
        }

        private Session CreateSession(bool withQuestion)
        {
            var question = withQuestion
               ? new[] { new Question(new Word("word"), new Translation("question")) }
               : new Question[0];

            return new Session(
                Guid.NewGuid(),
                Guid.NewGuid(),
                question,
                new List<Answer>()
                );
        }
    }
}

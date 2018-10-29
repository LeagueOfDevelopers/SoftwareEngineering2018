using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LeonLearn
{
    public class SpeedExerciseSession : ISpeedExerciseSession

    {
        private Dictionary<Lesson, bool[]> LessonsAnswers = new Dictionary<Lesson, bool[]>();
        private IUserRepository userRepo;
        private IWordsRepository wordRepo;
        private User user;
        private int lessonLength = 10;

        public SpeedExerciseSession(Guid userId)
        {
            userRepo = JsonUserRepository.Default;
            wordRepo = JsonWordsRepository.Default;
            user = userRepo.GetUser(userId);
        }

        public SpeedExerciseSession(string usersPath, string wordsPath, Guid userId)
        {
            userRepo = new JsonUserRepository(usersPath);
            wordRepo = new JsonWordsRepository(wordsPath);
            user = userRepo.GetUser(userId);
        }

        public Lesson CreateLesson()
        {
            Random r = new Random();

            List<bool> correctAnswers = new List<bool>();

            var wordsToLearn = wordRepo.GetUnlearnedWords(user.LearnedWords)
                .OrderBy(item => r.Next()).Take(lessonLength).ToArray();

            if (wordsToLearn.Length == 0) throw new DataException("No words to learn");

            var taskWords = wordsToLearn.Select(wordPair =>
            {
                if (r.Next(2) > 0.5f)
                {
                    correctAnswers.Add(true);
                    return wordPair;
                }

                correctAnswers.Add(false);
                var fakePair = wordRepo.GetRandomPairFromSource(wordsToLearn.Except(new[] {wordPair}));

                return new WordPair(wordPair.Origin, fakePair.Translation);
            });

            var taskWordsArray = taskWords.ToArray();

            var lesson = new Lesson(taskWordsArray);
            LessonsAnswers.Add(lesson, correctAnswers.ToArray());
            return lesson;
        }

        public bool[] EndLesson(Lesson lesson, bool[] userAnswers)
        {
            bool[] userCorrect = CheckAnswers(lesson, userAnswers);

            for (int i = 0; i < userCorrect.Length; i++)
            {
                if (!userCorrect[i] || LessonsAnswers[lesson][i] == false) continue;
                var correctPair = lesson.Tasks.ToArray()[i];
                user.MarkWord(correctPair);
            }
            
            userRepo.EditUser(user);
            return userCorrect;
        }

        private bool[] CheckAnswers(Lesson lesson, bool[] userAnswers)
        {
            if (LessonsAnswers[lesson].Length != userAnswers.Length)
                throw new ArgumentException("Answers must be the same length as tasks");

            bool[] lessonCorrect = LessonsAnswers[lesson];
            bool[] userCorrect = new bool[lessonCorrect.Length];

            for (int i = 0; i < userAnswers.Length; i++)
            {
                userCorrect[i] = lessonCorrect[i] == userAnswers[i];
            }

            return userCorrect;
        }
    }
}
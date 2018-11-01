using System;
using System.Collections.Generic;

namespace LeonLearn
{
    public class Service
    {
        private Dictionary<Guid, SpeedExerciseSession> _sessions = new Dictionary<Guid, SpeedExerciseSession>();

        public void RegisterUser(string name)
        {
            var repo = JsonUserRepository.Default;

            var newUser = new User(
                Guid.NewGuid(),
                name, DateTimeOffset.Now,
                new List<WordPair>(),
                new List<int>(),
                new List<WordPair>());

            repo.AddUser(newUser);
        }

        public Lesson BeginLesson(Guid userId)
        {
            if (_sessions.ContainsKey(userId)) return _sessions[userId].CreateLesson();

            var newSession = new SpeedExerciseSession(userId);
            _sessions.Add(userId, newSession);

            return _sessions[userId].CreateLesson();
        }

        public bool[] EndLesson(Guid userId, Lesson lesson, bool[] answers)
        {
            var session = _sessions[userId];
            var correct = session.EndLesson(lesson, answers);

            return correct;
        }

        public IEnumerable<WordPair> GetInProgressWords(Guid userId)
        {
            var repo = JsonUserRepository.Default;

            var user = repo.GetUser(userId);

            return user.InProgressWords;
        }

        public IEnumerable<WordPair> GetLearnedWords(Guid userId)
        {
            var repo = JsonUserRepository.Default;

            var user = repo.GetUser(userId);

            return user.LearnedWords;
        }
    }
}
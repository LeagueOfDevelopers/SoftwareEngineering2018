using System;
using System.Linq;
using System.Collections.Generic;
using English.Domain;
using English.Infrastructure;

namespace English.Application
{
    public class ExerciseService : IExerciseService
    {
        private readonly UserDatabase _userDatabase = UserDatabase.GetInstance();
        private readonly ItemRepository<IExercise> _exerciseRepository;

        public ExerciseService(
            ItemRepository<IExercise> exerciseRepository)
        {
            _exerciseRepository = exerciseRepository 
                ?? throw new ArgumentNullException(nameof(exerciseRepository));
        }

        public (List<Word>, List<Word>) GetWords(Guid exericeId, Guid userId, int amount)
        {
            var user = _userDatabase.LoadUserFromFile(userId);
            var exerice = _exerciseRepository.Load(exericeId);

            var (originalWords, anotherWords) = exerice.GetWordsFor(user, amount);

            return (originalWords, anotherWords);
        }

        public bool GuessWord(Guid exericeId, Guid userId, string original, Guid translationId)
        {
            var user = _userDatabase.LoadUserFromFile(userId);
            var translation = user.LearningWords.First(word => word.Key.Id == translationId);
            var exerice = _exerciseRepository.Load(exericeId);

            var result = exerice.GuessWord(user, translation.Key, original);
            SaveUserProgress(user);

            return result;
        }

        public void SaveUserProgress(IUser user)
        {
            _userDatabase.SaveUserToFile(user);
        }

        public IUser GetUserById(Guid userId)
        {
            return _userDatabase.LoadUserFromFile(userId);
        }
    }
}

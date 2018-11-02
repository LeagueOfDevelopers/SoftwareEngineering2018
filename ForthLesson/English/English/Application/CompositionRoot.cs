using English.Domain;
using English.Infrastructure;
using System.Collections.Generic;

namespace English.Application
{
    public class CompositionRoot
    {
        public static CompositionRoot Create()
        {
            var exerciseRepository = new ItemRepository<IExercise>(new List<IExercise>());

            var userService = new UserService();
            var exerciseService = new ExerciseService(exerciseRepository);

            return new CompositionRoot()
            {
                UserService = userService,
                ExerciseService = exerciseService
            };
        }

        public IUserService UserService { get; private set; }
        public IExerciseService ExerciseService { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using English.Domain;

namespace English.Application
{
    public interface IExerciseService
    {
        (List<Word>, List<Word>) GetWords(Guid exericeId, Guid userId, int amount);

        bool GuessWord(Guid exericeId, Guid userId, string original, Guid translationId);

        void SaveUserProgress(IUser user);
    }
}

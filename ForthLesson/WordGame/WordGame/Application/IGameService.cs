using System;

namespace WordGame.Application
{
    interface IGameService
    {
        void CheckAnswer(Guid idOfUser, WordForGame word);
        WordForGame GetWord(Guid idOfUser);
    }
}
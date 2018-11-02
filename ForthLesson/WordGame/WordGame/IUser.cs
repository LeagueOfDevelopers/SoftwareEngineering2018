using System;
using System.Collections.Generic;

namespace WordGame
{
    public interface IUser
    {
        Guid Id { get; }
        Dictionary<string, int> InProcessWords { get; }
        string Name { get; }
        List<string> StudiedWords { get; }

        void AddOneValueToProcess(WordForGame word);
        bool WordAlreadyStudied(string Word);
        bool WordStudiedNow(WordForGame word);
    }
}
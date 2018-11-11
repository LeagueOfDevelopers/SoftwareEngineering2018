using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
    public class TraineeUser
    {
        public TraineeUser(Guid id, string nickName, IEnumerable<Word> learnedWords,
            IEnumerable<StudiedWord> studiedWords)
        {
            Id = id;
            NickName = nickName ?? throw new ArgumentNullException(nameof(nickName));
            _learnedWords = learnedWords.ToList();
            _studiedWords = studiedWords.ToList();
        }

        public Guid Id { get; }

        public string NickName { get; }

        public IEnumerable<Word> LearnedWords => _learnedWords;

        public IEnumerable<StudiedWord> StudiedWords => _studiedWords;
    

        public bool WordIsLearned(Word word)
        {
            return _learnedWords?.Contains(word) ?? false;
        }

        public void SaveWrongAnsweredWords(IEnumerable<Word> wrongAnsweredWords)
        {
            foreach (var word in wrongAnsweredWords)
            {
                if (!IsStudied(word))
                    _studiedWords.Add(
                        new StudiedWord(word.Value, 0));
            }
        }

        public void SaveCorrectAnsweredWords(IEnumerable<Word> correctAnsweredWords)
        {
            foreach (var word in correctAnsweredWords)
            {
                if (IsStudied(word))
                    _studiedWords.Find(studiedWord =>
                        studiedWord.Value == word.Value)
                        .IncreaseCount();
                else
                    _studiedWords
                        .Add(new StudiedWord(word.Value, 1));
            }

            CheckNewLearnedWords();
        }

        private void CheckNewLearnedWords()
        {
            if (_studiedWords != null)
            {
                var newLearnedWords = _studiedWords.Where(word =>
                        word.RightAnswersCount >= 3)
                    .ToList()
                    .Select(learnedWord =>
                        new Word(learnedWord.Value))
                    .ToList();

                _learnedWords.AddRange(newLearnedWords.ToList());

                foreach (var word in newLearnedWords)
                    _studiedWords
                        .Remove(_studiedWords
                            .Find(studiedWord =>
                                studiedWord.Value == word.Value));
            }
        }

        private bool IsStudied(Word checkingWord)
        {
            var studiedWord = _studiedWords?.Find(word => 
                    word.Value == checkingWord.Value);

            return (studiedWord != null);
        }

        private readonly List<Word> _learnedWords;
        private readonly List<StudiedWord> _studiedWords;
    }
}

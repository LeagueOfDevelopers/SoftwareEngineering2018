using LoDSprintApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LoDSprintApi
{
    public class UserModel
    {
        public UserModel(Guid id, string nickName, List<WordModel> learnedWords, List<StudiedWordModel> studiedWords)
        {
            Id = id;
            NickName = nickName ?? throw new ArgumentNullException(nameof(nickName));
            _learnedWords = learnedWords ?? new List<WordModel>();
            _studiedWords = studiedWords ?? new List<StudiedWordModel>();
        }

        public Guid Id { get; }

        public string NickName { get; }

        public IEnumerable<WordModel> LearnedWords => _learnedWords;

        public IEnumerable<StudiedWordModel> StudiedWords => _studiedWords;

        public bool WordIsLearned(WordModel word)
        {
            return (_learnedWords != null)
                ? _learnedWords.Contains(word)
                : false;
        }

        public void SaveWrongAnsweredWords(IEnumerable<WordModel> wrongAnsweredWords)
        {
            foreach(var word in wrongAnsweredWords)
            {
                if (!IsStudied(word))
                    _studiedWords.Add(
                        new StudiedWordModel(word, 0));
            }
        }

        public void SaveCorrectAnsweredWords(IEnumerable<WordModel> correctAnsweredWords)
        {
            foreach(var word in correctAnsweredWords)
            {
                if (IsStudied(word))
                    _studiedWords
                        .Find(studiedWord => 
                        studiedWord.Word == word)
                        .IncreaseCount();
                else
                    _studiedWords
                        .Add(new StudiedWordModel(word, 1));
            }

            CheckNewLearnedWords();
        }

        private void CheckNewLearnedWords()
        {
            if (StudiedWords != null)
            {
                var newLearnedWords = _studiedWords.Where(word =>
                        word.RightAnswersCount >= 3)
                    .ToList()
                    .Select(learnedWord =>
                        learnedWord.Word);
                if (newLearnedWords != null)
                {
                    _learnedWords.AddRange(newLearnedWords.ToList());

                    foreach (var word in newLearnedWords)
                        _studiedWords
                            .Remove(
                                _studiedWords
                                .Find(studiedWord => 
                                    studiedWord.Word == word));
                }
            }
        }

        private bool IsStudied(WordModel word)
        {
            return StudiedWords
                .Select(studiedWord => 
                    studiedWord.Word)
                .Contains(word);
        }

        private List<StudiedWordModel> _studiedWords;
        private List<WordModel> _learnedWords;
    }
}

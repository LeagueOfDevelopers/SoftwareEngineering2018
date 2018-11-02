using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoDSprintApi.Models
{
    public class StudiedWordModel
    {
        public StudiedWordModel(WordModel word, int rightAnswersCount)
        {
            Word = word ?? throw new ArgumentNullException(nameof(word));
            RightAnswersCount = rightAnswersCount;
        }

        public WordModel Word { get; }
        public int RightAnswersCount { get; private set; }

        public void IncreaseCount()
        {
            RightAnswersCount++;
        }
    }
}

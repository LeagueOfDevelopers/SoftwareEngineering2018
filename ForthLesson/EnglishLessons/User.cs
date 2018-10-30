using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public class User
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        private List<RepWord> _learned { get; set; }
        private Dictionary<string, UserWord> _inProgress { get; set; }
        
        public List<RepWord> ShowLearned()
        {
            return _learned;
        }

        public List<RepWord> ShowInProgress()
        {
            List<RepWord> toShow = new List<RepWord>();
            foreach (KeyValuePair<string, UserWord> a in _inProgress)
            {
                toShow.Add(new RepWord(a.Value.Eng, a.Value.Rus));
            }

            return toShow;
        }

        public bool MarkAsLearned(string toMark)
        {
            if (_inProgress[toMark] == null) return false;

            _learned.Add(_inProgress[toMark]);
            _inProgress.Remove(toMark);

            return true;
        }

        public User (string name, Guid id, List<RepWord> learned, Dictionary<string, UserWord> inprogress)
        {
            Name = name;
            Id = id;
            _learned = learned;
            _inProgress = inprogress;
        }
    }
}

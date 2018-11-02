using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer
{
    public class User : IUser
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<Word> _inMemoryWords { get; set; }

        public User(string name, Guid id, List<Word> inMemoryWords)
        {
            Name = name;
            Id = id;
            _inMemoryWords = inMemoryWords;

        }
    }
}

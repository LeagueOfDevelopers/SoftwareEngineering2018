using System;
using System.Collections.Generic;
using System.Linq;

namespace LeonLearn
{
    public class Lesson
    {
        public IEnumerable<WordPair> Tasks { get; private set; }

        public Lesson(IEnumerable<WordPair> tasks)
        {
            Tasks = tasks;
        }

        public override string ToString()
        {
            return string.Join("\n", Tasks.Select(item => item.Origin + " " + item.Translation));
        }
    }
}
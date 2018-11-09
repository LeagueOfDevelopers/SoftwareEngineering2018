using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leo_sprint
{
    public class UserPub
    {
        public string _nickname;
        public Guid _id;
        public List<string> learned_words;
        public List<string> words_in_process;

        public UserPub(string nickname, Guid id, List<string> learned_words, List<string> words_in_process)
        {
            _nickname = nickname;
            _id = id;
            this.learned_words = learned_words;
            this.words_in_process = words_in_process;
        }

        public override bool Equals(object obj)
        {
            var pub = obj as UserPub;
            return pub != null &&
                   _nickname == pub._nickname &&
                   _id.Equals(pub._id) &&
                   EqualityComparer<List<string>>.Default.Equals(learned_words, pub.learned_words) &&
                   EqualityComparer<List<string>>.Default.Equals(words_in_process, pub.words_in_process);
        }

        public User ToUser()
        {
            var l_words = new List<Word>();
            var p_words = new List<Word>();
            if (learned_words != null)
            {
                l_words = learned_words.Select(m =>
                {
                    var words_parts = m.Split('.');
                    return new Word(words_parts[0], words_parts[1], int.Parse(words_parts[2]));
                }).ToList();
            }
            if (words_in_process != null)
            {
                p_words = words_in_process.Select(m =>
                {
                    var words_parts = m.Split('.');
                    return new Word(words_parts[0], words_parts[1], int.Parse(words_parts[2]));
                }).ToList();
            }
            return new User(_nickname, _id, l_words, p_words);
        }
    }
}

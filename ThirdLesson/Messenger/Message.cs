using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class Message
    {
        public User Author { get; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; }
        public Guid Id { get; }

        public Message(User author, string text, DateTimeOffset date, Guid id)
        {
            Author = author;
            Text = text;
            Date = date;
            Id = id;
        }
    }
}

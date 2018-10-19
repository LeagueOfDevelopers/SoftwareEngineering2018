using System;

namespace LeonChat
{

    public class Message
    {
        public int IdInChat { get; }
        public string Text { get; set; }
        public DateTimeOffset Time { get; }
        public Guid AuthorId;

        public Message(Guid authorId,int idInChat, string text)
        {
            IdInChat = idInChat;
            Text = text;
            Time = DateTimeOffset.Now;
            AuthorId = authorId;
        }
        
    }
}

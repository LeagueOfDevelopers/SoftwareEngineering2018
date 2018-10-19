using System;
namespace LoD_Chat
{
    public class Channel : IChat
    {
        public Channel(Guid id, Client creator, string chatName, ClientRepository members, MessagesRepository messages)
        {
            Id = id;
            Creator = creator;
            ChatName = chatName;
            Members = members;
            Messages = messages;
        }

        public Guid Id { get; }

        public Client Creator { get; }

        public string ChatName { get; set; }

        public ClientRepository Members { get; private set; }

        public MessagesRepository Messages { get; private set; }

        public void DeleteMessage(Message message)
        {
            Messages.DeleteMessage(message);
        }

        public void EditMessage(Message message, string textChanged)
        {
            var foundMessage = Messages.FindMessage(message);

            foundMessage.EditMessage(textChanged);
        }

        public void AddMessage(Message message)
        {
            Messages.AddMessage(message);
        }
    }
}

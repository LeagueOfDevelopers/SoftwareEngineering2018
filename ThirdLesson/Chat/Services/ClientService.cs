using System;

namespace Chat
{
    public class ClientService : IClientService
    {
        IUserRepository userRepa;
        IChatRepository chatRepa;

        public ClientService(IUserRepository userRepa, IChatRepository chatRepa)
        {
            this.userRepa = userRepa ?? throw new ArgumentNullException(nameof(userRepa));
            this.chatRepa = chatRepa ?? throw new ArgumentNullException(nameof(chatRepa));
        }

        public void CreateChat(Guid userId, IChat chat)
        {
            if (!userRepa.Contains(userId)) throw new Exception("User is not registered");
            chatRepa.AddChat(chat);
        }

        public void AddMessage(Guid chatId, Guid userId, string text)
        {
            foreach(IChat chat in chatRepa.Chats)
            {
                if(chat.Id == chatId)
                {
                    IUser user = chat.FindUserById(userId);
                    if (user == null) throw new Exception("User is not in chat");
                    chat.AddMessage(user.SendMessage(text));
                    return;
                }
            }
        }

        public void DeleteMessage(Guid chatId, Guid userId, Guid messageId)
        {
            foreach(IChat chat in chatRepa.Chats)
            {
                if (chat.Id == chatId)
                {
                    chat.DeleteMessage(messageId, chat.FindUserById(userId));
                }
            }
        }

        public void EditMessage(Guid chatId, Guid userId, Guid messageId, string newText)
        {
            foreach(IChat chat in chatRepa.Chats)
            {
                if (chat.Id == chatId)
                {
                    chat.EditMessage(messageId, chat.FindUserById(userId), newText);
                }
            }
        }
    }
}

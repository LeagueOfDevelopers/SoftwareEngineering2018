using System;
using System.Linq;

namespace LoD_Chat.Application
{
    public class GroupChatService : IGroupChatService
    {
        private readonly ChatRepository _chatRepository;

        public GroupChatService(ChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public void AddMessage(Guid groupChatId, Guid clientId, Message message)
        {
            var chat = _chatRepository.GetChat(groupChatId);

            if (chat.Members.DoesClientExist(clientId))
            {
                chat.AddMessage(message);
            }
            else throw new Exception("You can't send messages to this chat");
        }

        public void DeleteMessage(Guid groupChatId, Guid clientId, Message message)
        {
            var chat = _chatRepository.GetChat(groupChatId);

            if (chat.Members.DoesClientExist(clientId) && (message.Sender.Id == clientId))
            {
                chat.DeleteMessage(message);
            }
            else throw new Exception("You can't delete this message");
        }

        public void EditMessage(Guid groupChatId, Guid clientId, Message message, string textChanges)
        {
            var chat = _chatRepository.GetChat(groupChatId);

            if (chat.Members.DoesClientExist(clientId) && (message.Sender.Id == clientId))
            {
                chat.EditMessage(message, textChanges);
            }
            else throw new Exception("You can't edit this message");

        }

        
    }
}

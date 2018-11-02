using System;
namespace LoD_Chat
{
    public class ChannelService : IChannelService
    {
        private readonly ChatRepository _chatRepository;

        public ChannelService(ChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public void AddMessage(Guid ChannelId, Guid clientId, Message message)
        {
            var chat = _chatRepository.GetChat(ChannelId);

            if (chat.Creator.Id == clientId)
            {
                chat.AddMessage(message);
            }
            else throw new Exception("You can't send messages to this chat");
        }

        public void DeleteMessage(Guid ChannelId, Guid clientId, Message message)
        {
            var chat = _chatRepository.GetChat(ChannelId);

            if (chat.Creator.Id == clientId && (message.Sender.Id == clientId))
            {
                chat.DeleteMessage(message);
            }
            else throw new Exception("You can't delete this message");
        }

        public void EditMessage(Guid ChannelId, Guid clientId, Message message, string textChanges)
        {
            var chat = _chatRepository.GetChat(ChannelId);

            if (chat.Creator.Id == clientId && (message.Sender.Id == clientId))
            {
                chat.EditMessage(message, textChanges);
            }
            else throw new Exception("You can't edit this message");

        }
    }
}

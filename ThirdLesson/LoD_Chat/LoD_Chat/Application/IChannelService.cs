using System;

namespace LoD_Chat
{
    public interface IChannelService
    {
        void AddMessage(Guid ChannelId, Guid clientId, Message message);

        void EditMessage(Guid ChannelId, Guid clientId, Message message, string textChanges);

        void DeleteMessage(Guid ChannelId, Guid clientId, Message message);
    }
}

using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public interface IUserService
    {
        IEnumerable<IChat> GetUserChats(Guid getterId, Guid userId);
        Guid CreateGroupChat(Guid creatorId, string groupName);
        Guid CreatePrivateChat(Guid creatorId, Guid companionId);
        Guid CreateChannel(Guid creatorId, string channelName);
        Guid CreateUser(string name);

        void SubscribeToTheChannel(Guid userId, Guid channelId);
    }
}

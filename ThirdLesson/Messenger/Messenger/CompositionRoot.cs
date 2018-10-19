using System;
using System.Collections.Generic;

namespace Messenger
{
    public class CompositionRoot
    {
        public static CompositionRoot Create()
        {
            var privateChatService = new PrivateChatService(
                    Guid.NewGuid(),
                    new PrivateChatRepository(new List<PrivateChat>()));
            
            var groupService = new GroupService(
                    Guid.NewGuid(),
                    new GroupRepository(new List<Group>()));

            var channelService = new ChannelService(
                    Guid.NewGuid(),
                    new ChannelRepository(new List<Channel>()));

            return new CompositionRoot
            {
                PrivateChatService = privateChatService,
                GroupService = groupService,
                ChannelService = channelService
            };
        }

        public IChatService PrivateChatService { get; private set; }
        public IChatService GroupService { get; private set; }
        public IChatService ChannelService { get; private set; }
    }
}

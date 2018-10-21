using System;
using System.Collections.Generic;

namespace LoDMessenger.Application
{
    public class UserService : IUserService
    {
        public UserService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public IEnumerable<IChat> GetUserChats(Guid getterId, Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            var getter = _userRepository.GetUser(getterId);

            return user.GetChats(getter);
        }

        public Guid CreateGroupChat(Guid creatorId, string groupName)
        {
            var creator = _userRepository.GetUser(creatorId);
            var administrators = new List<IUser> { creator };
            var participants = new List<IUser> { creator };
            var groupChatId = Guid.NewGuid();
            var messages = new List<IMessage>();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);

            creator.SaveChat(groupChat);
            _chatRepository.SaveChat(groupChat);
            _userRepository.SaveUser(creator);

            return groupChatId;
        }

        public Guid CreatePrivateChat(Guid creatorId, Guid companionId)
        {
            var creator = _userRepository.GetUser(creatorId);
            var companion = _userRepository.GetUser(companionId);
            var privateChatId = Guid.NewGuid();
            var participants = new IUser[]
            {
                creator,
                companion
            };
            var messages = new List<IMessage>();
            var privateChat = new PrivateChat(
                privateChatId,
                participants,
                messages);

            creator.SaveChat(privateChat);
            companion.SaveChat(privateChat);
            _chatRepository.SaveChat(privateChat);
            _userRepository.SaveUser(creator);
            _userRepository.SaveUser(companion);

            return privateChatId;
        }

        public Guid CreateChannel(Guid creatorId, string channelName)
        {
            var creator = _userRepository.GetUser(creatorId);
            var administrators = new List<IUser> { creator };
            var channelId = Guid.NewGuid();
            var subscribers = new List<IUser>();
            var messages = new List<IMessage>();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);

            creator.SaveChat(channel);
            _chatRepository.SaveChat(channel);
            _userRepository.SaveUser(creator);

            return channelId;
        }

        public void SubscribeToTheChannel(Guid newSubscriberId, Guid channelId)
        {
            var newSubscriber = _userRepository.GetUser(newSubscriberId);
            var chat = _chatRepository.GetChat(channelId);
            if (chat is Channel channel)
                channel.Subscribe(newSubscriber);
            else
                throw new ArgumentException(
                    $"Chat with id {channelId} not a channel");
        }

        public Guid CreateUser(string name)
        {
            var newUserId = Guid.NewGuid();
            var chats = new List<IChat>();
            var newUser = new User(
                newUserId,
                name,
                chats);

            _userRepository.SaveUser(newUser);

            return newUserId;
        }

        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
    }
}

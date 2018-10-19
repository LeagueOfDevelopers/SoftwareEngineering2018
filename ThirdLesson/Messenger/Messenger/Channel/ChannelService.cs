using System;
using System.Linq;

namespace Messenger
{
    public class ChannelService : IChatService
    {
        public ChannelService(Guid id, ChannelRepository channelRepository)
        {
            Id = id;
            _channelRepository = channelRepository
                ?? throw new ArgumentNullException(nameof(channelRepository));
        }

        private ChannelRepository _channelRepository;

        public Guid Id { get; }

        public void CreateChat(IChat chat)
        {
            _channelRepository.SaveItem(chat as Channel);
        }

        public void AddMessage(Guid channelId, Guid userId, IMessage message)
        {
            var channel = _channelRepository.GetItem(channelId);

            if (!HasAccess(channel, userId))
            {
                throw new MemberAccessException("User has no rights to add messages");
            }

            if (channel.Messages.Contains(message))
            {
                throw new MemberAccessException($"Message with id {message.Id} already exists");
            }

            channel.AddMessage(message);
        }

        public void ChangeMessage(Guid channelId, Guid userId, Guid messageId, string newBody)
        {
            var channel = _channelRepository.GetItem(channelId);
            var message = channel.GetMessageById(messageId);

            if (!HasAccess(channel, userId))
            {
                throw new MemberAccessException("User has no rights to change messages");
            }

            channel.ChangeMessage(messageId, newBody);
        }

        public void DeleteMessage(Guid channelId, Guid userId, Guid messageId)
        {
            var channel = _channelRepository.GetItem(channelId);
            var message = channel.GetMessageById(messageId);

            if (!HasAccess(channel, userId))
            {
                throw new MemberAccessException("User has no rights to delete messages");
            }

            channel.DeleteMessage(messageId);
        }

        public void AddUser(Guid channelId, IUser user)
        {
            var channel = _channelRepository.GetItem(channelId);
            channel.AddUser(user);
        }

        public void AddAdmin(Guid channelId, Guid userId, IUser newAdmin)
        {
            var channel = _channelRepository.GetItem(channelId);

            if (!HasAccess(channel, userId))
            {
                throw new MemberAccessException("User can't add new admins");
            }

            channel.AddAdmin(newAdmin);
        }

        public void RemoveAdmin(Guid channelId, Guid userId, IUser oldAdmin)
        {
            var channel = _channelRepository.GetItem(channelId);

            if (!HasAccess(channel, userId))
            {
                throw new MemberAccessException("User can't add remove admins");
            }

            channel.RemoveAdmin(oldAdmin);
        }

        private bool HasAccess(Channel channel, Guid userId)
        {
            var admin = channel.Admins.FirstOrDefault(innerUser => innerUser.Id == userId);
            return channel.Admins.Contains(admin);
        }
    }
}

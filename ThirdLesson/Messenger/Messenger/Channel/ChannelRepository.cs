using System;
using System.Collections.Generic;

namespace Messenger
{
    public class ChannelRepository : IRepository<Channel>
    {
        public ChannelRepository(List<Channel> channels)
        {
            _channels = channels ?? throw new ArgumentNullException(nameof(channels));
        }

        private List<Channel> _channels;

        public IEnumerable<Channel> Items => _channels;

        public void DeleteItem(Channel channel)
        {
            _channels.Remove(channel);
        }

        public void DeleteItemById(Guid channelId)
        {
            _channels.RemoveAll(channel => channel.Id == channelId);
        }

        public Channel GetItem(Guid channelId)
        {
            return TryGetChannel(channelId) ?? throw new InvalidOperationException(
                $"Channel with id {channelId} not found");
        }

        public void SaveItem(Channel channel)
        {
            if (TryGetChannel(channel.Id) == null)
            {
                _channels.Add(channel);
            }
        }

        public void UpdateItem(Channel channel)
        {
            DeleteItemById(channel.Id);
            SaveItem(channel);
        }

        public void AddItem(Channel channel)
        {
            _channels.Add(channel);
        }

        private Channel TryGetChannel(Guid channelId)
        {
            foreach (var channel in _channels)
            {
                if (channel.Id == channelId)
                {
                    return channel;
                }
            }
            return null;
        }
    }
}

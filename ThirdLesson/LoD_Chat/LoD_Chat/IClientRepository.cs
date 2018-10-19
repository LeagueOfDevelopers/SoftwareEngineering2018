using System;
namespace LoD_Chat
{
    public interface IClientRepository
    {
        IClient[] Clients { get; }

        IClient GetClient(Guid chatId);

        void AddClient(IClient chat);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat.Application
{
    public class UserService : IUserService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void AddAdmin(Guid idOfUser, Guid idOfChat)
        {
            throw new NotImplementedException();
        }

        public void CreateChannel(List<IUser> Users, IUser user)
        {
            throw new NotImplementedException();
        }

        public void CreateGroupChat(List<IUser> Users, IUser user)
        {
           
        }

        public void CreatePrivateChat(IUser interlocutor, IUser user)
        {
            List<Message> listOfMessage = new List<Message>();
            List<IUser> users = new List<IUser>
            {
                user,
                interlocutor
            };
            PrivateChat privateChat = new PrivateChat(Guid.NewGuid(), listOfMessage, users);
            _chatRepository.SaveChat(privateChat);
        }

        public void DeleteMessage(Guid idOfChat, Guid idOfMessage, Guid idOfUser)
        {
            throw new NotImplementedException();
        }

        public void EditMessage(Guid idOfChat, Guid idOfMessage, string NewMessage, Guid idOfUser)
        {
            throw new NotImplementedException();
        }

        public void SendNewMessage(IChat chat, IUser user, string message)
        {
            throw new NotImplementedException();
        }
    }
}

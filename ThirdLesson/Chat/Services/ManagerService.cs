using System;

namespace Chat
{
    class ManagerService : IManagerService
    {
        IUserRepository userRepa;
        IChatRepository chatRepa;

        public ManagerService(IUserRepository userRepa, IChatRepository chatRepa)
        {
            this.userRepa = userRepa ?? throw new ArgumentNullException(nameof(userRepa));
            this.chatRepa = chatRepa ?? throw new ArgumentNullException(nameof(chatRepa));
        }

        public void RegisterUser(IUser user)
        {
            if (user == null) return;
            userRepa.AddUser(user);
        }
    }
}

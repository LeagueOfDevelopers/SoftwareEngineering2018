using System;

namespace Chat
{
    class CompositionRoot
    {
        public CompositionRoot Create()
        {
            UserRepository userRepa = new UserRepository();
            ChatRepository chatRepa = new ChatRepository();
            return new CompositionRoot
            (
                new ManagerService(userRepa, chatRepa),
                new ClientService(userRepa, chatRepa)
            );
        }

        public CompositionRoot(IManagerService managerService, IClientService clientService)
        {
            ManagerService = managerService ?? throw new ArgumentNullException(nameof(managerService));
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        public IManagerService ManagerService { get; private set; }
        public IClientService ClientService { get; private set; }
    }
}

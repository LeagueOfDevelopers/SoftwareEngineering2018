using System;
using System.Collections.Generic;

namespace Messenger
{
    internal class UserService : IUserService
    {
        private UserRepository userRepository;
        private ChatRepository chatRepository;
        private IUser this_user;

        public UserService(UserRepository userRepository, ChatRepository chatRepository)
        {
            this.userRepository = userRepository;
            this.chatRepository = chatRepository;
        }

        public void Identification(Guid user_id)
        {
            this_user = userRepository.Get(user_id);
        }

        public void ChangeMessage(string changed_message, Guid message_id, IChat chat)
        {
            chat.ChangeMessage(changed_message, message_id, this_user._id);
        }
        public void RemoveMessage(Guid message_id, IChat chat)
        {
            chat.RemoveMessage(message_id, this_user._id);
        }
        public void SendMessage(IMessage message, IChat chat)
        {
            chat.SendMessage(message, this_user._id);
        }

        public void AddAdminInChannel(Channel channel, IUser new_admin)
        {
            channel.AddNewAdmin(new_admin, this_user._id);
        }
        public void InvateUserInConversation(Conversation conversation, IUser new_interviewer)
        {
            conversation.AddNewUser(new_interviewer, this_user);
        }

        public void ChangeChannelName(Channel channel, string new_name)
        {
            channel.ChangeName(new_name, this_user._id);
        }
        public void ChangeConversationName(Conversation conversation, string new_name)
        {
            conversation.ChangeName(new_name, this_user._id);
        }

        public IChat CreateChannel(string name)
        {
            var channel_id = Guid.NewGuid();
            var admins = new Dictionary<Guid, IUser>() { { this_user._id, this_user } };
            var followers = admins;
            var new_channel = new Channel(channel_id, name, admins, followers, new Dictionary<Guid, IMessage>());
            chatRepository.Create(new_channel);
            return new_channel;
        }
        public IChat CreateDialogue(IUser interlocutor)
        {
            var dialogue_id = Guid.NewGuid();
            var new_dialogue = new Dialogue(dialogue_id, this_user, interlocutor, new Dictionary<Guid, IMessage>());
            chatRepository.Create(new_dialogue);
            return new_dialogue;
        }
        public IChat CreateConversation(List<IUser> users, string name)
        {
            var conversation_id = Guid.NewGuid();
            var interviewers = new Dictionary<Guid, IUser>();
            foreach (IUser user in users)
            {
                interviewers.Add(user._id, user);
            }
            var new_conversation = new Conversation(interviewers, new Dictionary<Guid, IMessage>(), name, conversation_id);
            chatRepository.Create(new_conversation);
            return new_conversation;
        }

        public void JoinChannel(Channel chanel)
        {
            chanel.Join(this_user);
        }
        public List<IMessage> GetMessages(IChat chat)
        {
            return chat.GetMessages(this_user);
        }

        public IMessage GetLastMessages(IChat chat)
        {
            return chat.GetLastMessages(this_user);
        }


    }
}
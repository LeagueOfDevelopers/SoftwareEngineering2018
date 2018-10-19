using System;
using System.Collections;

namespace LeonChat
{
    public class User
    {
        public Guid Id;

        public User(Guid id)
        {
            Id = id;
        }

        public bool EditMessage(IChat chat, int messageId, string newString)
        {
            try
            {
                chat.EditMessage(Id, messageId, newString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable GetMessages(IChat chat)
        {
            try
            {
                return chat.GetMessages(Id);
            }
            catch (Exception)
            {
                return Array.Empty<Message>();
            }
        }

        public bool SendMessage(IChat chat, string messageString)
        {
            try
            {
                chat.AddMessage(Id, messageString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMessage(IChat chat, int messageId)
        {
            try
            {
                chat.DeleteMessage(Id, messageId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool AddParticipant(IChat chat, Guid newParticipant)
        {
            try
            {
                chat.AddParticipant(Id, newParticipant);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveParticipant(IChat chat, Guid removeParticipant)
        {
            try
            {
                chat.RemoveParticipant(Id, removeParticipant);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
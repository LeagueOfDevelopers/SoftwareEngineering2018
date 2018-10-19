using System;
using System.Collections;
using System.Collections.Generic;

namespace LeonChat
{
    public class PersonalChat : IChat
    {
        List<Guid> ParticipantsIds = new List<Guid>();
        List<Message> Messages = new List<Message>();

        public PersonalChat(Guid peerOne, Guid peerTwo, IEnumerable<Message> messages)
        {

            ParticipantsIds.Add(peerOne);
            ParticipantsIds.Add(peerTwo);

            Messages.AddRange(messages);
        }

        public void AddMessage(Guid initiatorId, string msg)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            int newId = Messages.Count;

            Messages.Add(new Message(initiatorId, newId, msg));
        }

        public void AddParticipant(Guid initiatorId, Guid id)
        {
            if (ParticipantsIds.Count == 2) throw new NotImplementedException("Transform from personal chat to group");
        }

        public void DeleteMessage(Guid initiatorId, int msgId)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            Message deleteMessage = Messages.Find((Message message) => message.IdInChat == msgId) ?? throw  new Exception("No such id in chat messages");

            if (deleteMessage.AuthorId != initiatorId) throw new Exception("Cannot delete others messages");

            Messages.Remove(deleteMessage);
        }

        public void EditMessage(Guid initiatorId, int msgId, string newText)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            Message editMessage = Messages.Find((Message message) => message.IdInChat == msgId);

            if (editMessage.AuthorId != initiatorId) throw new Exception("Cannot edit others messages");

            editMessage.Text = newText;
        }

        public IEnumerable GetMessages(Guid initiatorId)
        {
            return Messages;
        }

        public IEnumerable GetParticipants(Guid initiatorId)
        {
            return ParticipantsIds;
        }

        public bool IsAdmin(Guid checkId)
        {
            throw new Exception("No admins in personal chat");
        }

        public bool IsParticipant(Guid checkId)
        {
            return ParticipantsIds.Contains(checkId);
        }

        public void RemoveParticipant(Guid initiatorId, Guid id)
        {
            throw new Exception("Cannot remove user from personal chat");
        }
    }
}

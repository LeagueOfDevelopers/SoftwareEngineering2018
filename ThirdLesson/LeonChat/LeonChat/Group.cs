using System;
using System.Collections;
using System.Collections.Generic;

namespace LeonChat
{
    public class Group : IChat
    {
        List<Guid> AdminsIds = new List<Guid>();
        List<Guid> ParticipantsIds = new List<Guid>();
        List<Message> Messages = new List<Message>();

        public Group(IEnumerable<Guid> admins, IEnumerable<Guid> participants, IEnumerable<Message> messages)
        {
            AdminsIds.AddRange(admins);
            ParticipantsIds.AddRange(participants);
            Messages.AddRange(messages);
        }

        public void AddMessage(Guid initiatorId, string msg)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            int newId = Messages.Count;

            Messages.Add(new Message(initiatorId, newId, msg));
        }

        public void AddParticipant(Guid initiatorId, Guid addId)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            ParticipantsIds.Add(addId);
        }

        public void DeleteMessage(Guid initiatorId, int msgId)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            Message deleteMessage = Messages.Find((Message message) => message.IdInChat == msgId) ?? throw  new Exception("No such id in chat messages");

            if (deleteMessage.AuthorId != initiatorId && !IsAdmin(initiatorId)) throw new Exception("Forbidden");

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
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            return Messages;
        }

        public IEnumerable GetParticipants(Guid initiatorId)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            return ParticipantsIds;
        }

        public bool IsAdmin(Guid checkId)
        {
            return AdminsIds.Contains(checkId);
        }

        public bool IsParticipant(Guid checkId)
        {
            return ParticipantsIds.Contains(checkId);
        }

        public void RemoveParticipant(Guid initiatorId, Guid deleteId)
        {
            if (!IsAdmin(initiatorId) && initiatorId != deleteId) throw new Exception("Forbidden");

            ParticipantsIds.Remove(deleteId);

            if (IsAdmin(initiatorId)) AdminsIds.Remove(deleteId);
        }
    }
}
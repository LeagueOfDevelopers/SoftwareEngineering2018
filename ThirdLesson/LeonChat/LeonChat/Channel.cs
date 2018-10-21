using System;
using System.Collections;
using System.Collections.Generic;

namespace LeonChat
{
    public class Channel : IChat
    {
        List<Guid> AdminsIds = new List<Guid>();
        List<Guid> ParticipantsIds = new List<Guid>();
        List<Message> Messages = new List<Message>();

        public Channel(IEnumerable<Guid> adminIds, IEnumerable<Guid> participantsIds, IEnumerable<Message> messages)
        {
            AdminsIds.AddRange(adminIds);

            ParticipantsIds.AddRange(participantsIds);

            Messages.AddRange(messages);

        }

        public void AddMessage(Guid initiatorId, string msg)
        {
            if (!IsAdmin(initiatorId)) throw new Exception("Forbidden");

            int newId = Messages.Count;

            Messages.Add(new Message(initiatorId, newId, msg));
        }

        public void AddParticipant(Guid initiatorId, Guid newId)
        {
            if (initiatorId != newId) throw new Exception("Connot add other user.");

            ParticipantsIds.Add(newId);
        }

        public void DeleteMessage(Guid initiatorId, int msgId)
        {
            if (!IsAdmin(initiatorId)) throw new Exception("Forbidden");

            Message deleteMessage = Messages.Find((Message message) => message.IdInChat == msgId) ?? throw  new Exception("No such id in chat messages");
            
            Messages.Remove(deleteMessage);
        }

        public void EditMessage(Guid initiatorId, int msgId, string newText)
        {
            if (!IsAdmin(initiatorId)) throw new Exception("Forbidden");

            Message message = Messages.Find((Message msg) => msg.IdInChat == msgId);
            message.Text = newText;
        }

        public IEnumerable GetMessages(Guid initiatorId)
        {
            if (!IsParticipant(initiatorId)) throw new Exception("Forbidden");

            return Messages;
        }

        public IEnumerable GetParticipants(Guid initiatorId)
        {
            if (!IsAdmin(initiatorId)) throw new Exception("Forbidden");

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

            if (IsAdmin(deleteId)) AdminsIds.Remove(deleteId);
        }
    }
}

using System;
using System.Collections;

namespace LeonChat
{
    public interface IChat
    {
        void AddMessage(Guid initiatorId, string msg);
        void DeleteMessage(Guid initiatorId, int msgId);
        void EditMessage(Guid initiatorId, int msgId, string newText);
        void AddParticipant(Guid initiatorId, Guid id);
        void RemoveParticipant(Guid initiatorId, Guid id);
        IEnumerable GetMessages(Guid initiatorId);
        IEnumerable GetParticipants(Guid initiatorId);
        bool IsAdmin(Guid checkId);
        bool IsParticipant(Guid checkId);
    }
}
using System;
using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IFinishSessionService
    {
        void FinishSession(Guid traineeUserId, Guid sessionId, IEnumerable<Answer> answers);
    }
}

using System;

namespace LeagueGram.Domain
{
  public interface IPublicChat : IChat
  {
    Guid CreatorId { get; }

    void InviteMember(Guid actorId, Guid targetUserId, string nickname);
    void PromoteToAdmin(Guid actorId, Guid targetUserId);
    void DemoteFromAdmin(Guid actorId, Guid targetUserId);
  }
}
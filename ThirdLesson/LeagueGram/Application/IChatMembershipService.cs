using System;

namespace LeagueGram.Application
{
  public interface IChatMembershipService
  {
    void InviteMemberToChat(Guid chatId, Guid invitingUserId, Guid invitedUserId);

    void PromoteMemberToAdmin(Guid chatId, Guid actorId, Guid targetId);

    void DemoteUserFromAdmin(Guid chatId, Guid actorId, Guid targetId);
  }
}
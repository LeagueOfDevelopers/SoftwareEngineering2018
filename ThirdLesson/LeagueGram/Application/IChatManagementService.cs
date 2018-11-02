using System;

namespace LeagueGram.Application
{
  public interface IChatManagementService
  {
    Guid CreatePrivateChat(Guid creatorId, Guid companionId);

    Guid CreateGroup(Guid creatorId);

    Guid CreateChannel(Guid creatorId);
  }
}
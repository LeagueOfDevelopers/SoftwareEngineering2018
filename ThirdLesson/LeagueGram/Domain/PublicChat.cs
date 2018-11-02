using System;
using System.Collections.Generic;
using LeagueGram.Domain.Exception;

namespace LeagueGram.Domain
{
  public abstract class PublicChat : Chat, IPublicChat
  {
    protected PublicChat(
      Guid id, 
      Guid creatorId, 
      IEnumerable<Message> messages, 
      IEnumerable<ChatMember> members)
      : base(id, messages, members)
    {
      CreatorId = creatorId;
    }

    public void InviteMember(Guid actorId, Guid targetUserId, string nickname)
    {
      if (GetMember(targetUserId) != null)
      {
        return;
      }

      var actor = GetMember(actorId);
      if (actor == null)
      {
        throw new UserNotFoundException(actorId);
      }

      if (!CanInvite(actor))
      {
        throw new InsufficientRightsException(actorId, nameof(InviteMember));
      }

      var newChatMember = new ChatMember(targetUserId, nickname, ChatMemberRole.User);
      var memberList = new List<ChatMember>(Members);
      memberList.Add(newChatMember);
      Members = memberList;
    }

    public void PromoteToAdmin(Guid actorId, Guid targetUserId)
    {
      var actor = GetMember(actorId);
      if (actor == null)
      {
        throw new UserNotFoundException(actorId);
      }

      var targetMember = GetMember(targetUserId);
      if (targetMember == null)
      {
        throw new UserNotFoundException(actorId);
      }

      if (!CanPromote(actor))
      {
        throw new InsufficientRightsException(actorId, nameof(PromoteToAdmin));
      }

      targetMember.PromoteToAdmin();
    }

    public void DemoteFromAdmin(Guid actorId, Guid targetUserId)
    {
      var actor = GetMember(actorId);
      if (actor == null)
      {
        throw new UserNotFoundException(actorId);
      }

      var targetMember = GetMember(targetUserId);
      if (targetMember == null)
      {
        throw new UserNotFoundException(actorId);
      }

      if (!CanDemote(actor) || (actorId == targetUserId))
      {
        throw new InsufficientRightsException(actorId, nameof(DemoteFromAdmin));
      }

      targetMember.DemoteFromAdmin();
    }

    public Guid CreatorId { get; }

    protected abstract bool CanInvite(ChatMember chatMember);
    protected abstract bool CanPromote(ChatMember chatMember);
    protected abstract bool CanDemote(ChatMember chatMember);
  }
}
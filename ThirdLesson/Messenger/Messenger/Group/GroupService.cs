using System;
using System.Linq;

namespace Messenger
{
    public class GroupService : IChatService
    {
        public GroupService(Guid id, GroupRepository groupRepository)
        {
            Id = id;
            _groupRepository = groupRepository
                ?? throw new ArgumentNullException(nameof(groupRepository));
        }

        private GroupRepository _groupRepository { get; }

        public Guid Id { get; }

        public void AddMessage(Guid groupId, Guid userId, IMessage message)
        {
            var group = _groupRepository.GetItem(groupId);

            if (group.Messages.Contains(message))
            {
                throw new MemberAccessException($"Message with id {message.Id} already exists");
            }

            if (group.Users.ToList().FindAll(user => user.Id == userId) != null)
            {
                group.AddMessage(message);
            }
        }

        public void ChangeMessage(Guid groupId, Guid userId, Guid messageId, string newBody)
        {
            var group = _groupRepository.GetItem(groupId);
            var message = group.GetMessageById(messageId);

            if (!CanUpdateMessage(userId, message))
            {
                throw new MemberAccessException($"User {userId} can't delete foreign message ${message.Id}");
            }

            group.ChangeMessage(messageId, newBody);
        }

        public void DeleteMessage(Guid groupId, Guid userId, Guid messageId)
        {
            var group = _groupRepository.GetItem(groupId);
            var message = group.GetMessageById(messageId);

            if (!CanDeleteMessage(group, userId, message))
            {
                throw new MemberAccessException($"User {userId} can't delete foreign message ${message.Id}");
            }

            group.DeleteMessage(messageId);
        }

        public void CreateChat(IChat group)
        {
            _groupRepository.SaveItem(group as Group);
        }

        public void AddUser(Guid groupId, Guid userId, IUser user)
        {
            var group = _groupRepository.GetItem(groupId);

            if (!IsAdmin(group, userId))
            {
                throw new MemberAccessException($"User {userId} can't add users to ${groupId}");
            }

            group.AddUser(user);
        }

        public void AddAdmin(Guid groupId, Guid userId, IUser newAdmin)
        {
            var group = _groupRepository.GetItem(groupId);

            if (!IsAdmin(group, userId))
            {
                throw new MemberAccessException($"User {userId} can't add admins");
            }

            group.AddAdmin(newAdmin);
        }

        public void RemoveAdmin(Guid groupId, Guid userId, IUser oldAdmin)
        {
            var group = _groupRepository.GetItem(groupId);

            if (!IsAdmin(group, userId))
            {
                throw new MemberAccessException($"User {userId} can't remove admins");
            }

            group.RemoveAdmin(oldAdmin);
        }

        private bool CanUpdateMessage(Guid userId, IMessage message)
        {
            return message.CreatorId == userId;
        }

        private bool CanDeleteMessage(Group group, Guid userId, IMessage message)
        {
            var user = group.Admins.FirstOrDefault(innerUser => innerUser.Id == userId);
            return (message.CreatorId == userId) || (group.Admins.Contains(user));
        }

        private bool IsAdmin(Group group, Guid userId)
        {
            var user = group.Admins.FirstOrDefault(innerUser => innerUser.Id == userId);
            return group.Admins.Contains(user);
        }
    }
}

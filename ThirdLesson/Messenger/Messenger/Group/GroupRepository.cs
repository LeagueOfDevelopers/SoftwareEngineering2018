using System;
using System.Collections.Generic;

namespace Messenger
{
    public class GroupRepository : IRepository<Group>
    {
        public GroupRepository(List<Group> groups)
        {
            _groups = groups ?? throw new ArgumentNullException(nameof(groups));
        }

        private List<Group> _groups;

        public IEnumerable<Group> Items => _groups;

        public void DeleteItem(Group group)
        {
            _groups.Remove(group);
        }

        public void DeleteItemById(Guid groupId)
        {
            _groups.RemoveAll(group => group.Id == groupId);
        }

        public Group GetItem(Guid groupId)
        {
            return TryGetGroup(groupId) ?? throw new InvalidOperationException(
                $"Group with id {groupId} not found");
        }

        public void SaveItem(Group group)
        {
            if (TryGetGroup(group.Id) == null)
            {
                _groups.Add(group);
            }
        }

        public void UpdateItem(Group group)
        {
            DeleteItemById(group.Id);
            SaveItem(group);
        }

        public void AddItem(Group item)
        {
            _groups.Add(item);
        }

        private Group TryGetGroup(Guid groupId)
        {
            foreach (var group in _groups)
            {
                if (group.Id == groupId)
                {
                    return group;
                }
            }
            return null;
        }
    }
}

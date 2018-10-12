using System.Collections.Generic;

namespace CarRent
{
    class UserRepository : IRepository<User>
    {
        private List<User> _user_list;

        public UserRepository()
        {
            _user_list = new List<User>();
        }

        public UserRepository(List<User> user_list)
        {
            _user_list = user_list;
        }

        public void Create(User user)
        {
            _user_list.Add(user);

        }

        public void Delete(int id)
        {
            _user_list.RemoveAt(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _user_list;
        }
    }
}

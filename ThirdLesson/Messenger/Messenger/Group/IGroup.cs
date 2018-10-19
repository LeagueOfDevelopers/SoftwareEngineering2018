namespace Messenger
{
    public interface IGroup
    {
        void AddUser(IUser user);

        void RemoveUser(IUser user);

        void AddAdmin(IUser newAdmin);

        void RemoveAdmin(IUser oldAdmin);
    }
}

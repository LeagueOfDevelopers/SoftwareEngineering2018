namespace Messenger
{
    public interface IChannel
    {
        void AddUser(IUser user);

        void RemoveUser(IUser user);

        void AddAdmin(IUser newAdmin);

        void RemoveAdmin(IUser oldAdmin);
    }
}

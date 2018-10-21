using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public interface IChatRepository
    {
        IEnumerable<IChat> Chats { get; }
        IChat GetChat(Guid idOfChat);
        void SaveChat(IChat chat);
    }
}

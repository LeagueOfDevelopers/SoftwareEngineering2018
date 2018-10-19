using System;
using System.Collections.Generic;
using System.Linq;


namespace Messenger
{
   public class ChatRepository : IChatRepository
   {
      private readonly List<IChat> _chats = new List<IChat>();

      public bool Exists(IChat chat)
      {
         return _chats.Find(cht => cht == chat) != null;
      }

      public bool ExistsPrivate(User fParticipant, User sParticipant)
      {
         return GetPrivates().Find(chat => chat.FParticipant == fParticipant &&
                                           chat.SParticipant == sParticipant) != null;
      }

      public void Add(IChat chat)
      {
         _chats.Add(chat);
      }

      public void Remove(IChat chat)
      {
         _chats.Remove(chat);
      }

      public List<IChat> GetAll()
      {
         return _chats;
      }

      private List<Private> GetPrivates()
      {
         return _chats.FindAll(chat => chat is Private).ConvertAll(obj => (Private) obj);
      }
   }
}
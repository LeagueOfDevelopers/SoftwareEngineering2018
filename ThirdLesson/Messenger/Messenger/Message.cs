using System;

namespace Messenger
{
   public class Message
   {
      public Guid   Id    { get; }
      public Guid   Owner { get; }
      public string Text  { get; private set; }

      public Message(Guid id, Guid owner, string text)
      {
         Id    = id;
         Owner = owner;
         Text  = text;
      }

      public void Edit(string text)
      {
         Text = text;
      }
   }
}
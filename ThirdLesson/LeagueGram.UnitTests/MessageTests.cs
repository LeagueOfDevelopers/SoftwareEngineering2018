using System;
using LeagueGram.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeagueGram.UnitTests
{
  [TestClass]
  public class MessageTests
  {
    [TestMethod]
    public void EditMessage_ChangesText()
    {
      var message = new Message(
        Guid.NewGuid(), 
        "original text",
        Guid.NewGuid(), 
        DateTimeOffset.UtcNow);
      var editedText = "edited text";

      message.Edit(editedText);

      Assert.AreEqual(editedText, message.Text);
    }
  }
}

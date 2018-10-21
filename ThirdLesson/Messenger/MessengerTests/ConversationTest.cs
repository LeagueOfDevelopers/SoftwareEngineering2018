using System;
using System.Collections.Generic;
using Messenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessengerTests
{
    [TestClass]
    public class ConversationTest
    {
        private Conversation CreateChannelWithOneInterviewer()
        {
            var interviewer = new User("igor", Guid.Empty);
            var testСonversation = new Conversation(new Dictionary<Guid, IUser>() { { interviewer._id, interviewer } }, new Dictionary<Guid, IMessage>(), "test", Guid.Empty); ;
            return testСonversation;
        }


        [TestMethod]
        public void InterviewerChangeName_NewConversationName()
        {
            var interviewer = new User("igor", Guid.Empty);
            var conversation = CreateChannelWithOneInterviewer();

            conversation.ChangeName("new name", interviewer._id);
            var expected_name = "new name";

            Assert.AreEqual(conversation._name, expected_name);
        }
        [TestMethod]
        public void NotInterviewerChangeName_Exception()
        {
            var not_interviewer = new User("igor", Guid.NewGuid());
            var conversation = CreateChannelWithOneInterviewer();

            Assert.ThrowsException<InvalidOperationException>(() => conversation.ChangeName("new name", not_interviewer._id));
        }

        [TestMethod]
        public void NotInterviewerAddingNewUser_Exception()
        {
            var not_interviewer = new User("igor", Guid.NewGuid());
            var adding_user = new User("ivan", Guid.NewGuid()); 
            var conversation = CreateChannelWithOneInterviewer();

            Assert.ThrowsException<InvalidOperationException>(() => conversation.AddNewUser(adding_user, not_interviewer));
        }
        [TestMethod]
        public void InterviewerAddingNewUser_NewUserInConversation()
        {
            var interviewer = new User("igor", Guid.Empty);
            var adding_user = new User("ivan", Guid.NewGuid());
            var conversation = CreateChannelWithOneInterviewer();
            var expected_conversation =  new Conversation(new Dictionary<Guid, IUser>() { { interviewer._id, interviewer }, { adding_user._id, adding_user} }, new Dictionary<Guid, IMessage>(), "test", Guid.Empty); ;

            Assert.ReferenceEquals(conversation, expected_conversation);
        }




    }
}

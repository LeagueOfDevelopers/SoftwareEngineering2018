using System;
using System.Collections.Generic;
using Messenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessengerTests
{
    [TestClass]
    public class WorkinWithMessageTest
    {
        private Dialogue CreateDialogue()
        {
            User user_1 = new User("igor", Guid.NewGuid());
            User user_2 = new User("ivan", Guid.NewGuid());
            var message = new Message("text",Guid.Empty, user_1, DateTimeOffset.MinValue);
            var testDialogue = new Dialogue(Guid.Empty, user_1, user_2, new Dictionary<Guid, IMessage>() { { message._id, message } });
            return testDialogue;
        }
        private Channel CreateChannelWithOneAdmin()
        {            
            User admin = new User("admin", Guid.Empty);
            var message = new Message("text", Guid.Empty, admin, DateTimeOffset.MinValue);
            Channel testChannel = new Channel(Guid.Empty, "test", new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IMessage>() { { message._id, message } });
            return testChannel;
        }
        private Conversation CreateConversationWithOneInterviewer()
        {
            var interviewer = new User("igor", Guid.Empty);
            var message = new Message("text", Guid.Empty, interviewer, DateTimeOffset.Now);
            var testСonversation = new Conversation(new Dictionary<Guid, IUser>() { { interviewer._id, interviewer } }, new Dictionary<Guid, IMessage>() { { message._id, message} }, "test", Guid.Empty); ;
            return testСonversation;
        }

        [TestMethod]
        public void UserTryToChangeMessageAnotherUserInDialogue_Exception()
        {
            var dialogue = CreateDialogue();
            var new_message = "new_text";

            Assert.ThrowsException<InvalidOperationException>(() => dialogue.ChangeMessage(new_message, Guid.Empty,dialogue.second_user._id ));


        }
        [TestMethod]
        public void UserTryToChangeMessageAdminInChannel_Exception()
        {
            var admin = new User("admin", Guid.Empty);
            var channel = CreateChannelWithOneAdmin();
            var current_user = new User("sasha", Guid.NewGuid());
            var new_message = "new_text";

            Assert.ThrowsException<InvalidOperationException>(() => channel.ChangeMessage(new_message, Guid.Empty, current_user._id));


        }
    
        [TestMethod]
        public void UserTryToChangeMessageAnotherUserInConversation_Exception()
        {
            var interviewer = new User("igor", Guid.Empty);
            var conversation = CreateConversationWithOneInterviewer();
            var current_user = new User("sasha", Guid.NewGuid());
            var new_message = "new_text";

            Assert.ThrowsException<InvalidOperationException>(() => conversation.ChangeMessage(new_message, Guid.Empty, current_user._id));


        }

        [TestMethod]
        public void UserTryToChangeOwnMessageInDialogue_ChangedMessage()
        {
            var dialogue = CreateDialogue();
            var new_message = new Message("new_text", Guid.Empty, dialogue.first_user, DateTimeOffset.MinValue);

            var expected_dialogue = new Dialogue(Guid.Empty,new User("igor", Guid.NewGuid()), new User("ivan", Guid.NewGuid()), new Dictionary<Guid, IMessage>() { { Guid.Empty, new_message } });

            Assert.ReferenceEquals(dialogue, expected_dialogue);

        }
        [TestMethod]
        public void UserTryToChangeOwnMessageInСhannel_ChangedMessage()
        {
            var admin = new User("admin", Guid.Empty);
            var channel = CreateChannelWithOneAdmin();
            var new_message = "new_text";
            channel.ChangeMessage(new_message, Guid.Empty, admin._id);

            var expected_channel = new Channel(Guid.Empty, "test", new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IMessage>() { { Guid.Empty, new Message("new text", Guid.Empty, admin, DateTimeOffset.MinValue) } });

            Assert.ReferenceEquals(channel, expected_channel) ;

        }
        [TestMethod]
        public void UserTryToChangeOwnMessageInConversation_ChangedMessage()
        {
            var interviewer = new User("igor", Guid.Empty);
            var conversation = CreateConversationWithOneInterviewer();
            var new_message = "new_text";

            conversation.ChangeMessage(new_message, Guid.Empty, interviewer._id);

            var expected_conversation = new Conversation(new Dictionary<Guid, IUser>() { { interviewer._id, interviewer } }, new Dictionary<Guid, IMessage>() { { Guid.Empty, new Message(new_message, Guid.Empty, interviewer, DateTimeOffset.MinValue) } }, "test", Guid.Empty);


            Assert.ReferenceEquals(conversation, expected_conversation);

        }
        [TestMethod]
        public void UserTryToRemoveMessageAnotherUserInDialogue_Exception()
        {
            var dialogue = CreateDialogue();

            Assert.ThrowsException<InvalidOperationException>(() => dialogue.RemoveMessage(Guid.Empty, dialogue.second_user._id));


        }
        [TestMethod]
        public void UserTryToRemoveMessageAdminInChannel_Exception()
        {
            var admin = new User("admin", Guid.Empty);
            var channel = CreateChannelWithOneAdmin();
            var current_user = new User("sasha", Guid.NewGuid());

            Assert.ThrowsException<InvalidOperationException>(() => channel.RemoveMessage(Guid.Empty, current_user._id));


        }
        [TestMethod]
        public void UserTryToRemoveMessageAnotherUserInConversation_Exception()
        {
            var interviewer = new User("igor", Guid.Empty);
            var conversation = CreateConversationWithOneInterviewer();
            var current_user = new User("sasha", Guid.NewGuid());

            Assert.ThrowsException<InvalidOperationException>(() => conversation.RemoveMessage(Guid.Empty, current_user._id));


        }

        [TestMethod]
        public void UserTryToRemoveOwnMessageInDialogue_RemovedMessage()
        {
            var dialogue = CreateDialogue();
            dialogue.RemoveMessage(Guid.Empty, dialogue.first_user._id);

            var expected_dialogue = new Dialogue(Guid.Empty, new User("igor", Guid.NewGuid()), new User("ivan", Guid.NewGuid()), new Dictionary<Guid, IMessage>());

            Assert.ReferenceEquals(dialogue, expected_dialogue);

        }
        [TestMethod]
        public void AdminTryToRemoveOwnMessageInСhannel_RemovedMessage()
        {
            var admin = new User("admin", Guid.Empty);
            var channel = CreateChannelWithOneAdmin();
            channel.RemoveMessage(Guid.Empty, admin._id);

            var expected_channel = new Channel(Guid.Empty, "test", new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IMessage>());

            Assert.ReferenceEquals(channel, expected_channel);

        }

        [TestMethod]
        public void UserTryToRemoveOwnMessageInСhannel_Excaption()
        {
            var current_user = new User("ivan", Guid.NewGuid());
            var message = new Message("text", Guid.NewGuid(), current_user, DateTimeOffset.MinValue);
            var channel = new Channel(Guid.Empty, "test", new Dictionary<Guid, IUser>(), new Dictionary<Guid, IUser>() { {current_user._id, current_user } }, new Dictionary<Guid, IMessage>() { { message._id, message } });

            Assert.ThrowsException<InvalidOperationException>(() => channel.RemoveMessage( message._id, current_user._id));


        }
        [TestMethod]
        public void UserTryToRemoveOwnMessageInConversation_RemovedMessage()
        {
            var interviewer = new User("igor", Guid.Empty);
            var conversation = CreateConversationWithOneInterviewer();

            conversation.RemoveMessage(Guid.Empty, interviewer._id);

            var expected_conversation = new Conversation(new Dictionary<Guid, IUser>() { { interviewer._id, interviewer } }, new Dictionary<Guid, IMessage>(), "test", Guid.Empty);


            Assert.ReferenceEquals(conversation, expected_conversation);

        }

    }
}

using Messenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MessengerTests
{
    [TestClass]
    public class ChannelTest
    {
        private Channel CreateChannelWithOneAdmin()
        {
            Guid admin_id = Guid.Empty;
            User admin = new User("admin", admin_id);
            Guid channel_id = Guid.Empty;
            Channel testChannel = new Channel(channel_id, "test", new Dictionary<Guid, IUser>() { { admin_id, admin } }, new Dictionary<Guid, IUser>() { { admin_id, admin } }, new Dictionary<Guid, IMessage>());
            return testChannel;
        }

        [TestMethod]
        public void AdminAddNewAdmin_NewAdminInAdminDictionary()
        {
            var admin_id = Guid.Empty;
            var admin = new User("admin", admin_id);
            var channel = CreateChannelWithOneAdmin();

            var new_admin_id = Guid.NewGuid();
            var new_admin = new User("new admin", new_admin_id);

            channel.AddNewAdmin(new_admin, admin_id);
            var ex_channel = new Channel(Guid.Empty, "test", new Dictionary<Guid, IUser>() { { admin_id, admin }, { new_admin_id, new_admin } }, new Dictionary<Guid, IUser>() { { admin_id, admin }, { new_admin_id, new_admin } }, new Dictionary<Guid, IMessage>());
            Assert.ReferenceEquals(channel, ex_channel);
        }

        [TestMethod]
        public void NotAdminAddNewAdmin_WithoutChanging()
        {
            var admin_id = Guid.Empty;
            var admin = new User("admin", admin_id);
            var channel = CreateChannelWithOneAdmin();
            var new_admin_id = Guid.NewGuid();
            var new_admin = new User("new admin", new_admin_id);

            var current_user = new User("user", Guid.NewGuid());

            Assert.ThrowsException<InvalidOperationException>(() => channel.AddNewAdmin(new_admin, current_user._id));
        }
        [TestMethod]
        public void AdminChangeChannelName_NewChannelName()
        {            
            var admin = new User("admin", Guid.Empty);
            var channel = CreateChannelWithOneAdmin();

            channel.ChangeName("new name", admin._id);
            var ex_channel = new Channel(Guid.Empty, "new name", new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IUser>() { { admin._id, admin } }, new Dictionary<Guid, IMessage>());
            
            Assert.ReferenceEquals(channel, ex_channel);
        }
        [TestMethod]
        public void NotAdminChangeChannelName_ReturnInvalidOperationException()
        {
            var admin = new User("admin", Guid.Empty);
            var channel = CreateChannelWithOneAdmin();
            var current_user = new User("user", Guid.NewGuid());
            
            Assert.ThrowsException<InvalidOperationException>(() => channel.ChangeName("new name", current_user._id));
        }
        

    }
}

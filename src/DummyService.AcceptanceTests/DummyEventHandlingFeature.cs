using DummyService.AcceptanceTests.Data;
using DummyService.AcceptanceTests.Data.Entities;
using FluentAssertions;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;
using Xunit;

namespace DummyService.AcceptanceTests
{
    [Story(
        Title = "DummyEvent is handled",
        AsA = "As a dummy service",
        IWant = "I want to handle dummy event",
        SoThat = "So that I can get message data inserted in database")]
    public class DummyEventHandlingFeature
    {
        private IDictionary<string, string> _inMemoryStorage = new Dictionary<string, string>();

        [Fact]
        public void Vanilla()
        {
            this.Given(_ => IHaveServiceRunning())
                .When(_ => ISendDummyEvent())
                .Then(_ => IShouldGetMessageDataInsertedInDatabase())
                .BDDfy("Vanilla case");
        }

        private void IHaveServiceRunning()
        {
            DeleteAllExistingData();
        }

        private void ISendDummyEvent()
        {
            var text = "Some dummy text";
            var message = new Message(System.Text.Encoding.UTF8.GetBytes(text));
            SendMessage(message);
            _inMemoryStorage.Add("text", text);
        }

        private void SendMessage(Message message)
        {
            var connectionString = ConfigurationReader.GetConfigValueFor("EndpointConfiguration:ConnectionString");
            var topic = ConfigurationReader.GetConfigValueFor("EndpointConfiguration:Topic");

            var client = new TopicClient(connectionString, topic);
            client.SendAsync(message).GetAwaiter().GetResult();
        }

        private void IShouldGetMessageDataInsertedInDatabase()
        {
            var expectedText = _inMemoryStorage["text"];

            var messageData = GetMessageData();

            messageData.Should().NotBeNull();
            messageData.MessageText.Should().Be(expectedText);

            DeleteAllExistingData();
        }

        private MessageData GetMessageData()
        {
            var options = DbHelper.GetDbContextOptions();
            using (var context = new DummyDbContext(options))
            {
                return context.MessageDatas.FirstOrDefault();
            }
        }

        private static void DeleteAllExistingData()
        {
            var options = DbHelper.GetDbContextOptions();
            using (var context = new DummyDbContext(options))
            {
                context.MessageDatas.RemoveRange(context.MessageDatas);
                context.SaveChanges();
            }
        }
    }
}

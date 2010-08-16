using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceBus;
using NServiceBus.Unicast.Transport;

namespace System.Quality
{
    public class TestMessage : IServiceMessage
    {
        public string Name { get; set; }
    }

    [TestClass]
    public class NServiceBusServiceBusTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ServiceBusManager.SetBusProvider(() => new NServiceBusServiceBus());
            //ServiceBusManager.SetBusProvider(() => new NServiceBusServiceBus(Configure.With(typeof(CompletionMessage).Assembly)
            //    .DefaultBuilder()
            //    .XmlSerializer()
            //    .UnicastBus()
            //    .LoadMessageHandlers()
            //    .CreateBus()
            //    .Start()));
        }

        [TestMethod]
        public void TestMethod1()
        {
            var testMessage = ServiceBus.MakeMessage<TestMessage>();
            testMessage.Name = "Test";
            ServiceBus.Send(testMessage);
        }
    }
}

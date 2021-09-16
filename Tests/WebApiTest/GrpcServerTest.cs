using GoogleGrpc;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;
using static GoogleGrpc.Greeter;

namespace Tests.WebApiTest
{
    [TestFixture]
    public class GrpcServerTest
    {
        const string Host = "localhost";
        Server server;
        Channel channel;
        GreeterClient client;

        [OneTimeSetUp]
        public void Init()
        {
            var mock = new Mock<ILogger<GreeterService>>();
            ILogger<GreeterService> logger = mock.Object;
            server = new Server(new[] { new ChannelOption(ChannelOptions.SoReuseport, 0) })
            {
                Services = { BindService(new GreeterService(logger)) },
                Ports = { { Host, ServerPort.PickUnused, ServerCredentials.Insecure } }
            };

            server.Start();
            channel = new Channel(Host, server.Ports.Single().BoundPort, ChannelCredentials.Insecure);
            client = new GreeterClient(channel);
        }

        [Test]
        public void SayHelloOscarTest()
        {
            var response = client.SayHello(new GoogleGrpc.HelloRequest { Name = "Oscar"});
            Assert.AreEqual("Hello Oscar",  response.Message);
        }

        [Test]
        public void SayHelloJulianTest()
        {
            var response = client.SayHello(new GoogleGrpc.HelloRequest { Name = "Julian" });
            Assert.AreNotEqual("Heelo Jul i an", response.Message);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            channel.ShutdownAsync().Wait();
            server.ShutdownAsync().Wait();
        }
    }
}

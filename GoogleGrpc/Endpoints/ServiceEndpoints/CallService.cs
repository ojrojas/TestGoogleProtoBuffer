using Ardalis.ApiEndpoints;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleGrpc.Endpoints.ServiceEndpoints
{
    public class CallService : BaseAsyncEndpoint.WithRequest<string>.WithResponse<string>
    {
        public CallService()
        {
        }

        [HttpGet("api/sayhello")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation(
           Summary = "get info client ",
           Description = "get info Grpc",
           OperationId = "index.get",
           Tags = new[] { "IndexEndpoint" })]
        public override async Task<ActionResult<string>> HandleAsync(string request, CancellationToken cancellationToken = default)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = request });

            return reply.Message;
        }
    }
}

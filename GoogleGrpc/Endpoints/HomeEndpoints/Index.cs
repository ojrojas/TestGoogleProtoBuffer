using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleGrpc.Endpoints.HomeEndpoints
{
    public class Index : BaseAsyncEndpoint.WithoutRequest.WithResponse<string>
    {
        [HttpGet("api/information")]

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerOperation(
           Summary = "get info client ",
           Description = "get info Grpc",
           OperationId = "index.get",
           Tags = new[] { "IndexEndpoint" })]
        public override async Task<ActionResult<string>> HandleAsync(CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return @"Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909";
        }
    }
}

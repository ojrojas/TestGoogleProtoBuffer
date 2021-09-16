using GoogleGrpc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        //public ApiWebApplicationFactory()
        //{
        //    WithWebHostBuilder(builder => { 
        //        builder
        //        .UseSetting("https_port", "5001")
        //        .Build(); });
        //}
    }
}

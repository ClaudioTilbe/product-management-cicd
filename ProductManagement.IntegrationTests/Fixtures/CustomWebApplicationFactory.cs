using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;


namespace ProductManagement.IntegrationTests.Fixtures
{
    public class CustomWebApplicationFactory
     : WebApplicationFactory<Program>
    {


        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {

            builder.UseEnvironment("Testing");


        }

    }


}

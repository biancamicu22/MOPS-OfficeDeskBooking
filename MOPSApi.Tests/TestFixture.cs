using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MOPSAPI.Tests
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestServer Server;
        private readonly HttpClient _client;


        public TestFixture()
        {
            var baseDirectory = AppContext.BaseDirectory;
            var MOPSAPIPATH = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\..\MMS\MOPSAPI"));
            var builder = new WebHostBuilder()
                .UseContentRoot(MOPSAPIPATH)
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            _client = new HttpClient();
        }


        public void Dispose()
        {
            _client.Dispose();
            Server.Dispose();
        }
    }
}

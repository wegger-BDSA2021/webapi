using System;
using System.Dynamic;
using System.Net.Http;
using Xunit;

namespace api.tests.Controller.Tests
{
    public class ControllerTestsBase : IClassFixture<WebApiTesterFactory>
    {
        protected readonly WebApiTesterFactory factory;
        protected HttpClient client;
        protected dynamic token;

        public ControllerTestsBase(WebApiTesterFactory factory)
        {
            this.factory = factory;
            client = factory.CreateClient();

            token = new ExpandoObject();
            token.sub = Guid.NewGuid();
            token.scope = new[] { "ReadAccess" };
        }
    }
}
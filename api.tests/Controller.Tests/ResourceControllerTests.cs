using api.src;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.tests.Controller.Tests
{
    [Xunit.Collection("Sequential")]
    class ResourceControllerTests : TestFixture
    {
        public ResourceControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }
        


    }
}

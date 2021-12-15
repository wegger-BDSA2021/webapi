﻿using api.src;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace api.tests.Controller.Tests
{
    [Xunit.Collection("Sequential")]
    public class RatingControllerTests : TestFixture
    {
        public RatingControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void Get_Ratings_From_Resource_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Rating/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
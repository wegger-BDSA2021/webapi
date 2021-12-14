using api.src.Controllers;
using api.src.Services;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Data.Response;

namespace api.tests.Controller.Tests
{
    public class RatingServiceTests
    {
        private readonly RatingService _ratingService;
        public RatingServiceTests()
        {
            _ratingService = new RatingService();
        }
    }
}
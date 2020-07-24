using Exercises.Controllers;
using Exercises.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Test
{
    public class BeerControllerTest
    {
        private BeerController BeerController { get; set; }
        public BeerControllerTest()
        {
            BeerController = new BeerController();
        }

        [Fact]
        public void GetById()
        {
            var actionResult = BeerController.GetById(1);
            var result = actionResult as dynamic;
            Assert.NotNull(result.Value);
            Assert.True(result.Value.Id == 1);
        }

        [Fact]
        public void PutBeerRatings()
        {
            var actionResult = BeerController.PutBeerRatings(1, new BeerUserRatings {
                UserName = "test@email.com",
                Comment = "I would get this beer again",
                Rating = 5
            });
            var result = actionResult as OkResult;
            Assert.IsType<OkResult>(result);
        }
    }
}

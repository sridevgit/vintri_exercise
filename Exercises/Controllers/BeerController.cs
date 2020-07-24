using System;
using Exercises.Filters;
using Exercises.Models;
using Exercises.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercises.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeerController : ControllerBase
    {
        private PunkApi PunkApi { get; set; }
        public BeerController()
        {
            PunkApi = new PunkApi();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(PunkApi.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("putBeerRatings/{id}")]
        [HttpPut]
        [ServiceFilter(typeof(EmailActionFilter))]
        public IActionResult PutBeerRatings(int id, [FromBody] BeerUserRatings data)
        {
            try
            {
                PunkApi.PutBeerRatings(id, data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}

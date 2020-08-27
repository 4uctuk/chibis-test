using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ChibisTest.Features.Common.Exceptions;
using ChibisTest.Features.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChibisTest.Features.WebHooks
{
    [Route("hooks")]
    [ApiController]
    public class WebHooksController : ControllerBase
    {
        private readonly IHooksService _service;

        public WebHooksController(IHooksService service)
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<Subscriber>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllSubscribers();
            return Ok(result);
        }

        [HttpGet("add")]
        public async Task<IActionResult> Add(string url)
        {
            try
            {
                await _service.AddSubscriber(url);
                return Ok();
            }
            catch (DuplicatedEntityException ex)
            {
                return BadRequest($"This subscriber:{url} already exist");
            }
            
        }

        [HttpGet("remove")]
        public async Task<IActionResult> Remove(string url)
        {
            try
            {
                await _service.RemoveSubscriber(url);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest($"This subscriber:{url} doesn't exist");
            }
        }
    }
}

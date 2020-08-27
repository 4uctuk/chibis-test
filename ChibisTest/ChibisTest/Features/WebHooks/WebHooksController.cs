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

        /// <summary>
        /// Return all subscribed urls
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Subscriber>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllSubscribers();
            return Ok(result);
        }

        /// <summary>
        /// Add new subscriber
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("add")]
        public async Task<IActionResult> Add(Uri url)
        {
            try
            {
                await _service.AddSubscriber(url.ToString());
                return Ok();
            }
            catch (DuplicatedEntityException ex)
            {
                return BadRequest($"This subscriber:{url} already exist");
            }
            
        }

        /// <summary>
        /// Remove subscriber
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("remove")]
        public async Task<IActionResult> Remove(Uri url)
        {
            try
            {
                await _service.RemoveSubscriber(url.ToString());
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return BadRequest($"This subscriber:{url} doesn't exist");
            }
        }
    }
}

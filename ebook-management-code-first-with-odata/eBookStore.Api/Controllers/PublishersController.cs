using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PublishersController : BaseController
    {
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _publisherService.GetAllAsync();
            return Ok(result.AsQueryable());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _publisherService.GetByIdAsync(id);
            if (result is not null)
            {
                return Ok(result);
            }
            else return BadRequest("Not found Publisher with Id: " + id.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PublisherCreateModel model)
        {
            var result = await _publisherService.CreateAsync(model);
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            else throw new Exception($"Create failed!");

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PublisherUpdateModel model)
        {
            var result = await _publisherService.UpdateAsync(model);
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status204NoContent, result);
            }
            else throw new Exception($"Update failed!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _publisherService.DeleteAsync(id);
            if (result)
                return NoContent();
            else throw new Exception("Delete failed!");
        }

    }
}

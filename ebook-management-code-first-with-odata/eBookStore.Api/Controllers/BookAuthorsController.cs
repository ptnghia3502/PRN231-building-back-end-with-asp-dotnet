using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookAuthorsController : BaseController
    {
        private readonly IBookAuthorService _bookAuthorService;
        public BookAuthorsController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _bookAuthorService.GetAllAsync();
            return Ok(result.AsQueryable());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _bookAuthorService.GetBookAuthorByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookAuthorCreateModel model)
        {
            var result = await _bookAuthorService.CreateBookAuthorAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookAuthorUpdateModel model)
        {
            return StatusCode(StatusCodes.Status204NoContent, await _bookAuthorService.UpdateBookAuthorAsync(model));
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookAuthorService.DeleteBookAuthorAsync(id);
            return NoContent();
        }
    }
}

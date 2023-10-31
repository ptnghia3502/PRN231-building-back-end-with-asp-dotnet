using eBookStore.Services.Interface;
using eBookStore.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorsController : BaseController
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok((await _authorService.GetAllAsync()).AsQueryable());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _authorService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorCreateModel model)
        {
            var result = await _authorService.CreateAuthor(model);
            if (result is not null)
                return StatusCode(StatusCodes.Status201Created, result);
            else throw new Exception("Create failed!");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorUpdateModel updateModel)
        {
            var result = await _authorService.UpdateAuthor(updateModel);
            if (result is not null)
                return NoContent();
            else throw new Exception("Update failed!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _authorService.DeleteAuthor(id);
            if (result)
                return NoContent();
            else throw new Exception("Delete failed!");
        }

    }
}

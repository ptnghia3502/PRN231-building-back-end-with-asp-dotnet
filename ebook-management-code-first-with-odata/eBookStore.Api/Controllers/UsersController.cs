using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.Api.Controllers
{
    public class UsersController : BaseController
    {
        public IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }    
        
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _userService.GetAllUsersAsync()).AsQueryable());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = (await _userService.GetAllUsersAsync()).FirstOrDefault(x => x.Id == id);
            if (result is not null) return Ok(result);
            else return NotFound($"Not found user with Id: {id}");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateModel model)
        {
            var result = await _userService.CreateAsync(model);
            if (result is not null)
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            else return BadRequest($"Create Failed!");
        }

        [Authorize]
        [EnableQuery]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateModel model)
        {
            var result = await _userService.UpdateAsync(model);
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status204NoContent, result);
            }
            else return BadRequest("Update failed!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _userService.DeleteAsync(id))
                return NoContent();
            else return BadRequest();
        }

    }
}

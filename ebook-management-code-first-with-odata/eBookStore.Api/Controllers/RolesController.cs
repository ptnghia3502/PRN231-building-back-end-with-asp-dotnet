using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.Api.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetRoles();
            return Ok(result.AsQueryable());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateModel model)
        {
            return StatusCode(StatusCodes.Status201Created, await _roleService.CreateRole(model));
        }

    }
}

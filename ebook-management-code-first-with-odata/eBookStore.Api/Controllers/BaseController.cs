using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ODataController
    {
    }
}

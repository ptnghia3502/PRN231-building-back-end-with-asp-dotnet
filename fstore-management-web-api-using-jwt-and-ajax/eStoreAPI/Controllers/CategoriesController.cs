using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Services.Interfaces;

namespace eStoreAPI.Controllers
{
    public class CategoriesController : BaseController
    {

        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get a list of all categories.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategory();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

    }
}

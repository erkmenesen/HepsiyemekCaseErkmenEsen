using HepsiyemekCaseErkmenEsen.Services.Menu.Cache;
using HepsiyemekCaseErkmenEsen.Services.Menu.Dtos;
using HepsiyemekCaseErkmenEsen.Services.Menu.Services;
using HepsiyemekCaseErkmenEsen.Shared.ControllerBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Cached(300, "Category")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{name}")]
        [Cached(300, "Category")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _categoryService.GetByNameAsync(name);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto ProductUpdateDto)
        {
            var response = await _categoryService.UpdateAsync(ProductUpdateDto);
            return CreateActionResultInstance(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }

    }
}

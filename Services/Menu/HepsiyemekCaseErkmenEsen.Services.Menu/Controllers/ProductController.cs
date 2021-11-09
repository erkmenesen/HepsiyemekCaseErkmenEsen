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
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        [Cached(300, "Product")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ProductService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{name}")]
        [Cached(300, "Product")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _ProductService.GetByNameAsync(name);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto ProductCreateDto)
        {
            var response = await _ProductService.CreateAsync(ProductCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto ProductUpdateDto)
        {
            var response = await _ProductService.UpdateAsync(ProductUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _ProductService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}

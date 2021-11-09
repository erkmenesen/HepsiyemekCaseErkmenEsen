using HepsiyemekCaseErkmenEsen.Services.Menu.Dtos;
using HepsiyemekCaseErkmenEsen.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryCreateDto>> CreateAsync(CategoryCreateDto category);
        Task<Response<CategoryDto>> GetByNameAsync(string name);
        Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}

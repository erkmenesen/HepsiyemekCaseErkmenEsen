using HepsiyemekCaseErkmenEsen.Services.Menu.Dtos;
using HepsiyemekCaseErkmenEsen.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductDto>>> GetAllAsync();
        Task<Response<ProductDto>> GetByNameAsync(string name);
        Task<Response<ProductDto>> CreateAsync(ProductCreateDto productCreateDto);
        Task<Response<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}

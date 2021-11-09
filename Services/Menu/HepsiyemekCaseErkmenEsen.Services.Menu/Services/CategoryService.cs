using AutoMapper;
using HepsiyemekCaseErkmenEsen.Services.Menu.Dtos;
using HepsiyemekCaseErkmenEsen.Services.Menu.Models;
using HepsiyemekCaseErkmenEsen.Services.Menu.Settings;
using HepsiyemekCaseErkmenEsen.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryCreateDto>> CreateAsync(CategoryCreateDto categoryDto)
        {
            if (string.IsNullOrEmpty(categoryDto.name))
            {
                return Response<CategoryCreateDto>.Fail("Name field is mandatory.", 400);
            }
            else
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _categoryCollection.InsertOneAsync(category);

                return Response<CategoryCreateDto>.Success(_mapper.Map<CategoryCreateDto>(category), 200);
            }
        }

        public async Task<Response<CategoryDto>> GetByNameAsync(string name)
        {
            var category = await _categoryCollection.Find<Category>(o => o.name == name).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var updateCategory = _mapper.Map<Category>(categoryUpdateDto);
            var result = await _categoryCollection.FindOneAndReplaceAsync(o => o._id == updateCategory._id, updateCategory);
            if (result == null)
            {
                return Response<NoContent>.Fail("Category not found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _categoryCollection.DeleteOneAsync(o => o._id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Product not found", 404);
            }
        }

    }
}

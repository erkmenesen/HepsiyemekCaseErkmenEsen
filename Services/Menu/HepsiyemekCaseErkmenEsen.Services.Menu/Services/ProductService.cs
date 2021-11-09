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
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(category => true).ToListAsync();

            if (products.Any())
            {
                foreach (var prod in products)
                {
                    prod.Category = await _categoryCollection.Find<Category>(o => o._id == prod.categoryId).FirstAsync();
                }
            }
            else
            {
                products = new List<Product>();
            }


            return Response<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
        }

        public async Task<Response<ProductDto>> GetByNameAsync(string name)
        {
            var product = await _productCollection.Find<Product>(o => o.name == name).FirstOrDefaultAsync();
            if (product == null)
            {
                return Response<ProductDto>.Fail("Category not found", 404);
            }
            else
            {
                product.Category = await _categoryCollection.Find<Category>(o => o._id == product.categoryId).FirstAsync();
            }

            return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }


        public async Task<Response<ProductDto>> CreateAsync(ProductCreateDto prodCreateDto)
        {
            if (string.IsNullOrEmpty(prodCreateDto.name) || string.IsNullOrEmpty(prodCreateDto.currency) || prodCreateDto.price == null || string.IsNullOrEmpty(prodCreateDto.categoryId))
            {
                return Response<ProductDto>.Fail("Name, Currency and Price fields are mandatory", 400);
            }
            else
            {
                var newProduct = _mapper.Map<Product>(prodCreateDto);
                await _productCollection.InsertOneAsync(newProduct);

                return Response<ProductDto>.Success(_mapper.Map<ProductDto>(newProduct), 200);
            }
        }

        public async Task<Response<NoContent>> UpdateAsync(ProductUpdateDto prodUpdateDto)
        {
            var updateProduct = _mapper.Map<Product>(prodUpdateDto);
            var result = await _productCollection.FindOneAndReplaceAsync(o => o._id == updateProduct._id, updateProduct);
            if (result == null)
            {
                return Response<NoContent>.Fail("Product not found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(o => o._id == id);

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

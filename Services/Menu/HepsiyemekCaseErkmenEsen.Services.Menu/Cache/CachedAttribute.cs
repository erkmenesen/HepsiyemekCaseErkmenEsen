using AutoMapper;
using HepsiyemekCaseErkmenEsen.Services.Menu.Dtos;
using HepsiyemekCaseErkmenEsen.Services.Menu.Models;
using HepsiyemekCaseErkmenEsen.Services.Menu.Services;
using HepsiyemekCaseErkmenEsen.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Cache
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        private readonly string _dataType;

        public CachedAttribute(int timeToLiveSeconds, string dataType)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
            _dataType = dataType;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();
            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheReponse = await cacheService.GetCacheReponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheReponse))
            {
                if (_dataType == "Category")
                {
                    if (!cacheReponse.StartsWith("["))
                    {
                        cacheReponse = "[" + cacheReponse + "]";
                    }
                    List<CategoryDto> cats = JsonConvert.DeserializeObject<List<CategoryDto>>(cacheReponse);
                    var response = Response<List<CategoryDto>>.Success(cats, 200);
                    var contextResult = new ObjectResult(response)
                    {
                        StatusCode = response.StatusCode
                    };
                    context.Result = contextResult;
                    return;
                }
                else
                {
                    if (!cacheReponse.StartsWith("["))
                    {
                        cacheReponse = "[" + cacheReponse + "]";
                    }
                    List<ProductDto> prods = JsonConvert.DeserializeObject<List<ProductDto>>(cacheReponse);
                    var response = Response<List<ProductDto>>.Success(prods, 200);
                    var contextResult = new ObjectResult(response)
                    {
                        StatusCode = response.StatusCode
                    };
                    context.Result = contextResult;
                    return;
                }
            }
            var executedContext = await next();

            dynamic result = executedContext.Result;
            dynamic dataToCache = result.Value.Data;


            await cacheService.CacheResponseAsync(cacheKey, dataToCache, TimeSpan.FromSeconds(_timeToLiveSeconds));

        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(o => o.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}

using HepsiyemekCaseErkmenEsen.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiyemekCaseErkmenEsen.Shared.ControllerBase
{
    public class CustomBaseController : Controller
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}

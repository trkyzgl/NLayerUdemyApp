using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    /* Buraya gerek yok CustomBaseController dan miraz alacaz*/
    [Route("api/[controller]")]
    [ApiController]

    //[ValidateFilterAttribute] // Global olarak kullacağımız için bu Attribute ü tek tek bütün controller lardatanımlamak yerine program.cs de tanımlayacağız
    public class CategoriesController : CustomBaseController
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories =await _categoryService.GetAllAsync();
            var categoryDto  = _mapper.Map<List<CategoryDto>>(categories.ToList());
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoryDto));

        }



         //api/categories/GetSingleCategoryByIdWithProductAsync/2
         [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProduct(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductAsync(categoryId));
        }


    }
}

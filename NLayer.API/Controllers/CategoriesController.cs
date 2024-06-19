using Microsoft.AspNetCore.Mvc;
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

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        //api/categories/GetSingleCategoryByIdWithProductAsync/2
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProduct(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductAsync(categoryId));
        }


    }
}

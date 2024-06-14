using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {


        /*   BU kısmı kapatacağız. Çünkü bu kısımda almamız gereken veriler API den DTO lu gelecek.
        private readonly IProductService _services;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService services, ICategoryService categoryService, IMapper mapper)
        {
            _services = services;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        */
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetProductWithCategoryAsync());
        }

        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View(); // Ayrıca bir not: en üst menuden Analyze->Code Cleanup-> profile bir kısmından kodları düzenleme yap.
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto); 
                return RedirectToAction(nameof(Index));// başarılıysa Index e gitsin tekrar baksın
            }
            // else durumunda
            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();// hatalıysa aynı sayfaya tekrar dönsün(Hata mesajı için)
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();


            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {

                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));// başarılıysa Index e gitsin tekrar baksın
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();


            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}

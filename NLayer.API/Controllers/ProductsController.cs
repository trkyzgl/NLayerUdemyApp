﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]    /* CustomControllerBase de olduğu için pasif oalcak */
    [ApiController]           /* CustomControllerBase de olduğu için pasif oalcak */
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        //private readonly IService<Product> _service;    // Burayı ipral edip alt satırda _service kullanacağız. Çünkü zaten IProductService kalıtım alıyor ve aynı özellikleri taşıyor.
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService productService = null)
        {

            //_service = service;
            _mapper = mapper;
            _service = productService;
        }


        //[HttpGet("GetProductWithCategory")]  // Bu durumda her defasında bu şekilde metot ismini belirtmek yerine "action" yazarsak otomatik olarak metot adını alır
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWithCategory());
        }

        [HttpGet]    // Bu bir HttpGet isteği olacak
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));  // BaseControllerde yazdığımız kodlar ile bir daha 'Ok' gibi durumlar dönmeye gerek yok
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        //www.mysite/api/products/5     gibi olacak
    [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]    // Bu bir HttpGet iisteği olacak
        public async Task<IActionResult> GetById(int id)
        {

            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        // DELETE api/products/5 
        [HttpDelete("{id}")]    // Bu bir HttpDelete isteği olacak
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}

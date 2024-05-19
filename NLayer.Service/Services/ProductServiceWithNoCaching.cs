using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositoties;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService
    {

        private readonly IProductRepository _repositoryRepository;
        private readonly IMapper _mapper;
        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository repositoryRepository ) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _repositoryRepository = repositoryRepository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var product = await _repositoryRepository.GetProductsWithCategory();
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>( product );
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productDto);
            //throw new NotImplementedException();
        }
    }
}

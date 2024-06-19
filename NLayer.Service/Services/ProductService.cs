using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositoties;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {

        private readonly IProductRepository _repositoryRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository repositoryRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _repositoryRepository = repositoryRepository;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
        {
            var product = await _repositoryRepository.GetProductsWithCategory();
<<<<<<< HEAD
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return productDto;
=======
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>( product );
            return productDto;       // CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productDto);
>>>>>>> 1c09a6cee11c8720302e63e204c79408ad25f231
            //throw new NotImplementedException();
        }
    }
}

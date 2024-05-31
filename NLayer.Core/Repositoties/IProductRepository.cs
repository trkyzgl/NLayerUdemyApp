using NLayer.Core.Models;

namespace NLayer.Core.Repositoties
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        Task<List<Product>> GetProductsWithCategory();
    }
}

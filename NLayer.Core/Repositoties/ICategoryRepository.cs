using NLayer.Core.Models;

namespace NLayer.Core.Repositoties
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetSingleCategoryByIdWithProductAsync(int categoryId);

    }
}

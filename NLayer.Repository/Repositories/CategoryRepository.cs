using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositoties;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();// Bide FirstOrDefault var. Oda birden fazla değer bulunması halinde ilkini döner. Burada Id primery key olduğu olduğu ve benzersiz olduğu olduğu için birden fazla değerin olması söz konusu olamaz zaten o yüzden SingleOrDefault kullanmak daha mantıklı.
            //throw new NotImplementedException();
        }
    }
}

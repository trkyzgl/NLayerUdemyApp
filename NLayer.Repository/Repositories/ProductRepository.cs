using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositoties;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            /*Eager loading yapıyoruz. Bide Lazy loading vardır. product a bağlı Category leri çeksekdik Lazy loading olur. ilk Product ları çektiğimizde
             * Categpry leri de çekersek Eager loading olur.*/
            return await _context.Products.Include(p => p.Category).ToListAsync();
            //throw new NotImplementedException();
        }
    }
}

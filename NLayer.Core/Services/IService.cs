using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {

        Task<T> GetByIdAsync(int id);
        /*//productRepository.where(x=>x.id).OrderBy.ToListAsync();
        IQueryable yapmamızın sebebi .ToListAsync() deyip veri tabanı sorgusu yapıp datayı memory ye aldıktan sonra OrderBy yapmak
        yerine direkt olarak OrderyBy ile sorgu yapmamızı sağlayacaktır.
         */
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRange(IEnumerable<T> entities);

    }
}

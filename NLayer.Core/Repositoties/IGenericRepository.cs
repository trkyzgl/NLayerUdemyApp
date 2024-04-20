using System.Linq.Expressions;

namespace NLayer.Core.Repositoties
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        //productRepository.where(x=>x.id).OrderBy.ToListAsync();
        /* IQueryable yapmamızın sebebi .ToListAsync() deyip veri tabanı sorgusu yapıp datayı memory ye aldıktan sonra OrderBy yapmak
        yerine direkt olarak OrderyBy ile sorgu yapmamızı sağlayacaktır.
         */
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);


        IQueryable<T> Where(Expression<Func<T,bool>> expression);

        IQueryable<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

    }
}

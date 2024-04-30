using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositoties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {


        // readonly yapıyoruz ki kazara başka yerlerde set etmeyelim
        protected readonly AppDbContext _context; // kalıtım aldığı yerlerden kullanışması gerektiği için protected
        private readonly DbSet<T> _dbSet; // Bu private olarak kalacak
        public GenericRepository(AppDbContext context)
        {
            // burda readonly olduğu için bunlar ya oluşturulduğu esnada yada ctor içinde tanımlanabilir
            _context = context;
            _dbSet = context.Set<T>();

        }

        //


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);  

            //throw new NotImplementedException();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            //throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
            //throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            // AsNoTracking kullanmamızın sebebi ilk başta verileri memory e almasın. Çünkü ben daha özel sorgular yazabilirim.
            return _dbSet.AsNoTracking().AsQueryable();
            //throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
            //throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            /* Remove metodunun async i yok. Çünkü silinmek istenen veri bu sekilde delete flag ı alıyor. 
             * Ardından savechange() yapılında EF gidip flag lanan verileri bulup DB den siliyor.
            */
            _dbSet.Remove(entity);
            //throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            /* Bu da Remove nin aynısı. Sadece veriler arasında Foreach ile entity ler arasında gezip hep bir entity nin 
             state sini delete oalrak işaretler  */
            _dbSet.RemoveRange(entities);

            //throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            /*bunda da async yok. Remove mantığının aynısı */
            _dbSet.Update(entity);  
            //throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);    
            //throw new NotImplementedException();
        }
    }
}

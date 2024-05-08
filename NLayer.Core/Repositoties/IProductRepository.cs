using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositoties
{
    public interface  IProductRepository : IGenericRepository<Product>
    {

        Task<List<Product>> GetProductsWithCategory();
    }
}

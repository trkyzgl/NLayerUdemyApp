using System.Collections.ObjectModel;

namespace NLayer.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Collection<Product> Products { get; set; }
    }
}

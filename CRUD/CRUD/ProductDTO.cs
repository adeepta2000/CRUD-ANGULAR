using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD
{
    public class ProductDTO
    {
        public string? Name { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
    }
}

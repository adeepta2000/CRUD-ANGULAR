using CRUD.EF;
namespace CRUD.EF.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Product>? Products { get; set; }

        public Brand()
        {
            Products = new List<Product>();
        }
    }
}

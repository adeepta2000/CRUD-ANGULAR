using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.EF.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =2)]
        public string? Name { get; set; } 

        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [NotMapped]
        public string? BrandName { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }

        public virtual Category ? Category { get; set; }
        public virtual Brand ? Brand { get; set; }
    }
}

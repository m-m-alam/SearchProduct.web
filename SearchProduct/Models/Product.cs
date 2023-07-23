using System.ComponentModel.DataAnnotations;

namespace SearchProduct.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name= "Category Name")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}

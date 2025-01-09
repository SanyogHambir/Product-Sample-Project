using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class Products
    {
        [Key]
        public int SN { get; set; }

        [Display(Name = "Product Name")]
        public String ProductName { get; set; }

        public DateTime Created { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProjectActivity.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProductAddedDate { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
    }
}

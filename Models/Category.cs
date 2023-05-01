using System.ComponentModel.DataAnnotations;

namespace ProjectActivity.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime CategoryAddedDate { get; set; }
        public List<Product> Products { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProjectActivity.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime BrandAddedDate { get; set; }
        public List<Product> Products { get; set; }
    }
}

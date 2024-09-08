using System.ComponentModel.DataAnnotations;

namespace ProgrammerTask.Models
{
    public class Product
    {
        public int Id { get; set;}
        [Display(Name ="Product Name")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Order>? Orders {  get; set; }   
    }
}

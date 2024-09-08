using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgrammerTask.Models
{
    public class Order
    {
        [Display(Name ="Code")]
        public Guid Id { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public IdentityUser user { get; set; }  
        public List<Product> Products { get; set; } 
        public DateTime DataTime { get; set; }= DateTime.Now;
    }
}

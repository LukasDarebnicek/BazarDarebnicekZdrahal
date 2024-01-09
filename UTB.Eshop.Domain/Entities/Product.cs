using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UTB.Eshop.Domain.Entities
{
    public class Product : Entity<int>
    {
        [Required] // je vyžadován 
        [StringLength(100)]  //omezení znaků
        public string Name { get; set; }
        
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        public string? ImageSrc { get; set; }
        
        public string? Kategory { get; set; }
        
        //public double? PhoneNumber { get; set; }

       // public string? Email { get; set; }

        //[ForeignKey (nameof(Entity<int>.Id))]
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FoodName { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string FoodDescription { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FoodCatagory { get; set; }
        [Required]
        [Column(TypeName = "varBinary(MAX)")]
        public byte[] Image { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Stock { get; set; }

        [Display(Name = "VendorId")]
        public virtual int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }
        [Column(TypeName = "int")]
        public int DiscountPercentage { get; set; }
        [Column(TypeName = "bit")]
        public bool isVegetarian { get; set; }
    }
}

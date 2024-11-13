using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.DTO
{
    public class FoodResponse
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public string FoodCatagory { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int AverageRating { get; set; }
        public virtual int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool isVegetarian { get; set; }
    }
}

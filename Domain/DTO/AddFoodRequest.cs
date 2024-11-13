using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Domain.DTO
{
    public class AddFoodRequest
    {
        //public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public string FoodCatagory { get; set; }
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int VendorId { get; set; }
        public bool isVegetarian { get; set; }
    }
}

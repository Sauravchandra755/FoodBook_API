using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UpdateFoodRequest
    {
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public string FoodCatagory { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int DiscountPercentage { get; set; }
        //public IFormFile Image { get; set; }

    }
}

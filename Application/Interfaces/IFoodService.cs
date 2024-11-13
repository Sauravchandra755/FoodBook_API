using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFoodService
    {
        Food Save(Food food);
        byte[] SaveFile(IFormFile uploadImage);
        //FoodResponse GetFoodWithImage(Food food);
    }
}

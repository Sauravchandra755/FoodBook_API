using Application.Interfaces;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Net.Mime;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FoodService : IFoodService
    {
        private readonly FoodBookDbContext _foodBook;

        public FoodService(FoodBookDbContext foodBook)
        {
            _foodBook = foodBook;
        }

        public Food Save(Food food)
        {
            _foodBook.FB__Foods.Add(food);
            _foodBook.SaveChanges();
            return food;
        }
        public byte[] SaveFile(IFormFile uploadImage)
        {
            var filebytes = new byte[200000];
         //   Food food = JsonConvert.DeserializeObject<Food>(uploadImage.Foodobj);
            if (uploadImage.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    uploadImage.CopyTo(ms);
                    filebytes = ms.ToArray();
                    return filebytes;
                }
            }
            return filebytes;
        }
        //public FoodResponse GetFoodWithImage(Food food)
        //{
        //    FoodResponse response = new FoodResponse();
        //    response.FoodId = food.FoodId;
        //    response.FoodName = food.FoodName;
        //    response.FoodCatagory = food.FoodCatagory;
        //    response.FoodDescription = food.FoodDescription;
        //    response.Stock = food.Stock;
        //    response.Price = food.Price;

        //    //var stream = new MemoryStream(food.Image);
        //    //IFormFile image = new FormFile(stream, 0, food.Image.Length, "image", food.FoodName);

        //    using (var stream = new MemoryStream(food.Image))
        //    {
        //        var image = new FormFile(stream, 0, food.Image.Length, "image", food.FoodName)
        //        {
        //            Headers = new HeaderDictionary(),
        //            //ContentType = contentType,
        //        };

        //        ContentDisposition cd = new ContentDisposition
        //        {
        //            FileName = image.FileName
        //        };
        //        image.ContentDisposition = cd.ToString();
        //        response.Image = image;
        //    }

            
        //    return response;
        //}
    }
}

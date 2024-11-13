using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data;
using Newtonsoft.Json;
using Application.Interfaces;
using Domain.DTO;
using System.Collections;
using Azure;
using System.Net.Mime;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using static NuGet.Packaging.PackagingConstants;

namespace FoodBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly FoodBookDbContext _context;
        private readonly IFoodService _foodService;

        public FoodsController(FoodBookDbContext context, IFoodService foodService)
        {
            _context = context;
            _foodService = foodService;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodResponse>>> GetFB__Foods()
        {
          if (_context.FB__Foods == null)
          {
              return NotFound();
          }
            var food = await _context.FB__Foods.ToListAsync();
            List<FoodResponse> foodResponse = new List<FoodResponse>();
            foreach (var item in food)
            {
                var foodres = new FoodResponse();
                foodres.FoodId = item.FoodId;
                foodres.FoodName = item.FoodName;
                foodres.FoodDescription = item.FoodDescription;
                foodres.FoodCatagory = item.FoodCatagory;
                foodres.Image = item.Image;
                foodres.Price = item.Price;
                foodres.Stock = item.Stock;
                foodres.isVegetarian = item.isVegetarian;
                foodres.VendorId = item.VendorId;
                foodres.Vendor = await _context.FB__Vendor.FindAsync(item.VendorId);

                List<int> rating = await _context.FB__Feedback.Where(f => f.FoodId == item.FoodId).Select(f => f.Rating).ToListAsync();
                if (rating.Count != 0)
                {
                    var sum = 0;
                    for (int i = 0; i < rating.Count; i++)
                    {
                        sum = sum + rating[i];
                    }
                    foodres.AverageRating = sum / rating.Count;
                }

                foodres.DiscountPercentage = item.DiscountPercentage;
                if (item.DiscountPercentage != 0)
                {
                    var discount = (item.Price * item.DiscountPercentage) / 100;
                    foodres.DiscountedPrice = item.Price - discount;
                    foodres.DiscountedPrice = Convert.ToInt32(foodres.DiscountedPrice);
                }

                else
                {
                    foodres.DiscountedPrice = 0;
                }
                foodResponse.Add(foodres);

            }
            return foodResponse;
            //List<FoodResponse> foodResponse = new List<FoodResponse>();
            //foreach (var food in resFood)
            //{
            //    //IFormFile resImage;
            //    //using (var stream = new MemoryStream(food.Image))
            //    //{
            //    //    var image = new FormFile(stream, 0, food.Image.Length, "image", food.FoodName)
            //    //    {
            //    //        Headers = new HeaderDictionary(),
            //    //        ContentType = "jpg",
            //    //    };

            //    //    ContentDisposition cd = new ContentDisposition
            //    //    {
            //    //        FileName = image.FileName
            //    //    };
            //    //    image.ContentDisposition = cd.ToString();
            //    //    resImage = image;
            //    //}
            //    FoodResponse fdRes = new FoodResponse();
            //    fdRes.FoodId = food.FoodId;
            //    fdRes.FoodName = food.FoodName;
            //    fdRes.FoodDescription = food.FoodDescription;
            //    fdRes.FoodCatagory = food.FoodCatagory;
            //    fdRes.Image = Convert.ToBase64String(food.Image);
            //    fdRes.Price = food.Price;
            //    fdRes.Stock = food.Stock;
            //    foodResponse.Add(fdRes);

            //}
            //return resFood;
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodResponse>> GetFood(int id)
        {
          if (_context.FB__Foods == null)
          {
              return NotFound();
          }
            var food = await _context.FB__Foods.FindAsync(id);           

            FoodResponse foodResponse = new FoodResponse();
            foodResponse.FoodId = food.FoodId;
            foodResponse.FoodName = food.FoodName;
            foodResponse.FoodDescription = food.FoodDescription;
            foodResponse.FoodCatagory = food.FoodCatagory;
            foodResponse.Image = food.Image;
            foodResponse.Price = food.Price;
            foodResponse.Stock = food.Stock;
            foodResponse.isVegetarian = food.isVegetarian;
            foodResponse.DiscountPercentage = food.DiscountPercentage;

            var discount = (food.Price * food.DiscountPercentage) / 100;
            foodResponse.DiscountedPrice = food.Price - discount;
            foodResponse.DiscountedPrice = Convert.ToInt32(foodResponse.DiscountedPrice);



            //var resfood = _foodService.GetFoodWithImage(food);
            //IFormFile resImage;
            //using (var stream = new MemoryStream(food.Image))
            //{
            //    var image = new FormFile(stream, 0, food.Image.Length, "image", food.FoodName)
            //    {
            //        Headers = new HeaderDictionary(),
            //        //ContentType = contentType,
            //    };

            //    ContentDisposition cd = new ContentDisposition
            //    {
            //        FileName = image.FileName
            //    };
            //    image.ContentDisposition = cd.ToString();
            //    resImage = image;
            //}
            //FoodResponse foodResponse = new FoodResponse();
            //foodResponse.FoodId = food.FoodId;
            //foodResponse.FoodName = food.FoodName;
            //foodResponse.FoodDescription = food.FoodDescription;
            //foodResponse.FoodCatagory = food.FoodCatagory;
            //foodResponse.Image = Convert.ToBase64String(food.Image);
            //foodResponse.Price = food.Price;
            //foodResponse.Stock = food.Stock;           

            if (food == null)
            {
                return NotFound();
            }
            //FoodResponse response = new FoodResponse();
            //response = _foodService.GetFoodWithImage(food);

            //return foodResponse;
            return foodResponse;
        }

        [HttpGet]
        [Route("GetfoodbyVendor/{id}")]
        public async Task<List<FoodResponse>> GetfoodbyVendor(int id)
        {
            var food = await _context.FB__Foods.Where(f => f.VendorId == id).ToListAsync();
            List<FoodResponse> foodResponse = new List<FoodResponse>();
            foreach (var item in food)
            {
                var foodres = new FoodResponse();
                foodres.FoodId = item.FoodId;
                foodres.FoodName = item.FoodName;
                foodres.FoodDescription = item.FoodDescription;
                foodres.FoodCatagory = item.FoodCatagory;
                foodres.Image = item.Image;
                foodres.Price = item.Price;
                foodres.Stock = item.Stock;
                foodres.VendorId = item.VendorId;
                foodres.isVegetarian = item.isVegetarian;
                foodres.Vendor = await _context.FB__Vendor.FindAsync(item.VendorId);

                List<int> rating = await _context.FB__Feedback.Where(f => f.FoodId == item.FoodId).Select(f => f.Rating).ToListAsync();
                if (rating.Count != 0)
                {
                    var sum = 0;
                    for (int i = 0; i < rating.Count; i++)
                    {
                        sum = sum + rating[i];
                    }
                    foodres.AverageRating = sum / rating.Count;
                }

                foodres.DiscountPercentage = item.DiscountPercentage;
                if (item.DiscountPercentage != 0)
                {
                    var discount = (item.Price * item.DiscountPercentage) / 100;
                    foodres.DiscountedPrice = item.Price - discount;
                    foodres.DiscountedPrice = Convert.ToInt32(foodres.DiscountedPrice);
                }
                else
                {
                    foodres.DiscountedPrice = 0;
                }
                foodResponse.Add(foodres);
                
            }
                return foodResponse;
            
        }


        // PUT: api/Foods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood([FromRoute] int id, [FromBody] UpdateFoodRequest updateFood)
        {
            //if (id != food.FoodId)
            //{
            //    return BadRequest();
            //}
            try
            {
                var food = await _context.FB__Foods.FindAsync(id);

                if (food != null)
                {
                    food.FoodName = updateFood.FoodName;
                    food.FoodDescription = updateFood.FoodDescription;
                    food.FoodCatagory = updateFood.FoodCatagory;
                    food.Price = updateFood.Price;
                    food.Stock = updateFood.Stock;
                    food.DiscountPercentage=updateFood.DiscountPercentage;
                    //food.Image = updateFood.Image;
                    //food.Image = _foodService.SaveFile(updateFood.Image);


                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            //var food = new Food()
            //{
            //    FoodName = updateFood.FoodName,
            //    FoodDescription = updateFood.FoodDescription,
            //    FoodCatagory = updateFood.FoodCatagory,
            //    Price = updateFood.Price,
            //    Stock = updateFood.Stock,
            //};

            //_context.Entry(food).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        

        // POST: api/Foods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood([FromForm]AddFoodRequest addFood)
        {

            try
            {
                if (_context.FB__Foods == null)
                {
                    return Problem("Entity set 'FoodBookDbContext.FB__Foods'  is null.");
                }
                var food = new Food()
                {
                    //FoodId= addFood.FoodId,
                    FoodName = addFood.FoodName,
                    FoodDescription = addFood.FoodDescription,
                    FoodCatagory = addFood.FoodCatagory,
                    Image = _foodService.SaveFile(addFood.Image),
                    //Image = addFood.Image,
                    Price = addFood.Price,
                    Stock = addFood.Stock,
                    VendorId = addFood.VendorId,
                    isVegetarian = addFood.isVegetarian
                };
                //food.Image=_foodService.SaveFile(uploadImage);
                await _context.FB__Foods.AddAsync(food);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
            }
            catch
            {
                throw;
            }
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            if (_context.FB__Foods == null)
            {
                return NotFound();
            }
            var food = await _context.FB__Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.FB__Foods.Remove(food);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodExists(int id)
        {
            return (_context.FB__Foods?.Any(e => e.FoodId == id)).GetValueOrDefault();
        }
    }
}

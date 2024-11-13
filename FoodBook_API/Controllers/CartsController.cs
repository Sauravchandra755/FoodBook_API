using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data;
using Domain.DTO;
using Application.Interfaces;

namespace FoodBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly FoodBookDbContext _context;
        private readonly IOrderService _orderService;

        public CartsController(FoodBookDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;

        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetFB__Cart()
        {
          if (_context.FB__Cart == null)
          {
              return NotFound();
          }
            List<Cart> cart = await _context.FB__Cart.ToListAsync();
            foreach (var od in cart)
            {
                od.Employee = await _context.FB__Employees.FindAsync(od.EmpId);
                od.Food = await _context.FB__Foods.FindAsync(od.FoodId);
            }
            return Ok(cart);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
          if (_context.FB__Cart == null)
          {
              return NotFound();
          }
            //var cart = await _context.FB__Cart.FindAsync(id);
            var cart = await _context.FB__Cart.Where(o => o.EmpId == id).ToListAsync();
            foreach (var od in cart)
            {
                //od.Employee = await _context.FB__Employees.FindAsync(od.EmpId);
                od.Food = await _context.FB__Foods.FindAsync(od.FoodId);
            }

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart([FromRoute] int id, [FromBody]UpdateCartRequest cart)
        {
            //if (id != cart.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(cart).State = EntityState.Modified;

            try
            {
                var carts = await _context.FB__Cart.FindAsync(id);

                if (carts != null)
                {
                    carts.Quantity = cart.Quantity;
                    if (cart.Method == "minus")
                    {
                        carts.TotalPrice = carts.TotalPrice - cart.Cost;
                    }
                    if (cart.Method == "plus")
                    {
                        carts.TotalPrice = carts.TotalPrice + cart.Cost;
                    }
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostCart(AddToCartRequest addToCart)
        {
          if (_context.FB__Cart == null)
          {
              return Problem("Entity set 'FoodBookDbContext.FB__Cart'  is null.");
          }


            var id = await _context.FB__Cart.MaxAsync(o => o.OrderId);
            
            var totalprice = _orderService.GetTotalPrice(addToCart.FoodId, addToCart.Quantity);
            var res = _orderService.CheckCredit(addToCart.EmpId, totalprice, addToCart.FoodId, addToCart.Quantity);
            //var food = await _context.FB__Foods.FindAsync(addToCart.FoodId);
            if (res.ToString() == "Proceed")
            {
                var cart = new Cart()
                {
                    //Id = 0,
                    OrderId = Convert.ToInt32(id) + 1,
                    EmpId = addToCart.EmpId,
                    FoodId = addToCart.FoodId,
                    Quantity = addToCart.Quantity,
                    TotalPrice = totalprice,
                    OrderDateTime = DateTime.Now,
                    IsActive = true,
                };
                await _context.FB__Cart.AddAsync(cart);
                //_orderService.UpdateStock(addToCart.Quantity, addToCart.FoodId);
                await _context.SaveChangesAsync();
                return res;
            }
            else
            {
                return res;
            } 
            
            

            
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.FB__Cart == null)
            {
                return NotFound();
            }
            var cart = await _context.FB__Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.FB__Cart.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return (_context.FB__Cart?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

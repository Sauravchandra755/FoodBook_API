using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data;
using Application.Interfaces;
using Domain.DTO;

namespace FoodBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly FoodBookDbContext _context;
        private readonly IOrderService _orderService;

        public OrdersController(FoodBookDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetFB__Orders()
        {
          if (_context.FB__Orders == null)
          {
              return NotFound();
          }
            List<Order> order = await _context.FB__Orders.OrderByDescending(y => y.OrderId).ToListAsync();
            List<OrderResponse> orderRes = new List<OrderResponse>();
            foreach(var od in order)
            {
                var oRes = new OrderResponse();
                oRes.OrderId = od.OrderId;
                oRes.EmpId = od.EmpId;
                oRes.FoodId = od.FoodId;
                oRes.Quantity = od.Quantity;
                oRes.TotalPrice = od.TotalPrice;
                oRes.OrderDateTime = od.OrderDateTime;
                oRes.OrderStatus = od.OrderStatus;
                oRes.Employee = await _context.Vw_MDB_EmployeeMaster.FindAsync(Convert.ToString(od.EmpId));
                oRes.Food = await _context.FB__Foods.FindAsync(od.FoodId);
                oRes.Rating = await _context.FB__Feedback.Where(f => f.OrderId == od.OrderId && f.FoodId == od.FoodId).Select(f => f.Rating).FirstOrDefaultAsync();
                oRes.Comment = await _context.FB__Feedback.Where(f => f.OrderId == od.OrderId && f.FoodId == od.FoodId).Select(f => f.Comments).FirstOrDefaultAsync();
                orderRes.Add(oRes);
            }
            return orderRes;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.FB__Orders == null)
          {
              return NotFound();
          }
            var order = await _context.FB__Orders.Where(o => o.EmpId == id).OrderByDescending(y => y.OrderId).ToListAsync();
            foreach (var od in order)
            {
                //od.Employee = await _context.Vw_MDB_EmployeeMaster.FindAsync(od.EmpId);
                od.Food = await _context.FB__Foods.FindAsync(od.FoodId);
                od.Food.Vendor = await _context.FB__Vendor.FindAsync(od.Food.VendorId);
            }

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] UpdateOrderRequest updateorder)
        {
            //if (id != id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(order).State = EntityState.Modified;

            try
            {
                var order = await _context.FB__Orders.FindAsync(id);
                if (order != null)
                {
                    order.OrderStatus = updateorder.OrderStatus;
                };
                //_context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //public async Task<ActionResult<Order>> PostOrder([FromBody]AddOrder addOrder)
        public async Task<ActionResult<Order>> PostOrder(AddOrder addOrder)
        {
          if (_context.FB__Orders == null)
          {
              return Problem("Entity set 'FoodBookDbContext.FB__Orders'  is null.");
          }
            //var id = _orderService.GetLastOrderId();
            //var cart = await _context.FB__Cart.Where(o => o.EmpId == empId).ToListAsync();
            //foreach (var od in cart)
            //{
            //    od.Employee = await _context.FB__Employees.FindAsync(od.EmpId);
            //    od.Food = await _context.FB__Foods.FindAsync(od.FoodId);
            //}
            var id = await _context.FB__Orders.MaxAsync(o => o.OrderId);
            var cart = await _context.FB__Cart.Where(c => c.EmpId == addOrder.EmployeeId && c.IsActive == true).ToListAsync();
            var totalprice = _orderService.GetTotalPrice(cart);
            var res = _orderService.CheckCredit(addOrder.EmployeeId, totalprice, cart);
            //if (empdata != null)
            //{
            //    empdata.DailyCredits = empdata.DailyCredits - totalprice;

            //    //_context.SaveChanges();
            //}
            if (res.ToString() == "Proceed")
            {
                var i = 1;
                foreach (var item in cart)
                {
                    
                    var order = new Order()
                    {
                        //Id = 0,
                        OrderId = Convert.ToInt32(id) + i,
                        EmpId = item.EmpId,
                        FoodId = item.FoodId,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                        OrderDateTime = DateTime.Now,
                        OrderStatus = true,
                    };
                    //var discount = (item.TotalPrice * item.discou)
                    i++;
                    //var cart = await _context.FB__Cart.Where(c => c.EmpId == addOrder.EmpId).ToListAsync();
                    await _context.FB__Orders.AddAsync(order);
                    await _orderService.UpdateStock(item.Quantity, item.FoodId);
                }
                
                await _orderService.UpdateCart(addOrder.EmployeeId);
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
                return Ok(res);
            }
            else
            {
                return Ok(res);
            }
             

            //return CreatedAtAction("GetOrder", new { id = Convert.ToInt32(id) }, addOrder);

        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.FB__Orders == null)
            {
                return NotFound();
            }
            var order = await _context.FB__Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.FB__Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.FB__Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}

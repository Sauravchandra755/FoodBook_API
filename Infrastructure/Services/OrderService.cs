using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly FoodBookDbContext _context;
        public OrderService(FoodBookDbContext context)
        {
            _context = context;
        }

        public string CheckCredit(int empId, decimal totalPrice, int foodId, int quantity)
        {
            try
            {
                decimal sum = 0;
                var empdata = _context.Vw_MDB_EmployeeMaster.Find(Convert.ToString(empId));
                var order =  _context.FB__Orders.Where(e => e.EmpId == empId && e.OrderDateTime.Date == DateTime.Now.Date).ToList();
                if (EmployeeExists(empId))
                {
                    return "Proceed";
                }
                else
                {
                    foreach (var o in order)
                    {
                        sum = o.TotalPrice + sum;
                    }

                    var food = _context.FB__Foods.Find(foodId);
                    if (totalPrice <= 200 - sum & quantity <= food.Stock)
                    {
                        //var emp = _context.FB__Employees.Find(empId);
                        //if (empdata != null)
                        //{
                        //    empdata.DailyCredits = empdata.DailyCredits - totalPrice;

                        //    _context.SaveChanges();
                        //}
                        return "Proceed";
                    }
                    else
                    {
                        return "Don't Proceed";
                    }
                }

            }
            catch
            {
                return "Don't Proceed";
            }
        }

        public string CheckCredit(int empId, decimal totalPrice, List<Cart> cartitem)
        {
            try
            {
                decimal sum = 0;
                var empdata = _context.Vw_MDB_EmployeeMaster.Find(Convert.ToString(empId));
                var order = _context.FB__Orders.Where(e => e.EmpId == empId && e.OrderDateTime.Date == DateTime.Now.Date).ToList();
                if (EmployeeExists(empId))
                {
                    return "Proceed";
                }
                else
                {
                    foreach (var o in order)
                    {
                        sum = o.TotalPrice + sum;
                    }

                    //var food = _context.FB__Foods.Find(foodId);
                    if (totalPrice <= 200 - sum)
                    {
                        foreach (var c in cartitem)
                        {
                            var food = _context.FB__Foods.Find(c.FoodId);
                            if (c.Quantity <= food.Stock)
                            {
                                return "Proceed";
                            }
                            else
                            {
                                return "Stock is Empty";
                            }
                        }
                        //var emp = _context.FB__Employees.Find(empId);
                        //if (empdata != null)
                        //{
                        //    empdata.DailyCredits = empdata.DailyCredits - totalPrice;

                        //    _context.SaveChanges();
                        //}
                        return "Proceed";
                    }
                    else
                    {
                        return "Don't Proceed";
                    }
                }

            }
            catch
            {
                return "Don't Proceed";
            }
        }

        public async Task<int> GetLastOrderId()
        {
            var id = await _context.FB__Orders.MaxAsync(o => o.OrderId);
            return id;
        }

        public decimal GetTotalPrice(int foodId, int quantity)
        {
            decimal totalPrice = 0;
            var food = _context.FB__Foods.Find(foodId);

            var discount = (food.Price * food.DiscountPercentage) / 100;
            var price = food.Price - discount;
            price = Convert.ToInt32(price);
            totalPrice = price * quantity;

            return totalPrice;
        }
        public decimal GetTotalPrice(List<Cart> cartitem)
        {
            decimal totalPrice = 0;
            foreach (var item in cartitem)
            {
                //var food = _context.FB__Foods.Find(item.FoodId);
                totalPrice = totalPrice + item.TotalPrice;
            }

            return totalPrice;
        }

        public async Task<int> UpdateStock(int quantity, int foodId)
        {
            var food = _context.FB__Foods.Find(foodId);
            if (food != null)
            {
                food.Stock = food.Stock - quantity;

                await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<string> UpdateCart(int empId)
        //public void UpdateCart(int empId)
        {
            var cart = await _context.FB__Cart.Where(c => c.EmpId == empId && c.IsActive == true).ToListAsync();
            foreach (var item in cart)
            {
                item.IsActive = false;
                await _context.SaveChangesAsync();
            }
            return "Ok";
        }

        private bool EmployeeExists(int id)
        {
            return (_context.FB__Employees?.Any(e => e.MId == id)).GetValueOrDefault();
        }
    }
}

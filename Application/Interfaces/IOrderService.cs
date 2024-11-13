using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        string CheckCredit(int empId, decimal totalPrice, List<Cart> cartitem);
        string CheckCredit(int empId, decimal totalPrice, int foodId, int quantity);
        Task<int> GetLastOrderId();
        decimal GetTotalPrice(List<Cart> cartitem);
        decimal GetTotalPrice(int foodId, int quantity);
        Task<int> UpdateStock(int quantity, int foodId);
        Task<string> UpdateCart(int empId);
        //void UpdateCart(int empId);
    }
}

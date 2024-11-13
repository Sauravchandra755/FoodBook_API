using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.DTO
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public int EmpId { get; set; }
        public Employee Employee { get; set; }

        public virtual int FoodId { get; set; }
        public Food Food { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDateTime { get; set; }
        public bool OrderStatus { get; set; }
    }
}

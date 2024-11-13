using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AddToCartRequest
    {
        public virtual int EmpId { get; set; }
        public virtual int FoodId { get; set; }
        public int Quantity { get; set; }

    }
}

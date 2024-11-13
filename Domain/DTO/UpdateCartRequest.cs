using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UpdateCartRequest
    {
        public int Quantity { get; set; }
        public int Cost { get; set; }
        public string Method { get; set; }
    }
}

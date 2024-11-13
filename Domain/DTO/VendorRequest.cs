using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class VendorRequest
    {
        public string VendorName { get; set; }
        public bool Flag { get; set; }
        public IFormFile Image { get; set; }
    }
}

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
    public class AddOrder
    {
        public virtual int EmployeeId { get; set; }
        //public decimal TotalPrice { get; set; }
        //public DateTime OrderDateTime { get; set; }
        //public bool OrderStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        //[Key]
        //[Column(TypeName = "int")]
        //public int Id { get; set; }

        [Column(TypeName = "int")]
        public int OrderId { get; set; }

        [Column(TypeName = "int")]
        public virtual int EmpId { get; set; }
        //[ForeignKey("EmployeeId")]
        //public virtual Employee Employee { get; set; }

        [Display(Name = "Food")]
        public virtual int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }

        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime OrderDateTime { get; set; }
        [Column(TypeName = "bit")]
        public bool OrderStatus { get; set; }
    }
}

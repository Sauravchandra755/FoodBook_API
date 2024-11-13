using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class Cart
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "int")]
        public int OrderId { get; set; }

        [Display(Name = "Employee")]
        public virtual int EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual Admin Employee { get; set; }

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
        public bool IsActive { get; set; }
    }
}

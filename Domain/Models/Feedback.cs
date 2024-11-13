using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int OrderId { get; set; }
        [Display(Name = "Food")]
        public virtual int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }
        [Column(TypeName = "int")]
        public int EmployeeMid { get; set; }
        [Column(TypeName = "int")]
        public int Rating { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Comments { get; set; }
    }
}

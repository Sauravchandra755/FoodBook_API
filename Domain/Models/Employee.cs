using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee
    {
        //[Key]
        //public int EmpId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string EmployeeId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string EmployeeName { get; set; }
        //[Required]
        //[Column(TypeName = "money")]
        //public Decimal DailyCredits { get; set; }

        //[Required]
        //[Column(TypeName = "datetime")]
        //public DateTime UpdatedCreditsDate { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FullEmailId { get; set; }
        //[Required]
        //[Column(TypeName = "varchar(50)")]
        //public string Role { get; set; }
        //[Required]
        //[Column(TypeName = "bit")]
        //public bool isActive { get; set; }
    }
}

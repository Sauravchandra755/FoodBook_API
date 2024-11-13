using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Admin
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int MId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string EmployeeName { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int VendorId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Role { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

    }
}

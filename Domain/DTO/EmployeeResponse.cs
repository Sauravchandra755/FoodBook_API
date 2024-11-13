using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class EmployeeResponse
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public Decimal DailyCredits { get; set; }
        public int? VendorId { get; set; }

        public string FullEmailId { get; set; }
    }
}

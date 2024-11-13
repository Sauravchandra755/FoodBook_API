using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AddEmployeeRequest
    {
        public int MId { get; set; }

        
        public string EmployeeName { get; set; }
        
       // public Decimal DailyCredits { get; set; }
        
        
        
        public string Email { get; set; }
        
        public string Role { get; set; }
       
        
    }
}

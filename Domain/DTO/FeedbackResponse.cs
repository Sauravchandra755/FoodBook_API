using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class FeedbackResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
        public int EmployeeMid { get; set; }
        public virtual Employee Employee { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}

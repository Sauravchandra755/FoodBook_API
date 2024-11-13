using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public string ReplyUrl { get; set; }
        public string Role { get; set; }
    }
}

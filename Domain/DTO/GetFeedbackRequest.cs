﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class GetFeedbackRequest
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
    }
}

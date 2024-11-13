using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly FoodBookDbContext _context;
        public EmployeeService(FoodBookDbContext context)
        {
            _context = context;
        }
        //public void UpdateCredit()
        //{
        //    List<Admin> employees = _context.FB__Employees.ToList();
        //    foreach(var emp in employees)
        //    {
        //        if(emp.UpdatedCreditsDate.Date != DateTime.Now.Date)
        //        {
        //            emp.DailyCredits = 200;
        //            emp.UpdatedCreditsDate = DateTime.Now;
        //        }
        //         _context.SaveChanges();
        //    }
        //}
    }
}

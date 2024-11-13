using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FoodBookDbContext : DbContext
    {
        public FoodBookDbContext(DbContextOptions<FoodBookDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> FB__Employees { get; set; }
        public DbSet<Food> FB__Foods { get; set; }
        public DbSet<Order> FB__Orders { get; set; }
        public DbSet<Cart> FB__Cart { get; set; }
        public DbSet<Vendor> FB__Vendor { get; set; }
        public DbSet<Feedback> FB__Feedback { get; set; }
        public DbSet<Employee> Vw_MDB_EmployeeMaster { get; set; }
        public DbSet<Menu> FB__ComponentMaster { get; set; }

    }
}

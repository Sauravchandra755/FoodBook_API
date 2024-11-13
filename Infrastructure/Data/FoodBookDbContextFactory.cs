using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FoodBookDbContextFactory : IDesignTimeDbContextFactory<FoodBookDbContext>
    {
        public FoodBookDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodBookDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TimeSheet;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new FoodBookDbContext(optionsBuilder.Options);
        }
    }
}

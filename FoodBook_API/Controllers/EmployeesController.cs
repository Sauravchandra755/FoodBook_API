using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Services;
using Application.Interfaces;
using Domain.DTO;

namespace FoodBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly FoodBookDbContext _context;
        private readonly IEmployeeService _empService;

        public EmployeesController(FoodBookDbContext context, IEmployeeService empService)
        {
            _context = context;
            _empService = empService;
            //_empService.UpdateCredit();
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllEmployees()
        {
          if (_context.FB__Employees == null)
          {
              return NotFound();
          }
            return await _context.FB__Employees.Where(a => a.Role == "Admin").ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetEmployee(int id)
        {
          if (_context.FB__Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.FB__Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet]
        [Route("GetUserByMid/{mid}")]
        public async Task<ActionResult<EmployeeResponse>> GetUserByMid(string mid)
        {
            decimal sum = 0;
            if (_context.FB__Employees == null)
            {
                return NotFound();
            }
            var empId = Convert.ToInt32(mid);
            EmployeeResponse emp = new EmployeeResponse();
            if (EmployeeExists(empId))
            {
                Admin admin = await _context.FB__Employees.FirstOrDefaultAsync(e => e.MId.ToString() == mid);
                if (admin.Role == "Vendor") {
                    emp.EmployeeId = admin.MId.ToString();
                    emp.EmployeeName = admin.EmployeeName;
                    emp.VendorId = admin.VendorId;
                    emp.FullEmailId = admin.Email;                   
                }
                else
                {
                    emp.EmployeeId = admin.MId.ToString();
                    emp.EmployeeName = admin.EmployeeName;
                    emp.VendorId = 0;
                    emp.FullEmailId = admin.Email;
                    var order = await _context.FB__Orders.Where(e => e.EmpId == empId && e.OrderDateTime.Date == DateTime.Now.Date).ToListAsync();
                    //foreach (var o in order)
                    //{
                    //    sum = o.TotalPrice + sum;
                    //}
                    emp.DailyCredits = 500000; //(sum > 0) ? 200 - sum : 200;
                }
                
            }
            else
            {
                Employee employee = await _context.Vw_MDB_EmployeeMaster.FirstOrDefaultAsync(e => e.EmployeeId == mid);

                emp.EmployeeId = employee.EmployeeId;
                emp.EmployeeName = employee.EmployeeName;
                emp.VendorId = null;
                emp.FullEmailId = employee.FullEmailId;

                var order = await _context.FB__Orders.Where(e => e.EmpId == empId && e.OrderDateTime.Date == DateTime.Now.Date).ToListAsync();
                foreach (var o in order)
                {
                    sum = o.TotalPrice + sum;
                }
                emp.DailyCredits = (sum > 0) ? 200 - sum : 200;

            }
            return emp;
        }

        //[HttpGet]
        //[Route("GetVendorList")]
        //public async Task<List<Employee>> GetVendorList()
        //{
        //    var employees = await _context.Vw_MDB_EmployeeMaster.Where(e => e.Role.ToLower() == "vendor").ToListAsync();
        //    return employees;
        //}

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] AddEmployeeRequest employee)
        {
          
            try
            {
                var emp = await _context.FB__Employees.FindAsync(id);
                if(emp!=null)
                {
                    emp.MId = employee.MId;
                    emp.EmployeeName = employee.EmployeeName;
                    //emp.UpdatedCreditsDate = DateTime.Now;
                    emp.Email = employee.Email;
                    emp.Role = employee.Role;
                    emp.isActive = true;

                };
                //_context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch 
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostEmployee(AddEmployeeRequest employee)
        {
          if (_context.FB__Employees == null)
          {
              return Problem("Entity set 'FoodBookDbContext.FB__Employees'  is null.");
          }
            var newemp = new Admin()
            {
                MId = employee.MId,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                Role = employee.Role,
                isActive = true

            };
            _context.FB__Employees.Add(newemp);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.FB__Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.FB__Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.FB__Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.FB__Employees?.Any(e => e.MId== id)).GetValueOrDefault();
        }
    }
}

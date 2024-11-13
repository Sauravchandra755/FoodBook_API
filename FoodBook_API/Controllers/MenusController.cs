using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data;
using Domain.DTO;

namespace FoodBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly FoodBookDbContext _context;

        public MenusController(FoodBookDbContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetFB__ComponentMaster()
        {
          if (_context.FB__ComponentMaster == null)
          {
              return NotFound();
          }
            return await _context.FB__ComponentMaster.ToListAsync();
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
          if (_context.FB__ComponentMaster == null)
          {
              return NotFound();
          }
            var menu = await _context.FB__ComponentMaster.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        [HttpGet("GetMenuitems")]
        //[Route]
        public async Task<ActionResult<List<Menu>>> GetMenuitems( string Role)
        {
            if (_context.FB__ComponentMaster == null)
            {
                return NotFound();
            }
            if (Role.ToLower() == "admin")
            {
                return await _context.FB__ComponentMaster.Where(m => m.Role == "Employee" || m.Role == "Admin").ToListAsync();
            }

            if (Role.ToLower() == "employee")
            {
                return await _context.FB__ComponentMaster.Where(m => m.Role == "Employee").ToListAsync();
            }

            if (Role.ToLower() == "vendor")
            {
                return await _context.FB__ComponentMaster.Where(m => m.Role == "Vendor").ToListAsync();
            }

            return NoContent();

        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
          if (_context.FB__ComponentMaster == null)
          {
              return Problem("Entity set 'FoodBookDbContext.FB__ComponentMaster'  is null.");
          }
            _context.FB__ComponentMaster.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            if (_context.FB__ComponentMaster == null)
            {
                return NotFound();
            }
            var menu = await _context.FB__ComponentMaster.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.FB__ComponentMaster.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return (_context.FB__ComponentMaster?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

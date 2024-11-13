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
using Application.Interfaces;

namespace FoodBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly FoodBookDbContext _context;
        private readonly IFoodService _foodService;

        public VendorsController(FoodBookDbContext context, IFoodService foodService)
        {
            _context = context;
            _foodService = foodService;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetFB__Vendor()
        {
          if (_context.FB__Vendor == null)
          {
              return NotFound();
          }

            
           


                return await _context.FB__Vendor.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
          if (_context.FB__Vendor == null)
          {
              return NotFound();
          }
            var vendor = await _context.FB__Vendor.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor([FromRoute]int id, [FromBody] UpdateVendorRequest updateVendor)
        {
            if (id != id)
            {
                return BadRequest();
            }
           
            //await _context.Entry(vendor).State = EntityState.Modified;

            try
            {

                var vendor = await _context.FB__Vendor.FindAsync(id);
                if (vendor != null)
                {
                    vendor.VendorName = updateVendor.VendorName;
                     
                    

                    //if (updateVendor.Flag == "true")
                    //{
                    //    vendor.Flag = true;
                    //}
                    //else
                    //{
                    //    vendor.Flag = false;
                    //}
                    vendor.Flag = updateVendor.Flag;
                    //vendor.Image = updateVendor.Image;
                    //vendor.Image = _foodService.SaveFile(updateVendor.Image);
                };
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor([FromForm]VendorRequest addvendor)
        {
          if (_context.FB__Vendor == null)
          {
              return Problem("Entity set 'FoodBookDbContext.FB__Vendor'  is null.");
          }
            var vendor = new Vendor()
            {
          
                VendorName = addvendor.VendorName,
                Flag = addvendor.Flag,
                Image = _foodService.SaveFile(addvendor.Image)
                //Image = addvendor.Image
                //Image = _foodService.SaveFile(addFood.Image)
            };
            //_context.FB__Vendor.Add(vendor);
            //var vendor = new Vendor();
            //vendor.VendorName = vendorReq.VendorName;
            //if(vendorReq.Flag == "true")
            //{
            //    vendor.Flag = true;
            //}
            //else
            //{
            //    vendor.Flag = false;
            //}
             await _context.FB__Vendor.AddAsync(vendor);
             await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.VendorId }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            if (_context.FB__Vendor == null)
            {
                return NotFound();
            }
            var vendor = await _context.FB__Vendor.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.FB__Vendor.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return (_context.FB__Vendor?.Any(e => e.VendorId == id)).GetValueOrDefault();
        }
    }
}

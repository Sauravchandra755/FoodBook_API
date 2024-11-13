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
    public class FeedbacksController : ControllerBase
    {
        private readonly FoodBookDbContext _context;

        public FeedbacksController(FoodBookDbContext context)
        {
            _context = context;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackResponse>>> GetFB__Feedback()
        {
          if (_context.FB__Feedback == null)
          {
              return NotFound();
          }
            var feedback = await _context.FB__Feedback.ToListAsync();
            List<FeedbackResponse> feedbackResponse = new List<FeedbackResponse>();
            foreach (var item in feedback)
            {
                var fbres = new FeedbackResponse();
                fbres.OrderId = item.OrderId;
                fbres.FoodId = item.FoodId;
                fbres.Rating = item.Rating;
                fbres.Comments = item.Comments;
                fbres.Order = await _context.FB__Orders.FindAsync(item.OrderId);
                fbres.Food = await _context.FB__Foods.FindAsync(item.FoodId);
                fbres.Employee = await _context.Vw_MDB_EmployeeMaster.FindAsync(Convert.ToString(item.EmployeeMid));
                feedbackResponse.Add(fbres);
            }
            return feedbackResponse;
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackResponse>> GetFeedback(int id)
        {
          if (_context.FB__Feedback == null)
          {
              return NotFound();
          }
            var feedback = await _context.FB__Feedback.FindAsync(id);
            var feedbackResponse = new FeedbackResponse();
            feedbackResponse.OrderId = feedback.OrderId;
            feedbackResponse.FoodId = feedback.FoodId;
            feedbackResponse.Rating = feedback.Rating;
            feedbackResponse.Comments = feedback.Comments;
            feedbackResponse.Order = await _context.FB__Orders.FindAsync(feedback.OrderId);
            feedbackResponse.Food = await _context.FB__Foods.FindAsync(feedback.FoodId);
            feedbackResponse.Employee = await _context.Vw_MDB_EmployeeMaster.FindAsync(Convert.ToString(feedback.EmployeeMid));

            if (feedback == null)
            {
                return NotFound();
            }

            return feedbackResponse;
        }

        [HttpGet]
        [Route("GetFeedbackByOrderId/{orderId}/{foodId}")]
        public async Task<ActionResult<FeedbackResponse>> GetFeedbackByOrderId([FromRoute]int orderId, [FromRoute] int foodId)
        {
            if (_context.FB__Feedback == null)
            {
                return NotFound();
            }
            var feedback = await _context.FB__Feedback.Where(f => f.OrderId == orderId && f.FoodId == foodId).FirstOrDefaultAsync();
            if (feedback == null)
            {
                return NotFound();
            }
            else
            {
                var feedbackResponse = new FeedbackResponse();
                feedbackResponse.Id = feedback.Id;
                feedbackResponse.OrderId = feedback.OrderId;
                feedbackResponse.FoodId = feedback.FoodId;
                feedbackResponse.EmployeeMid = feedback.EmployeeMid;
                feedbackResponse.Rating = feedback.Rating;
                feedbackResponse.Comments = feedback.Comments;
                feedbackResponse.Order = await _context.FB__Orders.FindAsync(feedback.OrderId);
                feedbackResponse.Food = await _context.FB__Foods.FindAsync(feedback.FoodId);
                feedbackResponse.Employee = await _context.Vw_MDB_EmployeeMaster.FindAsync(Convert.ToString(feedback.EmployeeMid));
                return feedbackResponse;
            }
            

            
        }

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch 
            {
                throw;
                //if (!FeedbackExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(FeedbackRequest feedbackreq)
        {
            if (_context.FB__Feedback == null)
            {
                  return Problem("Entity set 'FoodBookDbContext.FB__Feedback'  is null.");
            }

            if (FeedbackExists(feedbackreq.OrderId, feedbackreq.FoodId, feedbackreq.EmployeeMid))
            {
                var feed = new Feedback()
                {
                    Id = feedbackreq.Id,
                    OrderId = feedbackreq.OrderId,
                    FoodId = feedbackreq.FoodId,
                    EmployeeMid = feedbackreq.EmployeeMid,
                    Rating = feedbackreq.Rating,
                    Comments = feedbackreq.Comments
                };
                await PutFeedback(feedbackreq.Id, feed);
                return Ok();
            }
            else
            {
                var feedback = new Feedback()
                {
                    OrderId = feedbackreq.OrderId,
                    FoodId = feedbackreq.FoodId,
                    EmployeeMid = feedbackreq.EmployeeMid,
                    Rating = feedbackreq.Rating,
                    Comments = feedbackreq.Comments
                };
                _context.FB__Feedback.Add(feedback);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetFeedback", new { id = feedback.Id }, feedback);
            }
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            if (_context.FB__Feedback == null)
            {
                return NotFound();
            }
            var feedback = await _context.FB__Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.FB__Feedback.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackExists(int orderId, int foodId, int empId)
        {
            return (_context.FB__Feedback?.Any(e => e.OrderId == orderId && e.FoodId == foodId && e.EmployeeMid == empId)).GetValueOrDefault();
        }
    }
}

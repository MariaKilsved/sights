using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sqlite.Data;
using sqlite.Models;

namespace sights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCommentController : ControllerBase
    {
        private readonly SqliteContext _context;

        public SubCommentController(SqliteContext context)
        {
            _context = context;
        }

        // GET: api/SubComment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubComment>>> GetSubComments()
        {
          if (_context.SubComments == null)
          {
              return NotFound();
          }
            return await _context.SubComments.ToListAsync();
        }

        // GET: api/SubComment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubComment>> GetSubComment(long id)
        {
          if (_context.SubComments == null)
          {
              return NotFound();
          }
            var subComment = await _context.SubComments.FindAsync(id);

            if (subComment == null)
            {
                return NotFound();
            }

            return subComment;
        }

        // PUT: api/SubComment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubComment(long id, SubComment subComment)
        {
            if (id != subComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(subComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCommentExists(id))
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

        // POST: api/SubComment/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SubComment>> PostSubComment(SubComment subComment)
        {
          if (_context.SubComments == null)
          {
              return Problem("Entity set 'SqliteContext.SubComments'  is null.");
          }
            if (subComment.UserId == null)
            {
                return BadRequest("userId is null");

            }

            if(subComment.CommentId == null)
            {
                return NotFound("A subcomment must be attached to a main comment");
            }

            if (string.IsNullOrWhiteSpace(subComment.Content))
            {
                return BadRequest("You must write something");
            }

            _context.SubComments.Add(subComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubComment", new { id = subComment.Id }, subComment);
        }

        // DELETE: api/SubComment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubComment(long id)
        {
            if (_context.SubComments == null)
            {
                return NotFound();
            }
            var subComment = await _context.SubComments.FindAsync(id);
            if (subComment == null)
            {
                return NotFound();
            }

            _context.SubComments.Remove(subComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCommentExists(long id)
        {
            return (_context.SubComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sqlite.Data;
using sqlite.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/SubComment/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

        //Example jwt
        // POST: api/SubComment/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
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

            if (subComment.CommentId == null)
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

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSG_API.Models;

namespace CSG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly CarContext _context;

        public ScoresController(CarContext context)
        {
            _context = context;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scores>>> Getscores()
        {
            return await _context.scores.ToListAsync();
        }

        // GET: api/Scores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scores>> GetScores(int id)
        {
            var scores = await _context.scores.FindAsync(id);

            if (scores == null)
            {
                return NotFound();
            }

            return scores;
        }

        // GET: api/Users/Bob
        [HttpGet("name/{id}")]
        public async Task<ActionResult<Scores>> GetUserByName(string id)
        {
            var user = await _context.scores.Where(b => b.username == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScores(int id, Scores scores)
        {
            if (id != scores.userid)
            {
                return BadRequest();
            }

            _context.Entry(scores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoresExists(id))
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

        // POST: api/Scores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Scores>> PostScores(Scores scores)
        {
            _context.scores.Add(scores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScores", new { id = scores.userid }, scores);
        }

        // DELETE: api/Scores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Scores>> DeleteScores(int id)
        {
            var scores = await _context.scores.FindAsync(id);
            if (scores == null)
            {
                return NotFound();
            }

            _context.scores.Remove(scores);
            await _context.SaveChangesAsync();

            return scores;
        }

        private bool ScoresExists(int id)
        {
            return _context.scores.Any(e => e.userid == id);
        }
    }
}

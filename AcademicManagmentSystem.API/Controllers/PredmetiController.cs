using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredmetiController : ControllerBase
    {
        private readonly AcademicManagmentSystemDbContext _context;

        public PredmetiController(AcademicManagmentSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Predmeti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Predmet>>> GetPredmeti()
        {
            //Kao da pise SELECT * FROM PREMDETI
            return Ok(await _context.Predmeti.ToListAsync()); // Ok-om samo vracamo 200 odg umesto 201 koji bi se generealno vracao kao uspesan odg (estetika)
        }

        // GET: api/Predmeti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Predmet>> GetPredmet(int id)
        {
            var predmet = await _context.Predmeti.FindAsync(id);

            if (predmet == null)
            {
                return NotFound("Ne postoji trazeni predemt!");
            }

            return predmet;
        }

        // PUT: api/Predmeti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPredmet(int id, Predmet predmet)
        {
            if (id != predmet.PredmetId)
            {
                return BadRequest();
            }

            _context.Entry(predmet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PredmetExists(id))
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

        // POST: api/Predmeti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Predmet>> PostPredmet(Predmet predmet)
        {
            _context.Predmeti.Add(predmet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPredmet", new { id = predmet.PredmetId }, predmet);
        }

        // DELETE: api/Predmeti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePredmet(int id)
        {
            var predmet = await _context.Predmeti.FindAsync(id);
            if (predmet == null)
            {
                return NotFound();
            }

            _context.Predmeti.Remove(predmet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PredmetExists(int id)
        {
            return _context.Predmeti.Any(e => e.PredmetId == id);
        }
    }
}

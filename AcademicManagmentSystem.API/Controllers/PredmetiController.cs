using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Predmeti;
using AutoMapper;
using AcademicManagmentSystem.API.Models.Predavaci;
using AcademicManagmentSystem.API.Models.Katedre;

namespace AcademicManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredmetiController : ControllerBase
    {
        private readonly AcademicManagmentSystemDbContext _context;
        private readonly IMapper _mapper;

        public PredmetiController(AcademicManagmentSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Predmeti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPredmetDto>>> GetPredmeti()
        {
            //Kao da pise SELECT * FROM PREMDETI
            // return Ok(await _context.Predmeti.ToListAsync()); // Ok-om samo vracamo 200 odg umesto 201 koji bi se generealno vracao kao uspesan odg (estetika)

            var predmeti = await _context.Predmeti.ToListAsync();
            var records = _mapper.Map<List<GetPredmetDto>>(predmeti);
            return Ok(records);
        }

        // GET: api/Predmeti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPredmetDetailsDto>> GetPredmet(int id)
        {
            var predmet = await _context.Predmeti
        .Include(p => p.PredmetPredavaci )
            .ThenInclude(pp => pp.Predavac)
                .ThenInclude(pr => pr.Katedra)
                    
        //.Where(p => p.PredmetId == id)
        //.Select(p => new GetPredmetDetailsDto
        //{
        //    Id = p.PredmetId,
        //    Naziv = p.Naziv,
        //    Sifra = p.Sifra,
        //    PredmetPredavaci = p.PredmetPredavaci.Select(pp => new GetPredmetPredavacDto
        //    {
        //        PredavacId = pp.PredavacId,
        //        Predavac = new GetPredavacDto
        //        {
        //            PredavacId = pp.Predavac.PredavacId,
        //            Ime = pp.Predavac.Ime,
        //            Prezime = pp.Predavac.Prezime,
        //            Username = pp.Predavac.Username,
        //            KatedraId = pp.Predavac.KatedraId,
        //            Katedra = new GetKatedraDto
        //            {
        //                KatedraId = pp.Predavac.Katedra.KatedraID,
        //                Naziv = pp.Predavac.Katedra.Naziv
        //            }
        //        }
        //    }).ToList()
        //})
        .FirstOrDefaultAsync(p => p.PredmetId == id);

            predmet = await _context.Predmeti.Include(p => p.Delovi)
                                                .ThenInclude(pr => pr.Rezultati)
                                                    .ThenInclude(ps => ps.Student)
                                             .FirstOrDefaultAsync(p => p.PredmetId == id);
            var predmetDto = _mapper.Map<GetPredmetDetailsDto>(predmet);
            if (predmetDto == null)
            {
                return NotFound("Ne postoji trazeni predemt!");
            }

            return predmetDto;
        }

        // PUT: api/Predmeti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPredmet(int id, UpdatePredmetDto updatePredmetDto)
        {
            if (id != updatePredmetDto.predmetId)
            {
                return BadRequest();
            }

            //_context.Entry(predmet).State = EntityState.Modified;

            var predmet = await _context.Predmeti.FindAsync(id);

            if(predmet == null)
            {
                return NotFound();
            }

            _mapper.Map(updatePredmetDto, predmet);

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


        //////////////////////////////////////////////////////////////////////////
        

        //// PUT: api/Predmeti/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("api/Predmeti/details/{id}")]
        //public async Task<IActionResult> PutDetailsPredmet(int id, UpdatePredmetDetailsDto updatePredmetDetailsDto)
        //{
        //    if (id != updatePredmetDetailsDto.predmetId)
        //    {
        //        return BadRequest();
        //    }

        //    var predmet = await _context.Predmeti.FindAsync(id);

        //    if (predmet == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(updatePredmetDetailsDto, predmet);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PredmetExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        //////////////////////////////////////////////////////////////////////////




        // POST: api/Predmeti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        
        [HttpPost]
        public async Task<ActionResult<Predmet>> PostPredmet(CreatePredmetDto kreirajPredmetDto)
        {
            //var predmet = new Predmet
            //{
            //    Naziv = kreirajPredmet.Naziv,
            //    Sifra = kreirajPredmet.Sifra
            //};

            var predmet = _mapper.Map<Predmet>(kreirajPredmetDto);

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

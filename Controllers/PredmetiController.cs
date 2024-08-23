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
using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Services.Interface;
using AcademicManagmentSystem.API.Services.Implementation;

namespace AcademicManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredmetiController : ControllerBase
    {
        private readonly IPredmetService _predmetService;

        public PredmetiController(IPredmetService predmetService)
        {
            _predmetService = predmetService;
        }

        // GET: api/Predmeti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPredmetDto>>> GetPredmeti()
        {      
            try
            {
                var results = await _predmetService.GetAllSubjects();
                return Ok(results);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Predmeti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPredmetDetailsDto>> GetPredmet(int id)
        {
            try
            {
                var results = await _predmetService.GetDetailsForSubject(id);
                return Ok(results);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Predmeti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPredmet(int id, UpdatePredmetDto updatePredmetDto)
        {
            try
            {
                var result = await _predmetService.UpdateSubject(id, updatePredmetDto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Za sve ostale neočekivane greške
                return StatusCode(500, "Došlo je do greške na serveru.");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Predmet>> PostPredmet(CreatePredmetDto kreirajPredmetDto)
        {
             try
            {
                var result = await _predmetService.CreateSubject(kreirajPredmetDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Predmeti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePredmet(int id)
        {
            var success = await _predmetService.DeletePredmetAsync(id);
            if (!success)
            {
                return NotFound(); // Deo nije pronađen
            }

            return NoContent(); // Uspešno obrisano
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Predmeti;
using AcademicManagmentSystem.API.Core.Services.Interface;

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

        // GET: api/Predmeti/details/5
        [HttpGet("details/{id}")]
        public async Task<ActionResult<GetPredmetDetailsDto>> GetDetailsForSubject(int id)
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

        // GET: api/Predmeti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UpdatePredmetDto>> GetSubject(int id)
        {
            try
            {
                var results = await _predmetService.GetSubject(id);
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
        public async Task<IActionResult> UpdateSubject(int id, UpdatePredmetDto updatePredmetDto)
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
        public async Task<ActionResult<Predmet>> CreateSubject(CreatePredmetDto kreirajPredmetDto)
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
                return NotFound();
            }

            return NoContent();
        }
    }
}

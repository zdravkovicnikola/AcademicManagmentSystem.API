using Microsoft.AspNetCore.Mvc;
using AcademicManagmentSystem.API.Core.Services.Interface;
using AcademicManagmentSystem.API.Core.Models.Predmeti;
using AcademicManagmentSystem.API.Core.Models;

namespace AcademicManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeloviController : ControllerBase
    {
        private readonly ILogger<DeloviController> _logger;
        private readonly ICsvService _csvService;
        private readonly IUploadExamService _uploadExamService;
        private readonly IDeloviService _deloviService;
        private readonly IPendingChangesService _pendingChangesService;

        public DeloviController(ILogger<DeloviController> logger, ICsvService csvService, IUploadExamService uploadExamService, IDeloviService deloviService, IPendingChangesService pendingChangesService)
        {
            _logger = logger;
            _csvService = csvService;
            _uploadExamService = uploadExamService;
            _deloviService = deloviService;
            _pendingChangesService = pendingChangesService;
        }

        // GET: api/Delovi
        [HttpGet("results")]
        public async Task<IActionResult> GetAllResultsForSubject(int predmetId, DateTime datum, int sifraTipa )
        {
            try
            {
                var results = await _deloviService.GetAllResultsForSubject(predmetId, datum , sifraTipa);
                return Ok(results);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Delovi
        [HttpGet("resultsForStudent")]
        public async Task<IActionResult> GetAllResultsForStudent(string sifraPredmeta, string indeks)
        {
            try
            {
                var results = await _deloviService.GetAllResultsForSubjectAndStudent(sifraPredmeta, indeks);
                return Ok(results);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("results/students")]
        public async Task<IActionResult> GetResultsByPredmet(string sifraPredmeta, DateTime startDate, DateTime endDate)
        {
            try
            {
                var results = await _deloviService.GetResultsByPredmet(sifraPredmeta, startDate, endDate);
                return Ok(results);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("upload-usmeni/{predmetId}")]
        public async Task<IActionResult> UploadUsmeni(IFormFile file, int predmetId)
        {
            var errorResponse = _csvService.ValidateCsvFile(file);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            errorResponse = _csvService.ValidateCsvContent(records, isUsmeni: true);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid CSV content");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            _logger.LogInformation("CSV data loaded successfully");
            var validRecords = records.Skip(1);

            foreach (var record in validRecords)
            {
                await _uploadExamService.ProcessOrUpdateUsmeniRecord(record,false, predmetId);
            }

            return Ok("Sistem je uspešno sačuvao rezultate usmenog dela ispita.");
        }

        [HttpPost("upload-prakticni/{predmetId}")]
        public async Task<IActionResult> UploadPrakticni(IFormFile file, int predmetId)
        {
            var errorResponse = _csvService.ValidateCsvFile(file);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            errorResponse = _csvService.ValidateCsvContent(records, isUsmeni: false);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid CSV content");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            _logger.LogInformation("CSV data loaded successfully");
            var validRecords = records.Skip(1);

            foreach (var record in validRecords)
            {
                await _uploadExamService.ProcessOrUpdatePrakticniRecord(record, false, predmetId);
            }
            return Ok("Sistem je uspešno sačuvao rezultate pismenog dela ispita.");
        }

        [HttpPut("update-prakticni/{predmetId}")]
        public async Task<IActionResult> UpdatePrakticni(IFormFile file, int predmetId)
        {
            var errorResponse = _csvService.ValidateCsvFile(file);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            errorResponse = _csvService.ValidateCsvContent(records, isUsmeni: false);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid CSV content");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            _logger.LogInformation("CSV data loaded successfully");

            var validRecords = records.Skip(1);
            foreach (var record in validRecords)
            {
                await _uploadExamService.ProcessOrUpdatePrakticniRecord(record, true, predmetId);
            }

            return Ok("Sistem je uspešno ažurirao rezultate praktičnog dela.");
        }

        [HttpPut("update-usmeni/{predmetId}")]
        public async Task<IActionResult> UpdateUsmeni(IFormFile file, int predmetId)
        {
            var errorResponse = _csvService.ValidateCsvFile(file);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            errorResponse = _csvService.ValidateCsvContent(records, isUsmeni: true);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid CSV content");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            _logger.LogInformation("CSV data loaded successfully");
            var validRecords = records.Skip(1);

            foreach (var record in validRecords)
            {
                await _uploadExamService.ProcessOrUpdateUsmeniRecord(record,true, predmetId);
            }

            return Ok("Sistem je uspešno ažurirao rezultate usmenog dela .");
        }

        [HttpPut("upload-prakticni/uvid/{predmetId}")]
        public async Task<IActionResult> UploadPrakticniUvid(IFormFile file, int predmetId)
        {
            var errorResponse = _csvService.ValidateCsvFile(file);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            errorResponse = _csvService.ValidateCsvContent(records, isUsmeni: false);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid CSV content");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            _logger.LogInformation("CSV data loaded successfully");
            var validRecords = records.Skip(1);

            foreach (var record in validRecords)
            {
                await _pendingChangesService.ProcessPendingPrakticni(record, predmetId);
            }
            return Ok(await _pendingChangesService.ReturnListPendingStudents());
        }

        [HttpPut("upload-usmeni/uvid/{predmetId}")]
        public async Task<IActionResult> UploadUsmeniUvid(IFormFile file, int predmetId)
        {
            var errorResponse = _csvService.ValidateCsvFile(file);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            errorResponse = _csvService.ValidateCsvContent(records, isUsmeni: true);
            if (errorResponse != null)
            {
                _logger.LogWarning("Invalid CSV content");
                return BadRequest($"Sistem nije uspeo da obradi zahtev. Razlog: {errorResponse}");
            }

            _logger.LogInformation("CSV data loaded successfully");
            var validRecords = records.Skip(1);

            foreach (var record in validRecords)
            {
                await _pendingChangesService.ProcessPendingUsmeni(record, predmetId);
            }
            return Ok(await _pendingChangesService.ReturnListPendingStudents());
        }


        [HttpGet("pending/all")]
        public IActionResult GetAllPendingChanges()
        {
            var pendingChanges = _pendingChangesService.GetAllPendingChanges();
            return Ok(pendingChanges);
        }

        [HttpDelete("rollback/{guid}")]
        public async Task<IActionResult> RemovePendingChanges(Guid guid)
        {
            var result = await _pendingChangesService.RemovePendingChanges(guid);

            if (!result)
            {
                return BadRequest("Nema pending promena za uklanjanje za dati GUID.");
            }

            return Ok("Pending promene su uspešno uklonjene.");
        }

        [HttpPost("commit-pending/{guid}")]
        public async Task<IActionResult> CommitPendingChanges(Guid guid)
        {
            var result = await _pendingChangesService.CommitPendingChanges(guid);

            if (!result)
            {
                return BadRequest("Nema pending promena za upis u bazu za dati GUID.");
            }

            return Ok("Pending promene su uspešno upisane u bazu.");
        }
        
        [HttpGet("pre-commit/all")]
        public IActionResult GetAllRollbacks()
        {
            var pendingChanges = _pendingChangesService.GetAllRollbacks();
            return Ok(pendingChanges);
        }
        
        [HttpPost("ponisti-commit/{guid}")]
        public async Task<IActionResult> RollbackPendingChanges(string guid)
        {
            var result = await _pendingChangesService.RollbackChanges(Guid.Parse(guid)); if (!result)
            {
                return BadRequest("Nema rollback promena za upis u bazu za dati GUID.");
            }

            return Ok("Promene su uspešno rollback-ovane za dati GUID.");
        }

        // DELETE: api/Delovi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeo(int id)
        {
            var success = await _deloviService.DeleteDeoAsync(id);
            if (!success)
            {
                return NotFound(); // Deo nije pronađen
            }

            return NoContent(); // Uspešno obrisano
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using AcademicManagmentSystem.API.Services.Interface;

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
        public async Task<IActionResult> GetAllResultsForSubject(string sifraPredmeta)
        {
            try
            {
                var results = await _deloviService.GetAllResultsForSubject(sifraPredmeta);
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

        

        [HttpPost("upload-usmeni")]
        public async Task<IActionResult> UploadUsmeni(IFormFile file)
        {
            _logger.LogInformation("UploadUsmeni method called");
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest("Invalid file.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            foreach (var record in records)
            {
                await _uploadExamService.ProcessOrUpdateUsmeniRecord(record,false);
            }

            return Ok("Usmeni CSV data successfully uploaded.");
        }


        [HttpPost("upload-prakticni")]
        public async Task<IActionResult> UploadPrakticni(IFormFile file)
        {
            _logger.LogInformation("UploadPrakticni method called");
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest("Invalid file.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            _logger.LogInformation("CSV data loaded");
            foreach (var record in records)
            {
                await _uploadExamService.ProcessOrUpdatePrakticniRecord(record, false);
            }

            return Ok("Prakticni CSV data successfully uploaded.");
        }

        [HttpPut("update-prakticni")]
        public async Task<IActionResult> UpdatePrakticni(IFormFile file)
        {
            _logger.LogInformation("UploadPrakticni method called");
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest("Invalid file.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            _logger.LogInformation("CSV data loaded");
            foreach (var record in records)
            {
                await _uploadExamService.ProcessOrUpdatePrakticniRecord(record, true);
            }

            return Ok("Prakticni CSV data successfully uploaded.");
        }

        [HttpPut("update-usmeni")]
        public async Task<IActionResult> UpdateUsmeni(IFormFile file)
        {
            _logger.LogInformation("UpdateUsmeni method called");
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest("Invalid file.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            foreach (var record in records)
            {
                await _uploadExamService.ProcessOrUpdateUsmeniRecord(record,true);
            }

            return Ok("Usmeni CSV data successfully uploaded.");
        }

        [HttpPut("upload-prakticni/uvid")]
        public async Task<IActionResult> UploadPrakticniUvid(IFormFile file)
        {
            _logger.LogInformation("UploadUsmeni method called");
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest("Invalid file.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            foreach (var record in records)
            {
                await _pendingChangesService.ProcessPendingPrakticni(record);
            }
            return Ok(await _pendingChangesService.ReturnListPendingStudents());
        }

        [HttpPut("upload-usmeni/uvid")]
        public async Task<IActionResult> UploadUsmeniUvid(IFormFile file)
        {
            _logger.LogInformation("UploadUsmeni method called");
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Invalid file");
                return BadRequest("Invalid file.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            _logger.LogInformation($"Saving file to {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var records = _csvService.LoadCsvData(filePath);

            foreach (var record in records)
            {
                await _pendingChangesService.ProcessPendingUsmeni(record);
            }
            return Ok(await _pendingChangesService.ReturnListPendingStudents());
        }

        [HttpPost("commit-pending")]
        public async Task<IActionResult> CommitPendingChanges()
        {
            var result = await _pendingChangesService.CommitPendingChanges();

            if (result == false)
            {
                return BadRequest("Nije bilo pending promena za upis u bazu.");
            }

            return Ok("Pending promene su uspešno upisane u bazu.");
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

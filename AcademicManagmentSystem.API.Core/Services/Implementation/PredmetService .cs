using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Core.Models.Predmeti;
using AcademicManagmentSystem.API.Core.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagmentSystem.API.Core.Services.Implementation
{
    public class PredmetService : IPredmetService
    {
        private readonly IPredmetiRepository _predmetiRepository;
        private readonly IMapper _mapper;
        private readonly IDeloviService _deloviService;

        public PredmetService(IPredmetiRepository predmetiRepository, IMapper mapper, IDeloviService deloviService)
        {
            _predmetiRepository = predmetiRepository;
            _mapper = mapper;
            _deloviService = deloviService;
        }

        public async Task<Predmet> CreateSubject(CreatePredmetDto createPredmetDto)
        {
            var predmet = _mapper.Map<Predmet>(createPredmetDto);
            await _predmetiRepository.AddAsync(predmet);
            return predmet;
        }

        public async Task<bool> DeletePredmetAsync(int id)
        {
            var predmet = await _predmetiRepository.GetAsync(id);
            if (predmet == null)
            {
                return false;
            }
            await _predmetiRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<GetPredmetDto>> GetAllSubjects()
        {
            var predmeti = await _predmetiRepository.GetAllAsync();
            var records = _mapper.Map<List<GetPredmetDto>>(predmeti);
            return records;
        }

        public async Task<GetPredmetDetailsDto> GetDetailsForSubject(int id)
        {
            var predmet = await _predmetiRepository.GetDetailsPredavac(id);
            // Fetch the distinct types for the subject
            var tipovi = await _deloviService.GetTipoviForSubject(id);

            if (predmet == null || tipovi == null)
            {
                throw new InvalidOperationException("Ne postoji predmet sa zadatom šifrom. Molimo proverite unos.");
            }

            var predmetDto = _mapper.Map<GetPredmetDetailsDto>(predmet);

            // Assuming predmetDto.Delovi should be a list of some DTO for Tip, not directly Tip
            predmetDto.Delovi = tipovi;

            return predmetDto;
        }

        public async Task<int?> GetPredmetIdBySifraAsync(string sifra)
        {
            var predmet = await _predmetiRepository.GetAsyncByPass(sifra);
            return predmet?.PredmetId;
        }

        public async Task<Predmet> UpdateSubject(int id, UpdatePredmetDto updatePredmetDto)
        {
            var predmet = await _predmetiRepository.GetAsync(id);
            if (predmet == null)
            {
                throw new InvalidOperationException("Predmet sa zadatim ID-om ne postoji.");
            }

            _mapper.Map(updatePredmetDto, predmet);

            try
            {
                await _predmetiRepository.UpdateAsync(predmet);
                return predmet;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _predmetiRepository.Exists(id))
                {
                    throw new InvalidOperationException("Predmet sa zadatim ID-om više ne postoji.");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

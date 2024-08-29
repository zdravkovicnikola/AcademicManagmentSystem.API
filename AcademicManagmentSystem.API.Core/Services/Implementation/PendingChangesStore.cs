using AcademicManagmentSystem.API.Core.Models.Delovi;
using AcademicManagmentSystem.API.Core.Models.Studenti;
using AcademicManagmentSystem.API.Core.Services.Interface;
using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Core.Services.Implementation
{
    public class PendingChangesStore : IPendingChangesStore
    {
        private readonly Dictionary<Guid, List<PendingStudentDto>> _pendingStudentsDict = new Dictionary<Guid, List<PendingStudentDto>>();
        private readonly Dictionary<Guid, List<PendingStudentDto>> _rollbackStore = new Dictionary<Guid, List<PendingStudentDto>>();

        public void AddPendingStudents(Guid key, List<PendingStudentDto> students)
        {
            _pendingStudentsDict[key] = students;
        }

        public List<KeyValuePair<Guid, List<PendingStudentDto>>> GetAllPendingChanges()
        {
            return _pendingStudentsDict.ToList();
        }

        // Vrati pending studente na osnovu GUID-a
        public List<PendingStudentDto> GetPendingStudents(Guid key)
        {
            _pendingStudentsDict.TryGetValue(key, out var students);
            return students;
        }

        // Ukloni pending studente na osnovu GUID-a
        public void RemovePendingStudents(Guid key)
        {
            _pendingStudentsDict.Remove(key);
        }
        public void AddToRollbackStore(Guid key, List<PendingStudentDto> rollbackData)
        {
        _rollbackStore[key] = rollbackData;
        }
    
        public List<PendingStudentDto> GetRollbackData(Guid key)
        {
        _rollbackStore.TryGetValue(key, out var rollbackData);
        return rollbackData;
        }

        public void RemoveRollbackData(Guid key)
        {
        _rollbackStore.Remove(key);
        }

        public List<KeyValuePair<Guid, List<PendingStudentDto>>> GetRollbackData()
        {
        return _rollbackStore.ToList();
        }


    }

}

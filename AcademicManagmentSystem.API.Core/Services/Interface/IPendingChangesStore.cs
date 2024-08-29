using AcademicManagmentSystem.API.Core.Models.Delovi;
using AcademicManagmentSystem.API.Core.Models.Studenti;
using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Core.Services.Interface
{
    public interface IPendingChangesStore
    {
        public void AddPendingStudents(Guid key, List<PendingStudentDto> students);
        public List<PendingStudentDto> GetPendingStudents(Guid key);
        public void RemovePendingStudents(Guid key);
        public List<KeyValuePair<Guid, List<PendingStudentDto>>> GetAllPendingChanges();
        public List<PendingStudentDto> GetRollbackData(Guid guid);
        public void RemoveRollbackData(Guid key);
        public void AddToRollbackStore(Guid key, List<PendingStudentDto> rollbackData);
        List<KeyValuePair<Guid, List<PendingStudentDto>>> GetRollbackData();

    }
}

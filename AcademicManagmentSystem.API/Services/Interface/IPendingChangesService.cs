using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models;
using AcademicManagmentSystem.API.Models.Studenti;

namespace AcademicManagmentSystem.API.Services.Interface
{
    public interface IPendingChangesService
    {
        Task ProcessPendingPrakticni(BasicDTO record);
        Task ProcessPendingUsmeni(BasicDTO record);
        Task<List<PendingStudentDto>> ReturnListPendingStudents();
        Task<bool> CommitPendingChanges();
    }
}

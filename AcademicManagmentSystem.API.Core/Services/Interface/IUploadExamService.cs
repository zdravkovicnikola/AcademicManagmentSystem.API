using AcademicManagmentSystem.API.Core.Models;
using AcademicManagmentSystem.API.Core.Models.Studenti;

namespace AcademicManagmentSystem.API.Core.Services.Interface
{
    public interface IUploadExamService
    {
        public Task ProcessOrUpdatePrakticniRecord(BasicDTO record, bool isUpdate);
        public Task ProcessOrUpdateUsmeniRecord(BasicDTO record, bool isUpdate);
    }
}

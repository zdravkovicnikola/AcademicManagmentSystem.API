using AcademicManagmentSystem.API.Models;

namespace AcademicManagmentSystem.API.Services.Interface
{
    public interface IUploadExamService
    {
        public Task ProcessOrUpdatePrakticniRecord(BasicDTO record, bool isUpdate);
        public Task ProcessOrUpdateUsmeniRecord(BasicDTO record, bool isUpdate);
    }
}

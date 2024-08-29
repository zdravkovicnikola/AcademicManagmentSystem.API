﻿using AcademicManagmentSystem.API.Core.Models;
using AcademicManagmentSystem.API.Core.Models.Studenti;
using AcademicManagmentSystem.API.Data;

namespace AcademicManagmentSystem.API.Core.Services.Interface
{
    public interface IPendingChangesService
    {
        Task ProcessPendingPrakticni(BasicDTO record);
        Task ProcessPendingUsmeni(BasicDTO record);
        Task<List<PendingStudentDto>> ReturnListPendingStudents();
        Task<bool> CommitPendingChanges(Guid guid);
        public List<KeyValuePair<Guid, List<PendingStudentDto>>> GetAllPendingChanges();
        public List<KeyValuePair<Guid, List<PendingStudentDto>>> GetAllRollbacks();
        Task<bool> RollbackChanges(Guid guid);
        Task<bool> RemovePendingChanges(Guid guid);

    }
}
using AcademicManagmentSystem.API.Core.Services.Interface;

namespace AcademicManagmentSystem.API.Core.Services.Implementation
{
    public class EmailService: IEmailService
    {
        public string ExtractIndexFromEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            var parts = email.Split('@');
            if (parts.Length == 0) return null;

            var localPart = parts[0];
            if (localPart.Length < 8) return null;

            var year = localPart.Substring(2, 4);
            var index = localPart.Substring(6);
            return $"{index}/{year}"; ;
        }
    }
}

using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Predmeti;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace AcademicManagmentSystem.API.Configurations
{
    public class MapperConfig :Profile
    {
        public MapperConfig()
        {
            CreateMap<Predmet, CreatePredmetDto>().ReverseMap();
        }
    }
}

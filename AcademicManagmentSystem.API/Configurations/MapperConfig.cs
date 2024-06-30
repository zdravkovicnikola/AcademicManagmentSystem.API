using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Katedre;
using AcademicManagmentSystem.API.Models.Predavaci;
using AcademicManagmentSystem.API.Models.Predmeti;
using AcademicManagmentSystem.API.Models.Rezultati;
using AcademicManagmentSystem.API.Models.Studenti;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace AcademicManagmentSystem.API.Configurations
{
    public class MapperConfig :Profile
    {
        public MapperConfig()
        {
            //Kreiranje predmeta
            CreateMap<Predmet, CreatePredmetDto>().ReverseMap();
            
            //Prikaz predmeta
            CreateMap<Predmet, GetPredmetDto>().ReverseMap();

            //Detaljni prikaz Predmeta
            CreateMap<Predmet, GetPredmetDetailsDto>().ReverseMap();
            //Predavac
            CreateMap<PredmetPredavac, GetPredmetPredavacDto>().ReverseMap();
            CreateMap<Predavac, GetPredavacDto>().ReverseMap();
            CreateMap<Katedra, GetKatedraDto>().ReverseMap(); 
            //Deo predmeta
            CreateMap<Deo, GetDeoDto>().ReverseMap();
            CreateMap<Rezultat, GetRezultatDto>().ReverseMap();
            CreateMap<Student, GetStudentDto>().ReverseMap();
        }
    }
}
 
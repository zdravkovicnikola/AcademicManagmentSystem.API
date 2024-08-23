﻿using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Models.Delovi;
using AcademicManagmentSystem.API.Models.Katedre;
using AcademicManagmentSystem.API.Models.Ocena;
using AcademicManagmentSystem.API.Models.Predavaci;
using AcademicManagmentSystem.API.Models.Predmeti;
using AcademicManagmentSystem.API.Models.Studenti;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Deo = AcademicManagmentSystem.API.Data.Deo;

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
            CreateMap<Deo, DeoDto>().ReverseMap();
            CreateMap<Deo, StudentDeoDto>().ReverseMap();
            CreateMap<Student, GetStudentDto>().ReverseMap();

            //Student
            CreateMap<Student, StudentWithDeoDto>()
            .ForMember(dest => dest.Delovi, opt => opt.Ignore()); // Ignorisano jer se posebno mapira

            CreateMap<Student, PendingStudentDto>();

            CreateMap<Deo, DeoForStudentDto>()
            .ForMember(dest => dest.Tip, opt => opt.MapFrom(src => src.Tip));

            CreateMap<Deo, DeoForPendingStudentDto>();


            //Maprianje ocene
            CreateMap<Ocena, OcenaDto>();
            CreateMap<CreateOcenaDto, Ocena>();
            CreateMap<UpdateOcenaDto, Ocena>();

            //Azuriranje predmeta
            CreateMap<Predmet, UpdatePredmetDto>().ReverseMap();
        }
    }
}
 
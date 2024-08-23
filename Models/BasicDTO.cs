﻿using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AcademicManagmentSystem.API.Models
{
    public class BasicDTO
    {
        public List<string> Data { get; set; }

        public BasicDTO()
        {
            Data = new List<string>();
        }
     
    }

}

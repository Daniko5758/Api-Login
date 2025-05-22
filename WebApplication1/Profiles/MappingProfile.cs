using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<CourseAddDTO, Course>();
            CreateMap<CourseUpdateDTO, Course>();

        }
    }
}
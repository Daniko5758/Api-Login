using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class CourseDTO
    {
        public int CourseID { get; set; }  
        public string CourseName { get; set; } = null!;
        public String CourseDescription { get; set;} = null!;
        public double Duration { get; set; }
        public CategoryDTO? category { get; set; }
    }
}
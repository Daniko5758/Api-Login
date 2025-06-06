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
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public double Duration { get; set; }
    public CategoryDTO category { get; set; }
    public InstructorDTO Instructor { get; set; } 
    }
}
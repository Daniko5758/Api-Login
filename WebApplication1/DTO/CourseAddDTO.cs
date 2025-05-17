using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class CourseAddDTO
    {
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public double Duration { get; set; }
    public int CategoryID { get; set; }
    public int InstructorID { get; set; } // Tambahan
        

    }
}
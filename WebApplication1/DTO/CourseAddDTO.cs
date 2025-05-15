using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class CourseAddDTO
    {
        public string CourseName { get; set; } = null!;
        public String CourseDescription { get; set;} = null!;
        public double Duration { get; set; }
        public int CategoryID { get; set; } 

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ViewCourseWithCategory
    {
        public int CourseID { get; set; }   
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set;}
        public double Duration { get; set; }
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; } = null!;

    }
}
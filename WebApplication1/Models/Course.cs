    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace WebApplication1.Models
    {
        public class Course
        {
            public int CourseID { get; set; }  
            public string CourseName { get; set; } = null!;
            public String CourseDescription { get; set;} = null!;
            public double Duration { get; set; }
            public int CategoryID { get; set; } 


            public Category? category { get; set; } = null!;

        }
    }
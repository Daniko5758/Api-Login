    using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
    using System.Threading.Tasks;

    namespace WebApplication1.Models
    {
    public class Course
    {
    public int CourseID { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public Double Duration { get; set; }

    [Column("CategoryId")]
    public int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public Category category { get; set; }

    [Column("InstructorId")]
    public int InstructorID { get; set; }
    [ForeignKey("InstructorID")]
    public Instructor Instructor { get; set; }
     

        }
    }
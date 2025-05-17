using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models;
[Table("Instructor")]
public class Instructor
{
  [Key]
  [Column("InstructorId")]
    public int InstructorID { get; set; }
    public string InstructorName { get; set; }
    public string InstructorEmail { get; set; }
    public string InstructorPhoneNumber { get; set; }
    public string InstructorAddress { get; set; }
    public string InstructorCity { get; set; }

    public ICollection<Course> Courses { get; set; }
}

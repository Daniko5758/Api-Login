using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
[Table("Categories")]
public class Category
{
    [Key]
    [Column("CategoryId")]
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = null!;
    public IEnumerable<Course> Courses { get; set; } = new List<Course>();
}

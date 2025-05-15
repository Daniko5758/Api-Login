using System;

namespace WebApplication1.Models;

public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = null!;
    public IEnumerable<Course> Courses { get; set; } = new List<Course>();
}

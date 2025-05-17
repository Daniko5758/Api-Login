using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface ICourse
    {
    List<Course> GetAllCourse();
    Course GetCourseByIdCourse(int id);
    Course AddCourse(Course course);
    Course UpdateCourse(Course course);
    public bool DeleteCourse(int id);
       
    }
}
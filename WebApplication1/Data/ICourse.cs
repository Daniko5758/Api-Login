using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface ICourse
    {
     IEnumerable<Course> GetCourses();
    Course GetCourseById(int CourseID);
    Course AddCourse(Course course);
    Course UpdateCourse(Course course);
    void DeleteCourse(int CourseID);

    IEnumerable<Course> GetCoursesByCategoryId(int categoryId);
    IEnumerable<Course> GetAllCourse();
    Course GetCourseByIdCourse(int CourseID);
    }
}
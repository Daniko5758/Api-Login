using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CourseEF : ICourse
    {
        private readonly ApplicationDbContext _context;

        public CourseEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding course", ex);
            }
        }

        public void DeleteCourse(int CourseID)
        {
            var course = GetCourseById(CourseID);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            try
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting course", ex);
            }
        }

        public Course GetCourseById(int CourseID)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == CourseID);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            return course;
        }

        public IEnumerable<Course> GetCourses()
        {
            return _context.Courses.OrderBy(c => c.CourseName).ToList();
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = GetCourseById(course.CourseID);
            if (existingCourse == null)
            {
                throw new Exception("Course not found");
            }

            try
            {
                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Duration = course.Duration;
                existingCourse.CategoryID = course.CategoryID;

                _context.Courses.Update(existingCourse);
                _context.SaveChanges();
                return existingCourse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating course", ex);
            }
        }
    }
}
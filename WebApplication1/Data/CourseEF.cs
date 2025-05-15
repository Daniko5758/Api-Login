using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                if (course == null)
                {
                    throw new ArgumentNullException(nameof(course), "Course cannot be null");
                }
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
           }
           catch (DbUpdateException dbex)
           {
               throw new Exception("Error adding course to the database", dbex);
           }
           catch (System.Exception ex)
           {
               throw new Exception("An error occurred while adding the course", ex);
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

        public IEnumerable<Course> GetAllCourse()
        {
            var courses = from c in _context.Courses.Include(c => c.category)
                          orderby c.CourseName descending
                          select c;
            return courses;
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

        public Course GetCourseByIdCourse(int CourseID)
        {
            var course = _context.Courses.Include(c => c.category)
                .FirstOrDefault(c => c.CourseID == CourseID);
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

        public IEnumerable<Course> GetCoursesByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
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
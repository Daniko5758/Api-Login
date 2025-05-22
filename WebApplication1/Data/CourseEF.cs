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

        public List<Course> GetAllCourse()
        {
            return _context.Courses
                .Include(c => c.category)
                .Include(c => c.Instructor)
                .ToList();
        }

        public Course GetCourseByIdCourse(int id)
        {
            return _context.Courses
            .Include(c => c.category)
            .Include(c => c.Instructor)
            .FirstOrDefault(c => c.CourseID == id);
        }

        public Course AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseID == course.CourseID);
            if (existingCourse == null) return null;

            existingCourse.CourseName = course.CourseName;
            existingCourse.CourseDescription = course.CourseDescription;
            existingCourse.Duration = course.Duration;
            existingCourse.CategoryID = course.CategoryID;
            existingCourse.InstructorID = course.InstructorID;

            _context.SaveChanges();
            return existingCourse;
        }


        public bool DeleteCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return true;
        }
    }
}
    
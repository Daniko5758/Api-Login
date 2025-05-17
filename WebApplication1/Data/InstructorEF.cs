using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Data
{
    public class InstructorEF : Iinstructor
    {
        private readonly ApplicationDbContext _context;

        public InstructorEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Instructor AddInstructor(Instructor instructor)
        {
            throw new NotImplementedException();
        }

        public void DeleteInstructor(int instructorId)
        {
                throw new NotImplementedException();
        }

        public IEnumerable<Instructor> GetAllInstructor()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instructor> GetInstructorByCourseId(int courseID)
        {
            throw new NotImplementedException();
        }

        public Instructor GetInstructorById(int instructorId)
        {
            throw new NotImplementedException();
        }

        public Instructor GetInstructorByIdInstructor(int InstructorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instructor> GetInstructors()
        {
            throw new NotImplementedException();
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            throw new NotImplementedException();
        }
    }
}
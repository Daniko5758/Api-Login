using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public interface Iinstructor
{
    IEnumerable<Instructor> GetInstructors();
    Instructor GetInstructorById(int instructorId);
    Instructor AddInstructor(Instructor instructor);
    Instructor UpdateInstructor(Instructor instructor);
    void DeleteInstructor(int instructorId);


    IEnumerable<Instructor> GetInstructorByCourseId(int courseID);
    IEnumerable<Instructor> GetAllInstructor();
    Instructor GetInstructorByIdInstructor(int InstructorId);
}

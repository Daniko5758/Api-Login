using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class InstructorDal : Iinstructor
{

    private List<Instructor> _instructors = new List<Instructor>();

    public InstructorDal()
    {
        _instructors = new List<Instructor>()
        {
            new Instructor() { InstructorID = 1, InstructorName = "Daniko Sutopo", InstructorEmail = "daniko@gmail.com", InstructorPhoneNumber = "123-456-7890", InstructorAddress = "123 Main St", InstructorCity = "New York" },
            new Instructor() { InstructorID = 2, InstructorName = "Christian Bagas", InstructorEmail = "christian@gmail.com", InstructorPhoneNumber = "987-654-3210", InstructorAddress = "456 Elm St", InstructorCity = "Los Angeles" },
        };
    }

    public IEnumerable<Instructor> GetInstructors()
    {
        return _instructors;
    }

    public Instructor GetInstructorById(int instructorId)
    {
        var instructor = _instructors.FirstOrDefault(i => i.InstructorID == instructorId);
        if (instructor == null)
        {
            throw new Exception("Instructor not found");
        }
        return instructor;
    }

    public Instructor AddInstructor(Instructor instructor)
    {
        _instructors.Add(instructor);
        return instructor;
    }

    public void DeleteInstructor(int instructorId)
    {
        var instructor = GetInstructorById(instructorId);
        if (instructor != null)
        {
            _instructors.Remove(instructor);
        }
    }

    public Instructor UpdateInstructor(Instructor instructor)
    {
        var existingInstructor = GetInstructorById(instructor.InstructorID);
        if (existingInstructor != null)
        {
            existingInstructor.InstructorName = instructor.InstructorName;
            existingInstructor.InstructorEmail = instructor.InstructorEmail;
            existingInstructor.InstructorPhoneNumber = instructor.InstructorPhoneNumber;
            existingInstructor.InstructorAddress = instructor.InstructorAddress;
            existingInstructor.InstructorCity = instructor.InstructorCity;
        }
        return existingInstructor;
    }

    public IEnumerable<Instructor> GetInstructorByCourseId(int courseID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Instructor> GetAllInstructor()
    {
        throw new NotImplementedException();
    }

    public Instructor GetInstructorByIdInstructor(int InstructorId)
    {
        throw new NotImplementedException();
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.AddScoped<Iinstructor, InstructorADO>();
// builder.Services.AddScoped<ICategory, CategoryADO>();
// builder.Services.AddSingleton<ICourse, CourseADO>();

//Dependency Injection
//builder.Services.AddSingleton<Iinstructor, InstructorDal>();

// Add EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Dependecy Injection
builder.Services.AddScoped<ICategory, CategoryEF>();
builder.Services.AddScoped<ICourse, CourseEF>();
builder.Services.AddScoped<Iinstructor, InstructorEF>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("api/v1/instructors", (Iinstructor instructorData) =>
{
    var instructors = instructorData.GetInstructors();
    return instructors;
}); 

// app.MapGet("api/v1/instructors/{id}", (Iinstructor instructorData, int id) =>
// {
//     var instructor = instructorData.GetInstructorById(id);
//     return instructor;
// });

app.MapPost("api/v1/instructors", ( Iinstructor instructorData, Instructor instructor) =>
{
    var newInstructor = instructorData.AddInstructor(instructor);
    return newInstructor;
});

app.MapPut("api/v1/instructors", (Iinstructor instructorData, Instructor instructor) =>
{
    var updatedInstructor = instructorData.UpdateInstructor(instructor);
    return updatedInstructor;
});

app.MapDelete("api/v1/instructors/{id}", (Iinstructor instructorData, int id) =>
{
    instructorData.DeleteInstructor(id);
    return Results.NoContent();
});

// app.MapGet("api/v1/categories", (ICategory categoryData) =>
// {
//     var categories = categoryData.GetCategories();
//     return categories;
// });

// app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
// {
//         var category = categoryData.GetCategoryById(id);
//         return category;
// });

// app.MapPost("api/v1/categories", (ICategory categoryData, Category category)=>
// {
//     var newCategory = categoryData.AddCategory(category);
//     return newCategory;
// });

// app.MapPut("api/v1/categories", (ICategory categoryData,Category category)=>
// {
//     var updatedCategory = categoryData.UpdateCategory(category);
//     return updatedCategory;
// });

// app.MapDelete("api/v1/categories/{id}", (ICategory categoryData,int id) =>
// {
//     categoryData.DeleteCategory(id);
//     return Results.NoContent();  
// });

app.MapGet("api/v1/courses", (ICourse courseData) =>
{
    var courses = courseData.GetAllCourse();
    var courseDTOs = courses.Select(course => new CourseDTO
    {
        CourseID = course.CourseID,
        CourseName = course.CourseName,
        CourseDescription = course.CourseDescription,
        Duration = course.Duration,
        category = new CategoryDTO
        {
            CategoryID = course.category.CategoryID,
            CategoryName = course.category.CategoryName
        },
        Instructor = new InstructorDTO
        {
            InstructorID = course.Instructor.InstructorID,
            InstructorName = course.Instructor.InstructorName
        }
    }).ToList();

    return courseDTOs;
});

app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var course = courseData.GetCourseByIdCourse(id);

    if (course == null)
    {
        return Results.NotFound($"Course dengan ID {id} tidak ditemukan.");
    }
    var courseDTO = new CourseDTO
    {
        CourseID = course.CourseID,
        CourseName = course.CourseName,
        CourseDescription = course.CourseDescription,
        Duration = course.Duration,
        category = course.category != null ? new CategoryDTO
        {
            CategoryID = course.category.CategoryID,
            CategoryName = course.category.CategoryName
        } : null,
        Instructor = course.Instructor != null ? new InstructorDTO
        {
            InstructorID = course.Instructor.InstructorID,
            InstructorName = course.Instructor.InstructorName
        } : null
    };
    return Results.Ok(courseDTO);
});

app.MapPost("api/v1/courses", (ICourse courseData, CourseAddDTO dto)=>
{
     var course = new Course
    {
        CourseName = dto.CourseName,
        CourseDescription = dto.CourseDescription,
        Duration = dto.Duration,
        CategoryID = dto.CategoryID,
        InstructorID = dto.InstructorID
    };

    try
    {
        var newCourse = courseData.AddCourse(course);
        return Results.Created($"/api/v1/courses/{newCourse.CourseID}", newCourse);
    }
    catch (Exception ex)
    {
        return Results.Problem("Error menambahkan course: " + ex.GetBaseException().Message);
    }
});

app.MapPut("api/v1/courses", (ICourse courseData, CourseUpdateDTO dto)=>
{
    var course = new Course
    {
        CourseID = dto.CourseID,
        CourseName = dto.CourseName,
        CourseDescription = dto.CourseDescription,
        Duration = dto.Duration,
        CategoryID = dto.CategoryID,
        InstructorID = dto.InstructorID
    };

    var updated = courseData.UpdateCourse(course);
    return updated != null
        ? Results.Ok(updated)
        : Results.NotFound($"Course dengan ID {dto.CourseID} tidak ditemukan.");
});
app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var deleted = courseData.DeleteCourse(id);
    return deleted
        ? Results.Ok($"Course dengan ID {id} berhasil dihapus.")
        : Results.NotFound($"Course dengan ID {id} tidak ditemukan.");
});

app.Run();


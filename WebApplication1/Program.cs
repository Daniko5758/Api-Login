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
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// app.MapGet("api/v1/instructors", (Iinstructor instructorData) =>
// {
//     var instructors = instructorData.GetInstructors();
//     return instructors;
// }); 

// app.MapGet("api/v1/instructors/{id}", (Iinstructor instructorData, int id) =>
// {
//     var instructor = instructorData.GetInstructorById(id);
//     return instructor;
// });

// app.MapPost("api/v1/instructors", ( Iinstructor instructorData, Instructor instructor) =>
// {
//     var newInstructor = instructorData.AddInstructor(instructor);
//     return newInstructor;
// });

// app.MapPut("api/v1/instructors", (Iinstructor instructorData, Instructor instructor) =>
// {
//     var updatedInstructor = instructorData.UpdateInstructor(instructor);
//     return updatedInstructor;
// });

// app.MapDelete("api/v1/instructors/{id}", (Iinstructor instructorData, int id) =>
// {
//     instructorData.DeleteInstructor(id);
//     return Results.NoContent();
// });

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
    List<CourseDTO> courseDTOs = new List<CourseDTO>();
    var courses = courseData.GetAllCourse();
    foreach (var course in courses)
    {
        CourseDTO courseDTO = new CourseDTO
        {
            CourseID = course.CourseID,
            CourseName = course.CourseName,
            CourseDescription = course.CourseDescription,
            Duration = course.Duration,
            category = new CategoryDTO
            {
                CategoryID = course.category.CategoryID,
                CategoryName = course.category.CategoryName
            }
        };
        courseDTOs.Add(courseDTO);
    }
    return courseDTOs;
});

app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var course = courseData.GetCourseByIdCourse(id);
    if (course == null)
    {
        return Results.NotFound();
    }
    CourseDTO courseDTO = new CourseDTO
    {
        CourseID = course.CourseID,
        CourseName = course.CourseName,
        CourseDescription = course.CourseDescription,
        Duration = course.Duration,
        category = new CategoryDTO
        {
            CategoryID = course.category.CategoryID,
            CategoryName = course.category.CategoryName
        }
    };
    return Results.Ok(courseDTO);
});

app.MapPost("api/v1/courses", (ICourse courseData, CourseAddDTO courseAddDTO)=>
{
    Course course = new Course
    {
        CourseName = courseAddDTO.CourseName,
        CourseDescription = courseAddDTO.CourseDescription,
        Duration = courseAddDTO.Duration,
        CategoryID = courseAddDTO.CategoryID
    };
    try
    {
        var newCourse = courseData.AddCourse(course);
        CourseDTO courseDTO = new CourseDTO
        {
            CourseName = newCourse.CourseName,
            CourseDescription = newCourse.CourseDescription,
            Duration = newCourse.Duration,
            category = new CategoryDTO
            {
                CategoryID = newCourse.CourseID
            }
        };
        return Results.Created($"/api/v1/courses/{newCourse.CourseID}", courseDTO); 
    }
    catch (DbUpdateException dbex)
    {
        throw new Exception("Error adding course to the database", dbex);
    }
    catch (System.Exception ex)
    {
        throw new Exception("An error occurred while adding the course", ex);
    }
});

// app.MapPut("api/v1/courses", (ICourse courseData, Course course)=>
// {
//     var updatedCourse = courseData.UpdateCourse(course);
//     return updatedCourse;
// });
// app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
// {
//     courseData.DeleteCourse(id);
//     return Results.NoContent();
// });

app.Run();


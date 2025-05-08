using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
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
// builder.Services.AddScoped<ICategory, CategoryEF>();
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
    var courses = courseData.GetCourses();
    return courses;
});

app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var course = courseData.GetCourseById(id);
    return course;
});

app.MapPost("api/v1/courses", (ICourse courseData, Course course)=>
{
    var newCourse = courseData.AddCourse(course);
    return newCourse;
});

app.MapPut("api/v1/courses", (ICourse courseData, Course course)=>
{
    var updatedCourse = courseData.UpdateCourse(course);
    return updatedCourse;
});
app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    courseData.DeleteCourse(id);
    return Results.NoContent();
});

app.Run();


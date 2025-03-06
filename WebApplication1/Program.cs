using WebApplication1.Data;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Dependency Injection
builder.Services.AddSingleton<Iinstructor, InstructorDal>();

//Dependecy Injection
builder.Services.AddSingleton<ICategory, CategoryDal>();
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

app.MapGet("api/v1/instructors/{id}", (Iinstructor instructorData, int id) =>
{
    var instructor = instructorData.GetInstructorById(id);
    return instructor;
});

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

app.MapGet("api/v1/categories", (ICategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return categories;
});

app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
        var category = categoryData.GetCategoryById(id);
        return category;
});

app.MapPost("api/v1/categories", (ICategory categoryData, Category category)=>
{
    var newCategory = categoryData.AddCategory(category);
    return newCategory;
});

app.MapPut("api/v1/categories", (ICategory categoryData,Category category)=>
{
    var updatedCategory = categoryData.UpdateCategory(category);
    return updatedCategory;
});

app.MapDelete("api/v1/categories/{id}", (ICategory categoryData,int id) =>
{
    categoryData.DeleteCategory(id);
    return Results.NoContent();
});

app.Run();


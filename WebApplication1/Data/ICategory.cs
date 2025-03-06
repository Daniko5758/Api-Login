using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public interface ICategory
{
    //crud
    IEnumerable<Category> GetCategories();
    Category GetCategoryById(int CategoryId);
    Category AddCategory(Category category);
    Category UpdateCategory(Category category);
    void DeleteCategory(int CategoryId);

}

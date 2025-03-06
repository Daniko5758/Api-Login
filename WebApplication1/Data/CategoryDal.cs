    using System;
    using WebApplication1.Models;

    namespace WebApplication1.Data;

    public class CategoryDal : ICategory
    {
        private List<Category> _categories = new List<Category>();

        public CategoryDal()
        {
            _categories = new List<Category>()
            {
                new Category() { CategoryId = 1, CategoryName = "ASP.NET CORE"}, 
                new Category() { CategoryId = 2, CategoryName = "ASP.NET MVC"},
                new Category() { CategoryId = 3, CategoryName = "ASP.NET WEB API"},
                new Category() { CategoryId = 4, CategoryName = "Blazor"},
                new Category() { CategoryId = 5, CategoryName = "Xamarin"},
                new Category() { CategoryId = 6, CategoryName = "Azure"},

            };
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categories; 

        }

        public Category GetCategoryById(int CategoryId)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public Category AddCategory(Category category)
        {
            _categories.Add(category);
            return category;
        }

        public void DeleteCategory(int CategoryId)
        {
            var category = GetCategoryById(CategoryId);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
            }
            return existingCategory;
        }
    }

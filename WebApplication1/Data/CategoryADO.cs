using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CategoryADO : ICategory
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public CategoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }
        public Category AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                
                string strSql = @"SELECT * FROM Categories ORDER BY CategoryName";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Category category = new();
                        category.CategoryID = Convert.ToInt32(dr["CategoryId"]);
                        category.CategoryName = dr["CategoryName"].ToString();
                        categories.Add(category);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

            }
            return categories;
        }

        public Category GetCategoryById(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Category UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
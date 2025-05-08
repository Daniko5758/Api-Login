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
            using (SqlConnection conn = new SqlConnection(connStr))
            {
              string strsql = @"INSERT INTO categories (categoryName) VALUES (@categoryName); SELECT SCOPE_IDENTITY()"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
              SqlCommand cmd = new SqlCommand(strsql, conn);
              try
                {

                    cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                    conn.Open();
                    int categoryID = Convert.ToInt32(cmd.ExecuteScalar());
                    category.CategoryID = categoryID;
                    return category;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                { 
                cmd.Dispose();
                conn.Close();
                }
            }
        }


        public void DeleteCategory(int CategoryId)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"DELETE FROM categories WHERE categoryID = @categoryID"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@categoryID", CategoryId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result==0)
                    {
                        throw new Exception("Category not found");
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
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
            Category category = new();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT*FROM categories WHERE categoryID = @category"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
                //jangan pakai string biasa untuk menghindari sql injeksion di sanitize dulu
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@category", CategoryId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(); //baca data pakai data reader trs dimaping make while
                if(dr.HasRows)
                {
                    dr.Read();
                    //dimaping di class
                    
                    category.CategoryID = Convert.ToInt32(dr["categoryID"]);
                    category.CategoryName = dr["categoryName"].ToString();
                }      
                else
                {throw new Exception("Category not found");}
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE categories SET categoryName = @categoryName
                WHERE categoryID = @categoryID"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try{
                    cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("@categoryID", category.CategoryID);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result==0)
                    {
                        throw new Exception("Category not found");
                    }
                    return category;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
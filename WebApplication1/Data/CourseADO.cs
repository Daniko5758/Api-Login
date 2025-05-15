using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CourseADO : ICourse
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public CourseADO(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Course AddCourse(Course course)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"INSERT INTO Courses (CourseName, CourseDescription, Duration, CategoryId)
                      VALUES (@CourseName, @CourseDescription, @Duration, @CategoryId); select SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                    cmd.Parameters.AddWithValue("@Duration", course.Duration);
                    cmd.Parameters.AddWithValue("@CategoryId", course.CategoryID);

                    conn.Open();
                    int courseID = Convert.ToInt32(cmd.ExecuteScalar());
                    course.CourseID = courseID;
                    return course;
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

        public void DeleteCourse(int CourseID)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"DELETE FROM Courses WHERE CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseId", CourseID);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Course not found");
                    }
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

        public IEnumerable<Course> GetAllCourse()
        {
            throw new NotImplementedException();
        }

        public ViewCourseWithCategory GetCourseById(int CourseID)
        {
            throw new NotImplementedException();
        }

        public Course GetCourseByIdCourse(int CourseID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT dbo.Courses.CourseId, dbo.Courses.CourseName, dbo.Courses.CourseDescription, dbo.Courses.Duration, dbo.Categories.CategoryId, dbo.Categories.CategoryName
                                FROM   dbo.Categories INNER JOIN
                                dbo.Courses ON dbo.Categories.CategoryId = dbo.Courses.CategoryId";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                List<ViewCourseWithCategory> courses = new List<ViewCourseWithCategory>();
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViewCourseWithCategory course = new ViewCourseWithCategory();
                        course.CourseID = Convert.ToInt32(dr["CourseId"]);
                        course.CourseName = dr["CourseName"].ToString();
                        course.CourseDescription = dr["CourseDescription"].ToString();
                        course.CategoryID = Convert.ToInt32(dr["CategoryId"]);
                        course.CategoryName = dr["CategoryName"].ToString();
                        courses.Add(course);
                    }
                    return courses;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
             }
        }

        public IEnumerable<Course> GetCoursesByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Course UpdateCourse(Course course)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"UPDATE Courses
                      SET CourseName = @CourseName,
                          CourseDescription = @CourseDescription,
                          Duration = @Duration,
                          CategoryId = @CategoryId
                      WHERE CourseId = @CourseId";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                    cmd.Parameters.AddWithValue("@Duration", course.Duration);
                    cmd.Parameters.AddWithValue("@CategoryId", course.CategoryID);
                    cmd.Parameters.AddWithValue("@CourseId", course.CourseID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return course;
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

        Course ICourse.GetCourseById(int CourseID)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Course> ICourse.GetCourses()
        {
            throw new NotImplementedException();
        }
    }
}
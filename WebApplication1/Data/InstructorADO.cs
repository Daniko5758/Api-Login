using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Data
{
    public class InstructorADO
    {
        public class InstructorADO : Iinstructor
    {
        private IConfiguration _configuration;
        private string connStr = string.Empty;
        public InstructorADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Instructor addInstructor(Instructor Instructor)
        {
             using (SqlConnection connection = new SqlConnection(connStr))
            {
                string strsql = @"DELETE FROM Instructor WHERE InstructorID = @InstructorID";
                SqlCommand cmd = new SqlCommand(strsql, connection);
                cmd.Parameters.AddWithValue("@InstructorID", Instructor.InstructorID);
                connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }
            return Instructor;
        }

        public void deleteInstructor(int InstructorID)
        {
             using (SqlConnection connection = new SqlConnection(connStr))
            {
                string strsql = @"DELETE FROM Instructor WHERE InstructorID = @InstructorID";
                SqlCommand cmd = new SqlCommand(strsql, connection);
                cmd.Parameters.AddWithValue("@InstructorID", InstructorID);
                connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }
        }

        public Instructor GetInstructorById(int InstructorID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instructor> GetInstructors()
        {
            List<Instructor> instructors = new List<Instructor>();
            using(SqlConnection connection = new SqlConnection(connStr))
            {
                string strsql = @"SELECT * FROM Instructor ORDER BY InstructorName";
                SqlCommand cmd = new SqlCommand(strsql, connection);
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Instructor instructor = new();
                        instructor.InstructorID = Convert.ToInt32(dr["InstructorID"]);
                        instructor.InstructorName = dr["InstructorName"].ToString();
                        instructor.InstructorEmail = dr["InstructorEmail"].ToString();
                        instructor.InstructorPhone = dr["InstructorPhone"].ToString();
                        instructor.InstructorAddress = dr["InstructorAddress"].ToString();
                        instructor.InstructorCity = dr["InstructorCity"].ToString();
                        instructors.Add(instructor);
                    }
                }
                dr.Close();
                cmd.Dispose();
                connection.Close();
            }
            return instructors;
        }

        public Instructor updateInstructor(Instructor Instructor)
        {
            throw new NotImplementedException();
        }
    }
    }
}
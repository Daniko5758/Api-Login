using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;


namespace WebApplication1.Data
{
    public class InstructorADO : Iinstructor
    {
        private readonly string _connStr;

        public InstructorADO(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("DefaultConnection");
        }


        public Instructor AddInstructor(Instructor instructor)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                string sql = @"INSERT INTO Instructor (InstructorName, InstructorEmail, InstructorPhoneNumber, InstructorAddress, InstructorCity)
                               VALUES (@InstructorName, @InstructorEmail, @InstructorPhoneNumber, @InstructorAddress, @InstructorCity);
                               SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                cmd.Parameters.AddWithValue("@InstructorPhoneNumber", instructor.InstructorPhoneNumber);
                cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);

                connection.Open();
                instructor.InstructorId = Convert.ToInt32(cmd.ExecuteScalar()); // Ambil ID baru
                connection.Close();
            }
            return instructor;
        }


        public IEnumerable<Instructor> GetInstructors()
        {
            List<Instructor> instructors = new List<Instructor>();

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                string sql = "SELECT * FROM Instructor ORDER BY InstructorName";
                SqlCommand cmd = new SqlCommand(sql, connection);
                
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    instructors.Add(new Instructor
                    {
                        InstructorId = Convert.ToInt32(dr["InstructorID"]),
                        InstructorName = dr["InstructorName"].ToString(),
                        InstructorEmail = dr["InstructorEmail"].ToString(),
                        InstructorPhoneNumber = dr["InstructorPhoneNumber"].ToString(),
                        InstructorAddress = dr["InstructorAddress"].ToString(),
                        InstructorCity = dr["InstructorCity"].ToString()
                    });
                }

                dr.Close();
                connection.Close();
            }
            return instructors;
        }

    // READ - Dapatkan Instruktur Berdasarkan ID
        public Instructor GetInstructorById(int instructorId)
        {
            Instructor instructor = null;

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                string sql = "SELECT * FROM Instructor WHERE InstructorID = @InstructorID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@InstructorID", instructorId);

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    instructor = new Instructor
                    {
                        InstructorId = Convert.ToInt32(dr["InstructorID"]),
                        InstructorName = dr["InstructorName"].ToString(),
                        InstructorEmail = dr["InstructorEmail"].ToString(),
                        InstructorPhoneNumber = dr["InstructorPhoneNumber"].ToString(),
                        InstructorAddress = dr["InstructorAddress"].ToString(),
                        InstructorCity = dr["InstructorCity"].ToString()
                    };
                }

                dr.Close();
                connection.Close();
            }
            return instructor;
        }

        // UPDATE - Perbarui Data Instruktur
        public Instructor UpdateInstructor(Instructor instructor)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                string sql = @"UPDATE Instructor 
                               SET InstructorName = @InstructorName,
                                   InstructorEmail = @InstructorEmail,
                                   InstructorPhoneNumber = @InstructorPhoneNumber,
                                   InstructorAddress = @InstructorAddress,
                                   InstructorCity = @InstructorCity
                               WHERE InstructorID = @InstructorID";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@InstructorID", instructor.InstructorId);
                cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                cmd.Parameters.AddWithValue("@InstructorPhoneNumber", instructor.InstructorPhoneNumber);
                cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return instructor;
        }

        // DELETE - Hapus Instruktur
        public void DeleteInstructor(int instructorId)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                string sql = "DELETE FROM Instructor WHERE InstructorID = @InstructorID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@InstructorID", instructorId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
    }

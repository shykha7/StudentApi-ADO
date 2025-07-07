using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using StudentApi.Models;

namespace StudentApi.Repositories
{
    public class StudentRepository
    {
        private readonly string _connectionString;
        
        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Student> AddStudent(Student student)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddStudents", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Course", student.Course);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Course = reader["Course"].ToString()
                    });
                }

                reader.Close();
            }

            return students;
        }


          
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Student.Model;

namespace Student.Repository
{

    public class StudentRepository


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");


        public List<Student> GetStudents()
        {

            SqlCommand command = new SqlCommand
                ("SELECT * FROM Student;", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Student student = new Student();
                    student.student_id = reader.GetInt32(0);
                    student.first_name = reader.GetString(1);
                    student.last_name = reader.GetString(2);
                    student.adress_id = reader.GetInt32(3);


                    list_of_students.Add(student);
                }
                reader.Close();
                connection.Close();
                return list_of_students;
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }
        }


        public Student GetStudentById(int Student_id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Student WHERE Student_id = @Student_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@Student_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = Student_id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Student student = new Student();
                    student.student_id = Student_id;
                    student.first_name = reader.GetString(1);
                    student.last_name = reader.GetString(2);
                    student.adress_id = reader.GetInt32(3);

                        
                }
                connection.Close();
                return student;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public void CreateStudent(Student student)
        {
            Student _student = new Student();
            _student.student_id = student.student_id;
            _student.first_name = student.first_name;
            _student.last_name = student.last_name;
            _student.adress_id = student.adress_id;

            SqlCommand command = new SqlCommand("INSERT INTO Student (Student_id, First_name , Last_name , Adress_id )" +
                                                "VALUES ({_student.student_id},'{_student.first_name}','{_student.last_name}',{_student.adress_id})", connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }


        public bool UpdateStudent(int student_id, Student student)
        {
            Student _student = new Student();
            _student.student_id = student.student_id;
            _student.first_name = student.first_name;
            _student.last_name = student.last_name;
            _student.adress_id = student.adress_id;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Student WHERE Student_id ={student_id} ", connection);
            connection.Open();

            SqlDataReader reader = command_id.ExecuteReader();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Student SET First_name = {_student.first_name}, Last_name='{_student.last_name}', " +
                                                 "Adress_id='{_student.adress_id}' WHERE Student_id={student_id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public bool DeleteStudent(int student_id)
        {
            SqlCommand command_id = new SqlCommand("SELECT * FROM Student WHERE Student_id={student_id} ", connection);
            SqlCommand command = new SqlCommand();
            connection.Open();

            SqlDataReader reader = command_id.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    command = new SqlCommand("DELETE FROM Student WHERE Student_id={student_id}; ", connection);
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
    }
}
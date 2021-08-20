using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Student.Model;

namespace Student._Repository
{

    public class StudentRepository


    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BB9OT7S\\SQLEXPRESS;Initial Catalog = Martina; Integrated Security = True");


        public List<Students> GetStudents()
        {

            SqlCommand command = new SqlCommand
                ("SELECT * FROM Student;", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<Students> list_of_students = new List<Students>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Students student = new Students();
                    student.Student_id = reader.GetInt32(0);
                    student.First_name = reader.GetString(1);
                    student.Last_name = reader.GetString(2);
                    student.Adress_id = reader.GetInt32(3);


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


        public Students GetStudentById(int Student_id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand
                ("SELECT * FROM Student WHERE Student_id = @Student_id;", connection);

            SqlParameter id_parameter = new SqlParameter("@Student_id", System.Data.SqlDbType.Int);

            command.Parameters.Add(id_parameter).Value = Student_id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Students student = new Students();
                while (reader.Read())
                {
                    
                    student.Student_id = Student_id;
                    student.First_name = reader.GetString(1);
                    student.Last_name = reader.GetString(2);
                    student.Adress_id = reader.GetInt32(3);


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

        public bool CreateStudent(Students student)          
        {

            Students _student = new Students();
            _student.Student_id = student.Student_id;
            _student.First_name = student.First_name;
            _student.Last_name = student.Last_name;
            _student.Adress_id = student.Adress_id;

            SqlCommand command = new SqlCommand("INSERT INTO Student (Student_id, First_name , Last_name , Adress_id )" +
                                                "VALUES (@Student_id, @First_name, @Last_name, @Adress_id); ", connection);

            SqlCommand command_id = new SqlCommand("SELECT * FROM Student WHERE Student_id =@Student_id ", connection);
            command_id.Parameters.Add("Student_id", SqlDbType.Int).Value = _student.Student_id;


            SqlCommand command_check = new SqlCommand("SELECT * FROM Student WHERE Adress_id = @adress_id; ", connection);
            command_check.Parameters.Add("@adress_id", SqlDbType.Int).Value = _student.Adress_id;

            connection.Open();

            SqlDataReader reader_check_unique = command_id.ExecuteReader();    //provjerava postoji li vec student s danim id

            if (reader_check_unique.HasRows)
            {
                return false;
            }

            reader_check_unique.Close();

            SqlDataReader reader_check = command_check.ExecuteReader();    //provjerava je li adresa postojeca, inace ju ne moze dodati

            if (!reader_check.HasRows)
            {
                return false;
            }

            reader_check.Close();

            command.Parameters.Add("@Student_id", SqlDbType.Int).Value = _student.Student_id;
            command.Parameters.Add("@First_name", SqlDbType.VarChar).Value = _student.First_name;
            command.Parameters.Add("@Last_name", SqlDbType.VarChar).Value = _student.Last_name;
            command.Parameters.Add("@Adress_id", SqlDbType.Int).Value = _student.Adress_id;

          
            command.ExecuteNonQuery();
            connection.Close();
            return true;

        }


        public bool UpdateStudent(int student_id, Students student)     
        {
            Students _student = new Students();
            _student.Student_id = student_id;
            _student.First_name = student.First_name;
            _student.Last_name = student.Last_name;
            _student.Adress_id = student.Adress_id;

            SqlCommand command_id = new SqlCommand("SELECT * FROM Student WHERE Student_id =@Student_id ", connection);
            command_id.Parameters.Add("Student_id", SqlDbType.Int).Value = student_id;

            SqlCommand command_check = new SqlCommand("SELECT * FROM Student WHERE Adress_id = @adress_id; ", connection);
            command_check.Parameters.Add("@adress_id", SqlDbType.Int).Value = _student.Adress_id;

            connection.Open();

            SqlDataReader reader_check = command_check.ExecuteReader();        //provjerava je li adresa postojeca, inace ju ne moze promijeniti

            if (!reader_check.HasRows)
            {
                return false;
            }

            reader_check.Close();

            SqlDataReader reader = command_id.ExecuteReader();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand("UPDATE Student SET First_name = @First_name, Last_name=@Last_name, " +
                                                 "Adress_id=@Adress_id WHERE Student_id=@Student_id", connection);

            command.Parameters.Add("First_name", SqlDbType.VarChar).Value = _student.First_name;
            command.Parameters.Add("Last_name", SqlDbType.VarChar).Value = _student.Last_name;
            command.Parameters.Add("Adress_id", SqlDbType.Int).Value = _student.Adress_id;
            command.Parameters.Add("Student_id", SqlDbType.Int).Value = student_id;

            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public bool DeleteStudent(int student_id)
        {
            SqlCommand commandId = new SqlCommand("SELECT * FROM Student WHERE Student_id=@student_id; ", connection);
            SqlCommand command = new SqlCommand();
            connection.Open();

            commandId.Parameters.Add("@student_id", SqlDbType.Int).Value = student_id;
            SqlDataReader reader = commandId.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    command = new SqlCommand("DELETE FROM Student WHERE Student_id=@student_id; ", connection);
                    command.Parameters.Add("@student_id", SqlDbType.Int).Value = student_id;
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